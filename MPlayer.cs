using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP
{
    public class MPlayer : ModPlayer
    {
		public static bool hasProjectile;
		public static bool hasPull;
		public static bool pdt;
		public static bool flyChannel;
		public static bool altFire;
		public static float healPowerMult = 1f;
		
		//Great method for healing players
		public static void healPlayer(Player player, int healPower)
		{
			int heal = (int) ((float)healPower * healPowerMult);
			player.statLife += heal;
			if(player.statLife > player.statLifeMax2)
			{
				player.statLife = player.statLifeMax2;
			}
			player.HealEffect(heal, true);
		}
		
		//Great method for giving mana to players
		public static void starPlayer(Player player, int starPower)
		{
			player.statMana += starPower;
			if(player.statMana > player.statManaMax2)
			{
				player.statMana = player.statManaMax2;
			}
			player.ManaEffect(starPower);
		}
		
		public static void ResetPlayerEffects() //Just sets all properties to false
		{
			hasProjectile = false;
			hasPull = false;
			pdt = false;
			flyChannel = false;
			altFire = false;
		}
		
		//Stuff that happens after an update
		public override void PostUpdate()
		{
			if(Main.myPlayer == player.whoAmI)
			{
				//Check for armor if it has it, then provide buff
				for(int i = 0; i < 8 + player.extraAccessorySlots; i++)
				{
					if(player.armor[i].type == mod.ItemType("ShadowBloodVeil"))
					{
						player.AddBuff(mod.BuffType("ShadowBloodVeil"), 180, true);
					}
				}
				
				//Checks if player is holding down grappling hook key
				if(player.controlHook)
				{
					//Getting Item Properties
					Item item = null;
					if (item == null && player.miscEquips[4].type == mod.ItemType("BottledFairy"))
					{
						item = player.miscEquips[4];
					}
					int shoot = item.shoot;
					float shootSpeed = item.shootSpeed;
					int damage = item.damage;
					float knockBack = item.knockBack;
					
					//Firing the projectile
					Vector2 vector = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
					float num16 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
					float num17 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
					if (player.gravDir == -1f)
					{
						num17 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
					}
					float num18 = (float)Math.Sqrt((double)(num16 * num16 + num17 * num17));
					if ((float.IsNaN(num16) && float.IsNaN(num17)) || (num16 == 0f && num17 == 0f))
					{
						num16 = (float)player.direction;
						num17 = 0f;
						num18 = shootSpeed;
					}
					else
					{
						num18 = shootSpeed / num18;
					}
					num16 *= num18;
					num17 *= num18;
					
					//Check for any active projectile of type pull if false, then produce it
					for (int i = 0; i < 1000; i++)
					{
						if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
						{
							if(Main.projectile[i].type == mod.ProjectileType("FleeingFairy"))
							{
								hasPull = true;
								break;
							}
							else
							{
								hasPull = false;
							}
						}
					}
					if(!hasPull)
					{
						Projectile.NewProjectile(vector.X, vector.Y, num16, num17, shoot, damage, knockBack, player.whoAmI, 0f, 0f);
					}
					flyChannel = true;
				}
				else
				{
					flyChannel = false;
				}
				
				//Activate PDT buff if PDT Core is equipped on Pet Slot
				if(player.miscEquips[0].type == mod.ItemType("PDTCore"))
				{
					player.AddBuff(mod.BuffType("PDT"), 3600, true);
				}
				
				//Spawn a PDT if there is no PDT and buff is active
				if(player.HasBuff(mod.BuffType("PDT")) != -1)
				{
					pdt = true;
					bool noPDT = true;
					if(player.ownedProjectileCounts[mod.ProjectileType("PDT")] > 0)
					{
						noPDT = false;
					}
					if(noPDT && player.whoAmI == Main.myPlayer)
					{
						Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("PDT"), 0, 0f, player.whoAmI, 0f, 0f);
					}
				}
			}
		}
		
		//Makes player spawn immediately after kill if testmode is on
		public override void Kill(double damage, int hitDirection, bool pvp, string deathText)
		{
			if(MMod.testMode)
			{
				player.respawnTimer = 0;
			}
		}
		
		//You actually need this in order to apply the texture to the player
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			//If the selected item is the Midas Sprayer draw it
			if(player.inventory[player.selectedItem].type == mod.ItemType("MidasSprayer"))
			{
				//Making a draw information
				PlayerDrawInfo drawInfo = default(PlayerDrawInfo); //Make an instance of Draw Info
				Vector2 Position = player.position; //Make a variable Position for player position
				drawInfo.drawPlayer = player; //The draw info's player is the player
				drawInfo.position = player.position; //Draw position is the player's position
				drawInfo.shadow = 0f; //Set the shadow as a 0
				float shadow = 0f; //Make another variable shadow as 0 also
				
				//Get a vector for the player position
				Vector2 vector = player.position + (player.itemLocation - player.position);
				drawInfo.itemLocation = vector;
				
				//Get the alpha of the surrounding area
				//player.GetImmuneAlphaPure(Lighting.GetColor((int)((double)Position.X + (double)player.width * 0.5) / 16, (int)((double)Position.Y + (double)player.height * 0.5) / 16, Color.White), shadow);
				
				//Make draw data of value
				DrawData value = default(DrawData);
				
				//Putting the texture up
				//Getting the color of the player
				Color color12 = player.GetImmuneAlphaPure(Lighting.GetColor((int)((double)Position.X + (double)player.width * 0.5) / 16, (int)((double)Position.Y + (double)player.height * 0.5) / 16, Color.White), shadow);
				SpriteEffects spriteEffects = SpriteEffects.None; //Setting so there is no Sprite Effects
				Texture2D MidasBackpack = mod.GetTexture("Items/Usables/MidasBackpack"); //Getting the texture
				//Putting up the draw data
				value = new DrawData(MidasBackpack, new Vector2((float)((int)(Position.X - Main.screenPosition.X + (float)(player.width / 2) - (float)(9 * player.direction))) + -4f * (float)player.direction, (float)((int)(Position.Y - Main.screenPosition.Y + (float)(player.height / 2) + 2f * player.gravDir + -8f * player.gravDir))), new Rectangle?(new Rectangle(0, 0, MidasBackpack.Width, MidasBackpack.Height)), color12, player.bodyRotation, new Vector2((float)(MidasBackpack.Width / 2), (float)(MidasBackpack.Height / 2)), 1f, spriteEffects, 0);
				value.shader = 5; //Set shader as value 5, which is the shader in which the elf melter backpack uses.
				Main.playerDrawData.Add(value);
			}
		}
	}
}