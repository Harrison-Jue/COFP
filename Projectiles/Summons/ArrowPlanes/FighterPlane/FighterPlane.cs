using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.FighterPlane
{
	public class FighterPlane : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Fighter Plane";
			projectile.width = 66;
			projectile.height = 24;
			projectile.scale = 0.5f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 300;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
			projectile.minion = true;
			projectile.penetrate = -1;
			Main.projFrames[projectile.type] = 3;
		}
		public override void AI()
		{
			//Homing onto an npc
			float num486 = projectile.position.X;
			float num487 = projectile.position.Y;
			float num488 = 100000f;
			bool flag17 = false;
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 30f)
			{
				projectile.ai[0] = 30f;
				
				//Finding npc to home to
				for (int num489 = 0; num489 < 200; num489++)
				{
					if (Main.npc[num489].active && !Main.npc[num489].dontTakeDamage && !Main.npc[num489].friendly && Main.npc[num489].lifeMax > 5)
					{
						float num490 = Main.npc[num489].position.X + (float)(Main.npc[num489].width / 2);
						float num491 = Main.npc[num489].position.Y + (float)(Main.npc[num489].height / 2);
						float num492 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num490) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num491);
						if (num492 < 800f && num492 < num488 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num489].position, Main.npc[num489].width, Main.npc[num489].height))
						{
							num488 = num492;
							num486 = num490;
							num487 = num491;
							flag17 = true;
						}
					}
				}
			}
			if (!flag17)
			{
				num486 = projectile.position.X + (float)(projectile.width / 2) + projectile.velocity.X * 100f;
				num487 = projectile.position.Y + (float)(projectile.height / 2) + projectile.velocity.Y * 100f;
			}
			float num493 = 6f;
			float num494 = 0.1f;
			Vector2 vector36 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
			float num495 = num486 - vector36.X;
			float num496 = num487 - vector36.Y;
			float num497 = (float)Math.Sqrt((double)(num495 * num495 + num496 * num496));
			num497 = num493 / num497;
			num495 *= num497;
			num496 *= num497;
			if (projectile.velocity.X < num495)
			{
				projectile.velocity.X = projectile.velocity.X + num494;
				if (projectile.velocity.X < 0f && num495 > 0f)
				{
					projectile.velocity.X = projectile.velocity.X + num494 * 2f;
				}
			}
			else
			{
				if (projectile.velocity.X > num495)
				{
					projectile.velocity.X = projectile.velocity.X - num494;
					if (projectile.velocity.X > 0f && num495 < 0f)
					{
						projectile.velocity.X = projectile.velocity.X - num494 * 2f;
					}
				}
			}
			if (projectile.velocity.Y < num496)
			{
				projectile.velocity.Y = projectile.velocity.Y + num494;
				if (projectile.velocity.Y < 0f && num496 > 0f)
				{
					projectile.velocity.Y = projectile.velocity.Y + num494 * 2f;
				}
			}
			else
			{
				if (projectile.velocity.Y > num496)
				{
					projectile.velocity.Y = projectile.velocity.Y - num494;
					if (projectile.velocity.Y > 0f && num496 < 0f)
					{
						projectile.velocity.Y = projectile.velocity.Y - num494 * 2f;
					}
				}
			}
			
			projectile.light = 0.9f;
			
			//Shooting at an npc
			bool flag28 = false;
			Vector2 vector53 = projectile.position;
			float num723 = 100f;
			
			//Getting the npc to fire at
			for (int num724 = 0; num724 < 200; num724++)
			{
				NPC npc = Main.npc[num724];
				Vector2 npcCenter = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
				if (npc.active && !npc.friendly && npc.lifeMax > 5)
				{
					float num725 = Vector2.Distance(npcCenter, projectile.Center);
					if (((Vector2.Distance(projectile.Center, vector53) > 50f && num725 < num723) || !flag28) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
					{
						num723 = num725;
						vector53 = npcCenter;
						flag28 = true;
					}
				}
			}
			
			//If the previous npc fits all requirements and is not blocked by a tile, shoot at the npc
			if (flag28)
			{
				if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height) && projectile.timeLeft % 15 == 0)
				{
					if (Main.myPlayer == projectile.owner && Main.netMode != 1)
					{
						int num739 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * 30, projectile.velocity.Y * 30, mod.ProjectileType("FighterPlaneBullet"), projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 11);
						Main.projectile[num739].netUpdate = true;
						projectile.netUpdate = true;
					}
				}
			}
			
			//Setting direction and rotation of the projectile
			if (projectile.velocity.X > 0f)
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			}
			else if (projectile.velocity.X < 0f)
			{
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			}
			
			//Projectile animation
			if(projectile.frameCounter < 5)
				projectile.frame = 0;
			else if(projectile.frameCounter >= 5 && projectile.frameCounter < 10)
				projectile.frame = 1;
			else if(projectile.frameCounter >= 10 && projectile.frameCounter < 15)
				projectile.frame = 2;
			else
				projectile.frameCounter = 0;
			projectile.frameCounter++;
		}
	}
}