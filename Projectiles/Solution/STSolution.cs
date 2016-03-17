using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Solution
{
	public class STSolution : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "STSolution";
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
			projectile.ai[1] = (float) mod.BuffType("STSprayed");
			MProjectile.MidasAI(projectile, 9, 168, 11, 83);
		}
	}
}