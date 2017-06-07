using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.BomberPlane
{
	public class BomberPlane : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 135;
			projectile.height = 34;
			projectile.scale = 0.5f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 2;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bomber Plane");
		}
		
		public override void AI()
		{
			//Set direction of the projectile
			if (projectile.velocity.X > 0f)
			{
				projectile.spriteDirection = (projectile.direction = 1);
			}
			else
			{
				if (projectile.velocity.X < 0f)
				{
					projectile.spriteDirection = (projectile.direction = -1);
				}
			}
			
			projectile.light = 0.9f;
			
			//AI incrementation
			projectile.ai[1] = 0; //Don't know why I have it, will keep here anyways
			projectile.ai[0]++;
			
			//If it reaches 20 ticks, spawn a bomb
			if(projectile.ai[0] > 20 && Main.netMode != 1)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, 8, mod.ProjectileType("BomberPlaneBomb"), projectile.damage, 0, Main.myPlayer, 0f, 0f);
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 11);
				projectile.ai[0] = 0;
			}
			
			//Projectile animation!
			if(projectile.frameCounter < 5)
				projectile.frame = 0;
			else if(projectile.frameCounter >= 5 && projectile.frameCounter < 10)
				projectile.frame = 1;
			else
				projectile.frameCounter = 0;
			projectile.frameCounter++;
		}
	}
}