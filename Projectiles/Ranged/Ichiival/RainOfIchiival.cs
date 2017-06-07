using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Ranged.Ichiival
{
	public class RainOfIchiival : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = 0;
			projectile.timeLeft = 180;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichiival");
		}
		
		public override void AI()
		{
			projectile.light = 0.9f;
			projectile.ai[1] -= 1f;
			if ((int)projectile.ai[1] % 8 == 0 && projectile.owner == Main.myPlayer && Main.netMode != 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 12);
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0 + MathHelper.Lerp(-2f, 2f, (float)Main.rand.NextDouble()), 16, mod.ProjectileType("RainOfIchiivalBolts"), projectile.damage, 2, projectile.owner);
			}
		}
	}
}