using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Barrier
{
	public class ShadowBloodVeil : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 250;
			projectile.height = 250;
			projectile.timeLeft = 999999;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Blood Veil");
		}
		
		public override void AI()
		{
			projectile.light = 0.9f;
            Player owner = Main.player[projectile.owner];
            projectile.alpha = 255;
			
			//Plays a sound when the projectile spawns
			if (projectile.localAI[0] == 0f)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				projectile.localAI[0] += 1f;
			}
			
			projectile.ai[0] += 1f;
			float num578 = 25f;
			if (projectile.ai[0] > 130f)
			{
				num578 -= (projectile.ai[0] - 130f) / 2f;
			}
			if (num578 <= 0f)
			{
				num578 = 0f;
				projectile.Kill();
			}
			int num579 = 0;
			
			//The inferno code, with dust int values 
			while ((float)num579 < num578)
			{
				float num580 = (float)Main.rand.Next(-10, 11);
				float num581 = (float)Main.rand.Next(-10, 11);
				float num582 = (float)Main.rand.Next(3, 9);
				float num583 = (float)Math.Sqrt((double)(num580 * num580 + num581 * num581));
				num583 = num582 / num583;
				num580 *= num583;
				num581 *= num583;
				int DustID1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 5, 0f, 0f, 200, default(Color), 1.5f);
				Main.dust[DustID1].noGravity = true;
				Main.dust[DustID1].position.X = projectile.Center.X;
				Main.dust[DustID1].position.Y = projectile.Center.Y;
				Dust Dust1 = Main.dust[DustID1];
				Dust1.position.X = Dust1.position.X + (float)Main.rand.Next(-10, 11);
				Dust Dust2 = Main.dust[DustID1];
				Dust2.position.Y = Dust2.position.Y + (float)Main.rand.Next(-10, 11);
				Main.dust[DustID1].velocity.X = num580;
				Main.dust[DustID1].velocity.Y = num581;
				int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 54, 0f, 0f, 200, default(Color), 1.5f);
				Main.dust[DustID2].noGravity = true;
				Main.dust[DustID2].position.X = projectile.Center.X;
				Main.dust[DustID2].position.Y = projectile.Center.Y;
				Dust Dust3 = Main.dust[DustID2];
				Dust3.position.X = Dust3.position.X + (float)Main.rand.Next(-10, 11);
				Dust Dust4 = Main.dust[DustID2];
				Dust4.position.Y = Dust4.position.Y + (float)Main.rand.Next(-10, 11);
				Main.dust[DustID2].velocity.X = num580;
				Main.dust[DustID2].velocity.Y = num581;
				
				num579++;
			}
			
			//Adjusting the position of the projectile so that its center is at the player's center
            projectile.position.X = owner.Center.X - 125; 
            projectile.position.Y = owner.Center.Y - 125;
			
			//The damaging and healing thing every 5 ticks
			if(projectile.timeLeft % 5 == 0)
			{
				//For every npc in the array of npcs
				foreach (NPC N in Main.npc)
				{
					//Find the rectangles or "hitboxes" of the npc and projectile
					Rectangle MB = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
					Rectangle NB = new Rectangle((int)N.position.X, (int)N.position.Y, N.width, N.height); 
					
					//If the two cross together
					if (MB.Intersects(NB) && N.life > 0 && N.active &&!N.friendly)
					{
						N.life -= 1; //npc loses health
						N.StrikeNPC(1, 0f, N.direction, false, false, false); //Shows the damage on the npc
						int rand = Main.rand.Next(2); //RNG
						if(rand == 0 && Main.netMode != 1)
						{
							//Spawning our healing projectile
							Projectile.NewProjectile(N.Center.X, N.Center.Y, 0, 0, mod.ProjectileType("LifeBlood"), 0, 0, Main.myPlayer, 0f, 1f); 
						}
					}
				}
			}
			
			//If the player is not active or alive
			if (owner == null || !owner.active || owner.dead) { projectile.Kill();}
			
			//Check if the player has the buff for the projectile, if not kill it
			if (Main.myPlayer == owner.whoAmI) 
			{
				int id = owner.FindBuffIndex(mod.BuffType("ShadowBloodVeil"));
				
				//-1 is false and I beleive 1 is true
				if(id == -1)
				{ 
					projectile.Kill();
				} 
			}
		}
		
		public override void Kill(int timeLeft)
		{
			//Player variable
			Player owner = Main.player[projectile.owner];
			if (Main.myPlayer == owner.whoAmI) 
			{
				int id = owner.FindBuffIndex(mod.BuffType("ShadowBloodVeil"));
				if(id == -1)
				{ 
					//Does nothing
					return;
				} 
				else
				{
					//Spawns a new projectile of the same type
					if(Main.netMode != 1)
					{
						Projectile.NewProjectile(owner.Center.X, owner.Center.Y, 0, 0, mod.ProjectileType("ShadowBloodVeil"), 0, 0, Main.myPlayer);
					}
				}
			}
		}
	}
}