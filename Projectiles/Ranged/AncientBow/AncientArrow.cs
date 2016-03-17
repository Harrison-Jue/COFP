using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Ranged.AncientBow
{
	public class AncientArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Ancient Arrow";
			projectile.width = 14;
			projectile.height = 32;
			projectile.scale = 0.75f;
			projectile.aiStyle = 1;
			projectile.timeLeft = 60;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.ranged = true;
			projectile.extraUpdates = 1;
			projectile.penetrate = -1;
		}
		public override void AI()
        {
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 24, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 0.25f);
			projectile.light = 0.9f;
		}
		public override void Kill(int timeLeft)
        {
			if(Main.netMode != 1)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("RainOfArrows"), projectile.damage, 0, Main.myPlayer, 0f, 0f);
			}
		}
	}
}