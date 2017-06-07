using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Ranged.AncientBow
{
	public class RainOfArrows : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 31;
			projectile.timeLeft = 181;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Arrow");
		}
		
		public override void AI()
		{
			if ((int)projectile.timeLeft % 5 == 0 && projectile.owner == Main.myPlayer && Main.netMode != 1)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0 + MathHelper.Lerp(-2f, 2f, (float)Main.rand.NextDouble()), 1, mod.ProjectileType("AncientArrows"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			}
		}
	}
}