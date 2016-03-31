using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Magic
{
	public class RainbowBall : ModProjectile
	{
		//Dusts to be spawned
		private int[] dusts = {60, 6, 64, 61, 59, 62};
		
		public override void SetDefaults()
		{
			projectile.name = "Rainbow Ball";
			projectile.width = 28;
			projectile.height = 28;
			projectile.scale = 0.5f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.penetrate = 1;
			projectile.extraUpdates = 4;
		}
		public override void AI()
        {
			//The counter, updating
			projectile.ai[0] += 1f;

			//Activate this set of code when 1 second passes
			if(projectile.ai[0] > 60f)
			{
				for(int i = 0; i < 1000; i++)
				{
					if(Main.projectile[i].type == mod.ProjectileType("RainbowDevastationVortex"))
					{
						//Going towards the vortex
						Projectile vortex = Main.projectile[i];
						float shootToX = vortex.position.X + (float)vortex.width * 0.5f - projectile.Center.X; 
						float shootToY = vortex.position.Y - projectile.Center.Y; 
						float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
						
						if(distance > 100f)
						{
							projectile.extraUpdates = 8;
						}
						
						distance = 4f / distance; 
						shootToX *= distance; 
						shootToY *= distance; 
						projectile.velocity.X = shootToX;
						projectile.velocity.Y = shootToY;
						
						//Get the rectangles or hitboxes of the projectiles
						Rectangle MB = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
						Rectangle NB = new Rectangle((int)vortex.position.X + (int)vortex.velocity.X, (int)vortex.position.Y + (int)vortex.velocity.Y, vortex.width, vortex.height); 
						
						//If they intersect kill the ball
						if (MB.Intersects(NB))
						{
							projectile.Kill();
						}
					}
				}
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if(MMod.tileDestruction)
			{
				//Destroy Tiles on Collied
				int num101 = (int)(projectile.position.X / 16f);
				int num102 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
				int num103 = (int)(projectile.position.Y / 16f);
				int num104 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
				if (num101 < 0)
				{
					num101 = 0;
				}
				if (num102 > Main.maxTilesX)
				{
					num102 = Main.maxTilesX;
				}
				if (num103 < 0)
				{
					num103 = 0;
				}
				if (num104 > Main.maxTilesY)
				{
					num104 = Main.maxTilesY;
				}
				for (int num105 = num101; num105 < num102; num105++)
				{
					for (int num106 = num103; num106 < num104; num106++)
					{
						WorldGen.KillTile(num105, num106);
					}
				}
			}
			
			//Destroy the projectile
			return true;
		}
		
		//Makes the projectile not be drawn, yet
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return false;
		}
		
		//Affect the drawing of the projectile
		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if(projectile.ai[0] > 20f)
			{
				//Dust for the Rainbow Ball
				int rand = Main.rand.Next(0, 6);
				int DustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, dusts[rand], projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 1f);
				Main.dust[DustID].noGravity = true;
				
				//Rainbow Ball Drawing
				Color color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
				SpriteEffects effects = SpriteEffects.None;
				int num98 = 0;
				int num99 = 0;
				float num100 = (float)(Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + (float)projectile.width * 0.5f;
				Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + num100 + (float)num99, projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), color, projectile.rotation, new Vector2(num100, (float)(projectile.height / 2 + num98)), projectile.scale * 0.5f, effects, 0f);
				
				//Trail of rainbow balls
				if(projectile.ai[0] % 2 == 0)
				{
					Color color21 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
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
}