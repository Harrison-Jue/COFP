using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Pets
{
	public class PDTBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Point Defense Turret";
			projectile.width = 20;
			projectile.height = 20;
			projectile.scale = 1f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 180;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.extraUpdates = 5;
		}
		public override void AI()
		{
			projectile.ai[0] += 1f;
			
			//Set visibility depending on time spawned
			if(projectile.ai[0] < 6f)
			{
				projectile.alpha = 255; //Set invisible 
			}
			else
			{
				projectile.alpha = 0; //Set visible
			}
			
			//Rotate the projectile
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			
			//Checks all projectiles
			for(int i = 0; i < 1000; i++)
			{
				Projectile enemyProj = Main.projectile[i];
				
				//If the projectile is hostile
				if(enemyProj.hostile)
				{
					float shootToX = enemyProj.position.X + (float)enemyProj.width * 0.5f - projectile.Center.X; 
					float shootToY = enemyProj.position.Y - projectile.Center.Y; 
					float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
					if(distance < 480f && enemyProj.hostile && enemyProj.active)
					{
						distance = 3f / distance; 
						shootToX *= distance * 5; 
						shootToY *= distance * 5; 
						projectile.velocity.X = shootToX;
						projectile.velocity.Y = shootToY;
					}	
					
					//Get the rectangles or hitboxes of the projectiles
					Rectangle MB = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
					Rectangle NB = new Rectangle((int)enemyProj.position.X + (int)enemyProj.velocity.X, (int)enemyProj.position.Y + (int)enemyProj.velocity.Y, enemyProj.width, enemyProj.height); 
					
					//If they intersect kill both of them
					if (MB.Intersects(NB))
					{
						enemyProj.Kill();
						projectile.Kill();
					}
				}
			}
		}
	}
}