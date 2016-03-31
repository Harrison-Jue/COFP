using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Magic
{
	public class RainbowDevastationVortex : ModProjectile
	{
		//Dusts to spawn
		private int[] dusts = {60, 6, 64, 61, 59, 62, 109}; 
		
		public override void SetDefaults()
		{
			projectile.name = "Rainbow Devastation Vortex";
			projectile.width = 52;
			projectile.height = 52;
			projectile.scale = 1f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 999999;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.penetrate = -1;
		}
		public override void AI()
        {
			//Player variable
			Player p = Main.player[projectile.owner];
			
			//Setting up to update on the net
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
			
			//Setting the center of the projectile to the mouse cursor
			Vector2 vector14;
			vector14.X = (float)Main.mouseX + Main.screenPosition.X;
			if (p.gravDir == 1f)
			{
				vector14.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)p.height;
			}
			else
			{
				vector14.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
			}
			projectile.position = new Vector2(vector14.X - projectile.width/2, vector14.Y + projectile.height/2);
			
			//Rotating the Vortex
			projectile.rotation += 0.3f;
			
			//Spawning dust
			int rand = Main.rand.Next(0, 6);
			int DustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dusts[rand], projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 1f);
			Main.dust[DustID].noGravity = true;
			for(int i = 0; i < 3; i++)
			{
				DustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 109, projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 1f);
				Main.dust[DustID].noGravity = true;	
			}
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Trail for projectile
			if(projectile.ai[0] % 2 == 0)
			{
				Color color21 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
				SpriteEffects effects = SpriteEffects.None;
				int num98 = 0;
				int num99 = 0;
				float num100 = (float)(Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + (float)projectile.width * 0.5f;
				for(int num126 = 0; num126 < 10; num126++)
				{
					Color alpha4 = projectile.GetAlpha(color21);
					float num127 = (float)(9 - num126) / 9f;
					alpha4.R = (byte)((float)alpha4.R * num127);
					alpha4.G = (byte)((float)alpha4.G * num127);
					alpha4.B = (byte)((float)alpha4.B * num127);
					alpha4.A = (byte)((float)alpha4.A * num127);
					float num128 = (float)(9 - num126) / 9f;
					Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.oldPos[num126].X - Main.screenPosition.X + num100 + (float)num99, projectile.oldPos[num126].Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), alpha4, projectile.rotation, new Vector2(num100, (float)(projectile.height / 2 + num98)), num128 * projectile.scale, effects, 0f);
				}
			}
        }
	}
}