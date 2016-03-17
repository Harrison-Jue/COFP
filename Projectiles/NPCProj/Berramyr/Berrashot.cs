using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berrashot : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Berrashot";
			projectile.width = 30;
			projectile.height = 30;
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