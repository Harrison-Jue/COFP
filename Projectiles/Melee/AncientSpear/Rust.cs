using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Melee.AncientSpear
{
	public class Rust : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.melee = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Spear");
		}
		
		public override void AI()
        {
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 24, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 0.25f);
			projectile.light = 0.9f;
		}
	}
}