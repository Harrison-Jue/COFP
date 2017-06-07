using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berrabeam : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 180;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 5;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berrabeam");
		}
		
		public override void AI()
		{
			if(projectile.ai[0] > 4f)
			{
				for(int i = 0; i < 4; i++)
				{
					int DustID = Dust.NewDust(projectile.position, projectile.width + 2, projectile.height + 2, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					Main.dust[DustID].noGravity = true;
					Main.dust[DustID].scale *= 1.75f;
				}
			}
			projectile.ai[0] += 1f;
		}
	}
}