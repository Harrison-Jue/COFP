using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Flying
{	
	public class FleeingFairy : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Fleeing Fairy";
			projectile.width = 42;
			projectile.height = 30;
			Main.projFrames[projectile.type] = 4;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.aiStyle = 7; //Make it a grappling hook so you can equip it
		}
		public override bool PreAI()
		{
			return false; //Make sure to not use Vanilla AI of aiStyle 7
		}
		public override void PostAI()
        {
			//Player variable
			Player player = Main.player[projectile.owner];
			
			//Light/Dust
			projectile.light = 2f;
			int DustID1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y ), projectile.width , projectile.height , 57, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y ), projectile.width , projectile.height , 72, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y ), projectile.width , projectile.height , 15, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			int DustID4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y ), projectile.width , projectile.height , 71, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			Main.dust[DustID1].noGravity = true;
			Main.dust[DustID2].noGravity = true;
			Main.dust[DustID3].noGravity = true;
			Main.dust[DustID4].noGravity = true;
			
			//Animation
			projectile.frameCounter++;
			if(projectile.frameCounter < 4)
				projectile.frame = 0;
			else if (projectile.frameCounter >= 4 && projectile.frameCounter < 8)
				projectile.frame = 1;
			else if (projectile.frameCounter >= 8 && projectile.frameCounter < 12)
				projectile.frame = 2;
			else if (projectile.frameCounter >= 12 && projectile.frameCounter < 16)
				projectile.frame = 3;
			else
				projectile.frameCounter = 0;
			
			//Check and give Velocity to player
			if (Main.myPlayer == projectile.owner)
			{
				if (MPlayer.flyChannel)
				{
					//Makes sure that player faces where projectile faces
					Main.player[projectile.owner].direction = projectile.direction;
					
					//Adjust player speed
					float num146 = 32f;
					Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num147 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
					float num148 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
					if (Main.player[projectile.owner].gravDir == -1f)
					{
						num148 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
					}
					float num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
					num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
					num149 = num146 / num149;
					num147 *= num149;
					num148 *= num149;
					int num150 = (int)(num147 * 1000f);
					int num151 = (int)(projectile.velocity.X * 1000f);
					int num152 = (int)(num148 * 1000f);
					int num153 = (int)(projectile.velocity.Y * 1000f);
					if (num150 != num151 || num152 != num153)
					{
						projectile.netUpdate = true;
					}
					projectile.velocity.X = num147;
					projectile.velocity.Y = num148;
					player.velocity.X = projectile.velocity.X;
					player.velocity.Y = projectile.velocity.Y;
					projectile.velocity.X = num147 + 0.5f;
					projectile.velocity.Y = num148 + 0.5f;
					player.noFallDmg = true;
				}
				if(!MPlayer.flyChannel)
					projectile.Kill(); //Kill projectile once player stops channeling
			}
			
			//Placement of the projectile and direction it is facing
			if (projectile.velocity.X >= 1f)
			{
				//Look to the right
				projectile.spriteDirection = (projectile.direction = -1);
				projectile.position.X = player.Center.X + 48;
				projectile.position.Y = player.Center.Y;
			}
			else if (projectile.velocity.X <= 1f)
			{
				//Look to the left
				projectile.spriteDirection = (projectile.direction = 1);
				projectile.position.X = player.Center.X - 64;
				projectile.position.Y = player.Center.Y;
			}
			projectile.rotation = projectile.velocity.X * 0.01f;
		}
		
		//Really just in case, not sure if really necessary
		public override bool? CanUseGrapple(Player player)
		{
			return true;
		}
		public override bool? SingleGrappleHook(Player player)
		{
			return true;
		}
		
		//Get rid of chain from aiStyle 7
		public override bool PreDrawExtras(SpriteBatch spriteBatch)
		{
			return false;
		}
	}
}