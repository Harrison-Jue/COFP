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