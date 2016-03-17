using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Ranged.Ichiival
{
	public class RainOfIchiivalBolts : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Ichiival";
			projectile.width = 2;
			projectile.height = 42;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.extraUpdates = 1;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			projectile.light = 0.9f;
			int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.9f);
			Main.dust[DustID2].noGravity = true;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
		public override void Kill(int timeLeft)
        {
			if(Main.netMode != 1)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				for(int a = 0; a < 2; a++)
				{
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, MathHelper.Lerp(-4f, 4f, (float)Main.rand.NextDouble()) + MathHelper.Lerp(-4f, 0f, (float)Main.rand.NextDouble()), projectile.velocity.Y + MathHelper.Lerp(-16f, 16f, (float)Main.rand.NextDouble()), mod.ProjectileType("IchiivalFireball"), projectile.damage/4, 2, Main.myPlayer);
				}
			}
		}
	}
}