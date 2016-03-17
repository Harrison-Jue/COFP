using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berrabullet : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Berrabullet";
			projectile.width = 12;
			projectile.height = 12;
			projectile.scale = 2;
			projectile.timeLeft = 90;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
		{
			if(projectile.ai[0] > 4f)
			{
				projectile.alpha = 0;
			}
			else
			{
				projectile.alpha = 255;
			}
			projectile.ai[0] += 1f;
		}
	}
}