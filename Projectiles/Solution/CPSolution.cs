using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Solution
{
	public class CPSolution : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "CPSolution";
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
			projectile.ai[1] = (float) mod.BuffType("CPSprayed");
			MProjectile.MidasAI(projectile, 107, 221, 48, 167);
		}
	}
}