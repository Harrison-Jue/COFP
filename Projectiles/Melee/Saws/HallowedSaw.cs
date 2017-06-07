using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Melee.Saws
{	
	public class HallowedSaw : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.scale = 1.1f;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.melee = true;
			projectile.penetrate = 30;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Saw");
		}
		
		public override void AI()
        {
			projectile.light = 0.4f;
			int DustID1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 57, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 57, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 57, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.75f);
			Main.dust[DustID1].noGravity = true;
			Main.dust[DustID2].noGravity = true;
			Main.dust[DustID3].noGravity = true;
			projectile.rotation += (float)projectile.direction * 0.8f;
			
			//This whole if statement is for projectile control
			if (Main.myPlayer == projectile.owner && projectile.ai[0] == 0f)
			{
				projectile.rotation += (float)projectile.direction * 0.8f;
				if (Main.player[projectile.owner].channel)
				{
					projectile.rotation += (float)projectile.direction * 0.8f;
					float num146 = 12f;
					Vector2 vector10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					projectile.rotation += (float)projectile.direction * 0.8f;
					float num147 = (float)Main.mouseX + Main.screenPosition.X - vector10.X;
					projectile.rotation += (float)projectile.direction * 0.8f;
					float num148 = (float)Main.mouseY + Main.screenPosition.Y - vector10.Y;
					projectile.rotation += (float)projectile.direction * 0.8f;
					if (Main.player[projectile.owner].gravDir == -1f)
					{
						num148 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector10.Y;
						projectile.rotation += (float)projectile.direction * 0.8f;
					}
					float num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
					num149 = (float)Math.Sqrt((double)(num147 * num147 + num148 * num148));
					if (num149 > num146)
					{
						projectile.rotation += (float)projectile.direction * 0.8f;
						num149 = num146 / num149;
						num147 *= num149;
						num148 *= num149;
						int num150 = (int)(num147 * 1000f);
						int num151 = (int)(projectile.velocity.X * 1000f);
						projectile.rotation += (float)projectile.direction * 0.8f;
						int num152 = (int)(num148 * 1000f);
						int num153 = (int)(projectile.velocity.Y * 1000f);
						projectile.rotation += (float)projectile.direction * 0.8f;
						if (num150 != num151 || num152 != num153)
						{
							projectile.rotation += (float)projectile.direction * 0.8f;
							projectile.netUpdate = true;
							projectile.rotation += (float)projectile.direction * 0.8f;
						}
						projectile.velocity.X = num147;
						projectile.rotation += (float)projectile.direction * 0.8f;
						projectile.velocity.Y = num148;
						projectile.rotation += (float)projectile.direction * 0.8f;
					}
					else
					{
						projectile.rotation += (float)projectile.direction * 0.8f;
						int num154 = (int)(num147 * 1000f);
						int num155 = (int)(projectile.velocity.X * 1000f);
						projectile.rotation += (float)projectile.direction * 0.8f;
						int num156 = (int)(num148 * 1000f);
						int num157 = (int)(projectile.velocity.Y * 1000f);
						projectile.rotation += (float)projectile.direction * 0.8f;
						if (num154 != num155 || num156 != num157)
						{
							projectile.rotation += (float)projectile.direction * 0.8f;
							projectile.netUpdate = true;
							projectile.rotation += (float)projectile.direction * 0.8f;
						}
						projectile.velocity.X = num147;
						projectile.rotation += (float)projectile.direction * 0.8f;
						projectile.velocity.Y = num148;
						projectile.rotation += (float)projectile.direction * 0.8f;
					}
					projectile.rotation += (float)projectile.direction * 0.8f;
				}
				else
				{
					projectile.rotation += (float)projectile.direction * 0.8f;
					if (projectile.ai[0] == 0f)
					{
						projectile.ai[0] = 1f;
						projectile.rotation += (float)projectile.direction * 0.8f;
						projectile.netUpdate = true;
						projectile.rotation += (float)projectile.direction * 0.8f;
						float num158 = 12f;
						Vector2 vector11 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
						float num159 = (float)Main.mouseX + Main.screenPosition.X - vector11.X;
						projectile.rotation += (float)projectile.direction * 0.8f;
						float num160 = (float)Main.mouseY + Main.screenPosition.Y - vector11.Y;
						projectile.rotation += (float)projectile.direction * 0.8f;
						if (Main.player[projectile.owner].gravDir == -1f)
						{
							num160 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector11.Y;
							projectile.rotation += (float)projectile.direction * 0.8f;
						}
						float num161 = (float)Math.Sqrt((double)(num159 * num159 + num160 * num160));
						if (num161 == 0f)
						{
							vector11 = new Vector2(Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2), Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2));
							projectile.rotation += (float)projectile.direction * 0.8f;
							num159 = projectile.position.X + (float)projectile.width * 0.5f - vector11.X;
							projectile.rotation += (float)projectile.direction * 0.8f;
							num160 = projectile.position.Y + (float)projectile.height * 0.5f - vector11.Y;
							projectile.rotation += (float)projectile.direction * 0.8f;
							num161 = (float)Math.Sqrt((double)(num159 * num159 + num160 * num160));
						}
						num161 = num158 / num161;
						num159 *= num161;
						num160 *= num161;
						projectile.velocity.X = num159;
						projectile.rotation += (float)projectile.direction * 0.8f;
						projectile.velocity.Y = num160;
						projectile.rotation += (float)projectile.direction * 0.8f;
						if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
						{
							projectile.Kill();
						}
					}
					projectile.rotation += (float)projectile.direction * 0.8f;
				}
				projectile.rotation += (float)projectile.direction * 0.8f;
			}
			projectile.rotation += (float)projectile.direction * 0.8f;
			if (projectile.velocity.X != 0f || projectile.velocity.Y != 0f)
			{
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - 2.355f;
				projectile.rotation += (float)projectile.direction * 0.8f;
			}
			projectile.rotation += (float)projectile.direction * 0.8f;
			if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
				projectile.rotation += (float)projectile.direction * 0.8f;
			}
			if(projectile.timeLeft % 60 == 0)
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 23);
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			//Give npc random debuff from slow (which really does nothing), burning and ichor
			int rand = Main.rand.Next(3);
			if (rand == 0) 
			{ 
				n.AddBuff(39, 300); 
			}
			else if (rand == 1) 
			{ 
				n.AddBuff(69, 300); 
			}
			else if (rand == 2) 
			{ 
				n.AddBuff(32, 300); 
			}
		}
		
		//Bouncing off tiles
		public override bool OnTileCollide(Vector2 velocityChange) 
		{
			if (projectile.velocity.X != velocityChange.X) 
			{
				projectile.velocity.X = -velocityChange.X; 
			}
			if (projectile.velocity.Y != velocityChange.Y) 
			{ 
				projectile.velocity.Y = -velocityChange.Y; 
			}
			projectile.penetrate -= 1; //Decrease penetration after tile collision
			return false; //Don't destroy projectile on tile collide
		}
	}
}