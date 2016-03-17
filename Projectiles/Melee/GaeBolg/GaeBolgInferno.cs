using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Melee.GaeBolg
{
	public class GaeBolgInferno : ModProjectile
	{	
		public override void SetDefaults()
		{
			projectile.name = "Gae Bolg";
			projectile.width = 64;
			projectile.height = 64;
			projectile.aiStyle = 50;
			projectile.timeLeft = 180;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.melee = true;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			//Spawns barbs every 6 ticks
			projectile.ai[1] -= 1f;
			if ((int)projectile.ai[1] % 6 == 0 && projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0 + MathHelper.Lerp(-4f, 4f, (float)Main.rand.NextDouble()), 0 + MathHelper.Lerp(-4f, 4f, (float)Main.rand.NextDouble()), mod.ProjectileType("GaeBolgBarb"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			}
		}
	}
}