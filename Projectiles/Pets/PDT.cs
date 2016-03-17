using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Pets
{
	public class PDT : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Point Defense Turret";
			projectile.width = 30;
			projectile.height = 50;
			projectile.scale = 0.5f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 999999;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.minion = true;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.ai[0] += 1f;
			
			//Setting the position
			if(player.direction == 1)
			{
				projectile.position = new Vector2(player.position.X - 28, player.position.Y - 24);
			}
			else
			{
				projectile.position = new Vector2(player.position.X + 24, player.position.Y - 24);
			}
			
			//Finding a projectile
			for(int i = 0; i < 1000; i++)
			{
				//Enemy Projectile variable being set
				Projectile enemyProj = Main.projectile[i];
				float shootToX = enemyProj.position.X + (float)enemyProj.width * 0.5f - projectile.Center.X; 
				float shootToY = enemyProj.position.Y - projectile.Center.Y; 
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				if(distance < 480f && enemyProj.hostile && enemyProj.active)
				{
					//Setting the projectile's rotation to the projectile
					Vector2 shootAngle = enemyProj.Center - projectile.Center;
					float angle = (float)Math.Atan2(shootAngle.Y, shootAngle.X);
					projectile.rotation = angle;
					
					if(projectile.ai[0] > 4f && Main.netMode != 1)
					{
						distance = 3f / distance; 
						shootToX *= distance * 5; 
						shootToY *= distance * 5; 
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, mod.ProjectileType("PDTBullet"), 0, 0, Main.myPlayer, 0f, 0f);
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 11);
						projectile.ai[0] = 0f;
					}
				}
			}
			
			if(!player.active || player.dead || player.HasBuff(mod.BuffType("PDT")) == -1)
			{
				projectile.Kill();
			}
		}
	}
}