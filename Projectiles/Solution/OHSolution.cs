using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Solution
{
	public class OHSolution : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "OHSolution";
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 90;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 2;
		}
		public override void AI()
		{
			projectile.ai[1] = (float) mod.BuffType("OHSprayed");
			MProjectile.MidasAI(projectile, 56, 58, 224, 12);
		}
	}
}