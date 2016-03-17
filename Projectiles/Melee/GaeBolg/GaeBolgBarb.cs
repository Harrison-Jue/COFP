using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Melee.GaeBolg
{
	public class GaeBolgBarb : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Gae Bolg";
			projectile.width = 10;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.melee = true;
		}
		public override void AI()
        {
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 24, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 0.25f);
			projectile.light = 0.9f;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			projectile.position.X = n.Center.X;
			projectile.position.Y = n.Center.Y;
			projectile.velocity.X = n.velocity.X;
			projectile.velocity.Y = n.velocity.Y;
		}
		public override void Kill(int timeLeft)
		{
			int rand = Main.rand.Next(4);
			if(rand == 0)
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
		}
	}
}