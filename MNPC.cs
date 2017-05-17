using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP
{
	public class MNPC : GlobalNPC
	{	
		//A static method great for healing NPCs
		public static void healNPC(NPC npc, int healPower)
		{
			int heal = (int) ((float)healPower * MPlayer.healPowerMult);
			npc.life += heal;
			if(npc.life > npc.lifeMax)
			{
				npc.life = npc.lifeMax;
			}
			CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), new Color(100, 255, 100, 255), string.Concat(heal), false, false);
		}
		
		//Affects NPC when hit by Projectile
		public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			int fokmyl = mod.NPCType("Fokmyl");
			int lyndmyl = mod.NPCType("Lyndmyl");
			int niremyl = mod.NPCType("Niremyl");
			int veiynamyl = mod.NPCType("Veiynamyl");
			int berramyr = mod.NPCType("Berramyr");
			int[] berraIDs = {fokmyl, lyndmyl, niremyl, veiynamyl, berramyr};
			for(int j = 0; j < 5; j++)
			{
				if(npc.type == berraIDs[j])
				{
					int fSlot = NPC.FindFirstNPC(fokmyl);
					int nSlot = NPC.FindFirstNPC(niremyl);
					if(fSlot >= 0 && !Main.npc[fSlot].dontTakeDamage)
					{
						if(Main.npc[fSlot].ai[0] % 30 == 0)
						{
							Main.player[projectile.owner].statLife -= (int)(damage / 4);
							CombatText.NewText(new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height), new Color(255, 60, 70, 255), string.Concat((int)damage / 4), false, true);
						}
					}
					if(nSlot >= 0 && !Main.npc[nSlot].dontTakeDamage)
					{
						Main.player[projectile.owner].AddBuff(BuffID.OnFire, 180, false);
					}
				}
			}
		}
		
		//Affects NPC when hit by an item
		public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			int fokmyl = mod.NPCType("Fokmyl");
			int lyndmyl = mod.NPCType("Lyndmyl");
			int niremyl = mod.NPCType("Niremyl");
			int veiynamyl = mod.NPCType("Veiynamyl");
			int berramyr = mod.NPCType("Berramyr");
			int[] berraIDs = {fokmyl, lyndmyl, niremyl, veiynamyl, berramyr};
			for(int j = 0; j < 5; j++)
			{
				if(npc.type == berraIDs[j])
				{
					int fSlot = NPC.FindFirstNPC(fokmyl);
					int nSlot = NPC.FindFirstNPC(niremyl);
					if(fSlot >= 0 && !Main.npc[fSlot].dontTakeDamage)
					{
						if(Main.npc[fSlot].ai[0] % 30 == 0)
						{
							player.statLife -= (int)(damage / 4);
							CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(255, 60, 70, 255), string.Concat((int)damage / 4), false, true);
						}
					}
					if(nSlot >= 0 && !Main.npc[nSlot].dontTakeDamage)
					{
						player.AddBuff(BuffID.OnFire, 180, false);
					}
				}
			}
		}
		
		public override void PostAI(NPC npc)
		{
			int fokmyl = mod.NPCType("Fokmyl");
			int lyndmyl = mod.NPCType("Lyndmyl");
			int niremyl = mod.NPCType("Niremyl");
			int veiynamyl = mod.NPCType("Veiynamyl");
			int berramyr = mod.NPCType("Berramyr");
			int[] berraIDs = {fokmyl, lyndmyl, niremyl, veiynamyl, berramyr};
			for(int i = 0; i < 255; i++)
			{
				Player p = Main.player[i];
				Rectangle mb = new Rectangle((int)npc.position.X + (int)npc.velocity.X, (int)npc.position.Y + (int)npc.velocity.Y, npc.width, npc.height); 
				Rectangle nb = new Rectangle((int)p.position.X, (int)p.position.Y, p.width, p.height); 
				
				for(int j = 0; j < 5; j++)
				{
					if (mb.Intersects(nb) && p.FindBuffIndex(BuffID.OnFire) == -1 && npc.type == berraIDs[j])
					{
						p.AddBuff(BuffID.OnFire, 180, false);
					}
				}
			}
		}
		
		//Affects drop of NPCs
		public override void NPCLoot(NPC npc)
		{
			//Drops ore depending on buff
			if(npc.FindBuffIndex(mod.BuffType("CTSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.CopperOre, ItemID.TinOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("ILSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.IronOre, ItemID.LeadOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("STSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.SilverOre, ItemID.TungstenOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("GPSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.GoldOre, ItemID.PlatinumOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("MSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.Meteorite, -1);
			}
			if(npc.FindBuffIndex(mod.BuffType("DCSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.DemoniteOre, ItemID.CrimtaneOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("OHSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.Obsidian, ItemID.Hellstone);
			}
			if(npc.FindBuffIndex(mod.BuffType("CPSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.CobaltOre, ItemID.PalladiumOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("MOSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.MythrilOre, ItemID.OrichalcumOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("ATSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.AdamantiteOre, ItemID.TitaniumOre);
			}
			if(npc.FindBuffIndex(mod.BuffType("CSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.ChlorophyteOre, -1);
			}
		}
		
		//Static method that can be called on other class files to help drop two types of ores 
		//(though it doesn't have to be ores)
		public static void oreDropHelper(NPC npc, int oType1, int oType2)
		{
			int factor = 0;
			int amount = 0;
			int rng = 0;
			
			//Keep doing this while the amount is less than 0 because you can't have a negative amounts of items
			do
			{
				factor = Main.rand.Next(-10, 10);
				amount = npc.lifeMax/10 + factor;
			} while(amount < 0);
			
			//Determines what to drop
			rng = Main.rand.Next(0, 10);
			if(rng < 5 || oType2 == -1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, oType1, amount, false, 0, false);
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, oType2, amount, false, 0, false);
			}
		}
		
		public static void entrapment(NPC npc, int Berramyr, double rad)
		{
			npc.position.X = Main.player[Main.npc[Berramyr].target].Center.X - (int)(Math.Cos(rad) * npc.ai[3]) - npc.width/2;
			npc.position.Y = Main.player[Main.npc[Berramyr].target].Center.Y - (int)(Math.Sin(rad) * npc.ai[3]) - npc.height/2;
				
			int x = (int)(npc.Center.X / 16f); //Tile position X
			int y = (int)(npc.Center.Y / 16f); //Tile position y
			//If tile position X is less than 0, then set it to 0 since the x position can't be less than 0
			if (x < 0)
			{
				x = 0;
			}
			//If tile position X is greater than the maximum threashold, set it to the max threashold
			if(x > Main.maxTilesX)
			{
				x = Main.maxTilesX;
			}
			//Same thing as X
			if (y < 0)
			{
				y = 0;
			}
			if(y > Main.maxTilesY)
			{
				y = Main.maxTilesY;
			}
				
			Tile tile = Main.tile[x, y];
			if(Main.tile[x, y] == null || !Main.tile[x, y].active())
			{
				tile = new Tile();
				Main.tile[x, y] = tile;
				Main.tile[x, y].active(true);
				Main.tile[x, y].type = (ushort)1;
				WorldGen.SquareTileFrame(x, y, true);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
			else
			{
				Main.tile[x, y].type = (ushort)1;
				WorldGen.SquareTileFrame(x, y, true);
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
	}
}