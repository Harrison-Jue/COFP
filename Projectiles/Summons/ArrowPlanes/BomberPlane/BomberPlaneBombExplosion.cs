using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.BomberPlane
{	
	public class BomberPlaneBombExplosion : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Bomber Plane";
			projectile.width = 48;
			projectile.height = 48;
			projectile.scale = 0.5f;
			projectile.aiStyle = 49;
			projectile.timeLeft = 60;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.minion = true;
			projectile.penetrate = -1;
		}
		public override void AI()
        {
			projectile.light = 0.9f;
		}
	}
}