using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berraworks : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Berraworks";
			projectile.width = 12;
			projectile.height = 12;
			projectile.scale = 2;
			projectile.timeLeft = 30;
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
		public override void Kill(int timeLeft)
		{
			for(int i = 0; i <= 360; i += 10)
			{
				float rad = i * (float)(Math.PI/180);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)Math.Cos(rad) * 10, (float)Math.Sin(rad) * 10, mod.ProjectileType("Berrabullet"), 10, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}