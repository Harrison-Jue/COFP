using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Magic
{
	public class RainbowDevastationRod : ModProjectile
	{
		//Dusts to spawn
		private int[] dusts = {60, 6, 64, 61, 59, 62}; 
		
		public override void SetDefaults()
		{
			projectile.width = 74;
			projectile.height = 78;
			projectile.scale = 1f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 999999;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 7;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rainbow Devastation Rod");
		}
		
		public override void AI()
        {
			//Settings for updating on net
			Vector2 vector22 = Main.player[projectile.owner].RotatedRelativePoint(Main.player[projectile.owner].MountedCenter, true);
			if (Main.myPlayer == projectile.owner)
			{
				if (Main.player[projectile.owner].channel)
				{
					float num263 = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shootSpeed * projectile.scale;
					Vector2 vector23 = vector22;
					float num264 = (float)Main.mouseX + Main.screenPosition.X - vector23.X;
					float num265 = (float)Main.mouseY + Main.screenPosition.Y - vector23.Y;
					if (Main.player[projectile.owner].gravDir == -1f)
					{
						num265 = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector23.Y;
					}
					float num266 = (float)Math.Sqrt((double)(num264 * num264 + num265 * num265));
					num266 = (float)Math.Sqrt((double)(num264 * num264 + num265 * num265));
					num266 = num263 / num266;
					num264 *= num266;
					num265 *= num266;
					if (num264 != projectile.velocity.X || num265 != projectile.velocity.Y)
					{
						projectile.netUpdate = true;
					}
					projectile.velocity.X = num264;
					projectile.velocity.Y = num265;
				}
				else
				{
					projectile.Kill();
				}
			}
			
			//Setting the position and direction
			if (projectile.velocity.X > 0f)
			{
				Main.player[projectile.owner].ChangeDir(1);
			}
			else
			{
				if (projectile.velocity.X < 0f)
				{
					Main.player[projectile.owner].ChangeDir(-1);
				}
			}
			projectile.spriteDirection = projectile.direction;
			Main.player[projectile.owner].ChangeDir(projectile.direction);
			Main.player[projectile.owner].heldProj = projectile.whoAmI;
			projectile.position.X = vector22.X - (float)(projectile.width / 2);
			projectile.position.Y = vector22.Y - (float)(projectile.height / 2);
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
			
			//Rotation of Projectile and Item Use
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= 1.57f;
			}
			if (Main.player[projectile.owner].direction == 1)
			{
				Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
			}
			else
			{
				Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
			}
			
			//Make velocity really low towards mouse
			projectile.velocity.X = projectile.velocity.X * (1f + (float)Main.rand.Next(-3, 4) * 0.01f);
			
			//Animation and firing in terms of frameCounter and first counter
			if(projectile.frameCounter % (int)projectile.ai[0] == 0 )
			{
				//Animation
				if(projectile.frame < 6)
				{
					projectile.frame += 1;
				}	
				else
				{
					projectile.frame = 0;
				}
				
				//Spawning a random colored dust
				int rand = Main.rand.Next(0, 6);
				for(int i = 0; i < 3; i++)
				{
					int DustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dusts[rand], projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 1f);
					Main.dust[DustID].noGravity = true;
				}
				
				//Some light
				projectile.light = 0.7f;
				
				//Firing towards the mouse
				Vector2 vector = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float MouseX = (float)Main.mouseX + Main.screenPosition.X - vector.X;
				float MouseY = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
				float MouseDistance = (float)Math.Sqrt((double)(MouseX * MouseX + MouseY * MouseY));
				float shootSpeed = 4f;
				MouseDistance = shootSpeed / MouseDistance;
				MouseX *= MouseDistance;
				MouseY *= MouseDistance;
				Projectile.NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, MouseX + (float)Main.rand.Next(-2, 0), MouseY + (float)Main.rand.Next(-2, 2), mod.ProjectileType("RainbowBall"), (int)(180f * Main.player[projectile.owner].magicDamage), projectile.knockBack, projectile.owner, 0f, 0f);
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);
			}
			projectile.frameCounter++;
			
			//Increases rate of animation/fire rate
			if(projectile.ai[1] > 60f)
			{
				if(projectile.ai[0] > 3f)
				{
					//Decreasing first counter; First counter is set in the Item
					projectile.ai[0] -= 3f; 
				}
				projectile.ai[1] = 0;
			}
			
			//Update secondary counter
			projectile.ai[1] += 1f;
			
			//Reset how long the projectile lives
			Main.player[projectile.owner].itemTime = 10;
			Main.player[projectile.owner].itemAnimation = 10;
			projectile.timeLeft = 999999;
		}
	}
}