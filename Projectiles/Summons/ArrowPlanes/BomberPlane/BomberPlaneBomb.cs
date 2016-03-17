using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.BomberPlane
{
	public class BomberPlaneBomb : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Bomber Plane";
			projectile.width = 4;
			projectile.height = 4;
			projectile.scale = 0.5f;
			projectile.aiStyle = 1;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.minion = true;
		}
		public override void AI()
		{
			projectile.light = 2f;
		}
		public override void Kill(int timeLeft)
		{
			if(Main.netMode != 1)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("BomberPlaneBombExplosion"), projectile.damage, 0, Main.myPlayer);
			}
		}
	}
}