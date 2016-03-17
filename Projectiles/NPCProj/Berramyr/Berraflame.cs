using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berraflame : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Berraflame";
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 30;
			projectile.penetrate = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 2;
		}
		public override void AI()
		{
			if(projectile.ai[0] > 4f)
			{
				for(int i = 0; i < 2; i++)
				{
					int DustID = Dust.NewDust(projectile.position, projectile.width + 2, projectile.height + 2, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
					Main.dust[DustID].noGravity = true;
					Main.dust[DustID].scale *= 1.75f;
					
					//Accessory dust to round out the dust
					Dust Dust1 = Main.dust[DustID];
					Dust1.velocity.X = Dust1.velocity.X * 2f;
					Dust Dust2 = Main.dust[DustID];
					Dust2.velocity.Y = Dust2.velocity.Y * 2f;
					
					if(projectile.timeLeft == 20)
					{
						Main.dust[DustID].active = false;
					}
				}
			}
			projectile.ai[0] += 1f;
		}
	}
}