using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Melee.AncientSpear
{
	public class MiniAncientSpear : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Ancient Spear";
			projectile.width = 10;
			projectile.height = 10;
			projectile.scale = 0.75f;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.melee = true;
			projectile.penetrate = -1;
		}
		public override void AI()
        {
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 24, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 0.4f);
			projectile.light = 0.9f;
			projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 30f)
			{
				projectile.ai[0] = 30f;
				projectile.velocity.Y = projectile.velocity.Y + 0.25f;
				if (projectile.velocity.Y > 16f)
				{
					projectile.velocity.Y = 16f;
				}
				projectile.velocity.X = projectile.velocity.X * 0.995f;
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.alpha -= 50;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			if (projectile.owner == Main.myPlayer)
			{
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] >= 4f)
				{
					projectile.localAI[0] = 0f;
					int num668 = 0;
					for (int num669 = 0; num669 < 1000; num669++)
					{
						if (Main.projectile[num669].active && Main.projectile[num669].owner == projectile.owner && Main.projectile[num669].type == 344)
						{
							num668++;
						}
					}
					float num670 = (float)projectile.damage * 0.8f;
					if (num668 > 100)
					{
						float num671 = (float)(num668 - 100);
						num671 = 1f - num671 / 100f;
						num670 *= num671;
					}
					if (num668 > 100)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num668 > 120)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num668 > 140)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num668 > 150)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num668 > 160)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num668 > 165)
					{
						projectile.localAI[0] -= 1f;
					}
					if (num668 > 170)
					{
						projectile.localAI[0] -= 2f;
					}
					if (num668 > 175)
					{
						projectile.localAI[0] -= 3f;
					}
					if (num668 > 180)
					{
						projectile.localAI[0] -= 4f;
					}
					if (num668 > 185)
					{
						projectile.localAI[0] -= 5f;
					}
					if (num668 > 190)
					{
						projectile.localAI[0] -= 6f;
					}
					if (num668 > 195)
					{
						projectile.localAI[0] -= 7f;
					}
					if (num670 > (float)projectile.damage * 0.1f)
					{
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Rust"),  projectile.damage/4, projectile.knockBack * 0.55f, projectile.owner, 0f, (float)Main.rand.Next(3));
					}
				}
			}
		}
	}
}