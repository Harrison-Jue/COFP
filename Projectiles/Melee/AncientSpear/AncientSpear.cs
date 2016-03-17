using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Melee.AncientSpear
{
	public class AncientSpear : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Ancient Spear";
			projectile.width = 20;
			projectile.height = 20;
			projectile.scale = 1.1f;
			projectile.aiStyle = 19;
			projectile.timeLeft = 90;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.ownerHitCheck = true;
			projectile.hide = true;
		}
		public override void AI()
        {
			projectile.light = 0.9f;
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 24, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 0.5f);
			
			//Spear code
			Main.player[projectile.owner].direction = projectile.direction;
			Main.player[projectile.owner].heldProj = projectile.whoAmI;
			Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
			projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
			projectile.position += projectile.velocity * projectile.ai[0];if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 3f;
				projectile.netUpdate = true;
			}
			if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
			{
				projectile.ai[0] -= 1.1f; //How far the spear goes back
				if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner)
				{
					projectile.localAI[0] = 1f;
					if (Collision.CanHit(Main.player[projectile.owner].position, Main.player[projectile.owner].width, Main.player[projectile.owner].height, new Vector2(projectile.Center.X + projectile.velocity.X * projectile.ai[0], projectile.Center.Y + projectile.velocity.Y * projectile.ai[0]), projectile.width, projectile.height))
					{
						Projectile.NewProjectile(projectile.Center.X + projectile.velocity.X , projectile.Center.Y + projectile.velocity.Y , projectile.velocity.X * 1.5f, projectile.velocity.Y * 1.5f, mod.ProjectileType("MiniAncientSpear"), projectile.damage , projectile.knockBack * 0.85f, projectile.owner, 0f, 0f);
					}
				}
			}
			else
			{
				projectile.ai[0] += 0.75f; //How far the spear goes
			}
			
			//Kill projectile if item is done being animated
			if (Main.player[projectile.owner].itemAnimation == 0)
			{
				projectile.Kill();
			}
			
			//Rotation of the spear
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= 1.57f;
			}
		}
	}
}