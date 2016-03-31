using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berraheal : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Berraheal";
			projectile.width = 30;
			projectile.height = 30;
			projectile.alpha = 255;
			projectile.timeLeft = 999999;
			projectile.penetrate = -1;
			projectile.hostile = false;
			projectile.friendly = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		public override void AI()
		{
			//Basically the code for a spirit heal
			int netTarget = (int)projectile.ai[0];
			int target = (int)projectile.ai[0];
			int healPower = (int)projectile.ai[1];
			float num605 = 4f;
			Vector2 center = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
			float dX = Main.npc[target].Center.X - center.X;
			float dY = Main.npc[target].Center.Y - center.Y;
			float distance = (float)Math.Sqrt((double)(dX * dX + dY * dY));
			if (distance < 50f && projectile.position.X < Main.npc[target].position.X + (float)Main.npc[target].width && projectile.position.X + (float)projectile.width > Main.npc[target].position.X && projectile.position.Y < Main.npc[target].position.Y + (float)Main.npc[target].height && projectile.position.Y + (float)projectile.height > Main.npc[target].position.Y)
			{
				if (projectile.owner == Main.myPlayer)
				{
					MNPC.healNPC(Main.npc[496], (int) healPower);
					NetMessage.SendData(66, -1, -1, "", netTarget, (float)healPower, 0f, 0f, 0, 0, 0);
				}
				projectile.Kill();
			}
			distance = num605 / distance;
			dX *= distance;
			dY *= distance;
			projectile.velocity.X = (projectile.velocity.X * 15f + dX) / 16f;
			projectile.velocity.Y = (projectile.velocity.Y * 15f + dY) / 16f;
			
			for(int i = 0; i < 255; i++)
			{
				Player p = Main.player[i];
				//Find the rectangles or "hitboxes" of the npc and projectile
				Rectangle mb = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
				Rectangle nb = new Rectangle((int)p.position.X, (int)p.position.Y, p.width, p.height); 
				
				//If the two cross together
				if (mb.Intersects(nb) && p.active && projectile.owner == Main.myPlayer)
				{
					MPlayer.healPlayer(p, (int) healPower);
					NetMessage.SendData(66, -1, -1, "", p.whoAmI, (float)healPower, 0f, 0f, 0, 0, 0);
					projectile.Kill();
				}
			}
			
			//Dust in the image of the spirit heal with a different dust value
			for (int num614 = 0; num614 < 5; num614++)
			{
				float num615 = projectile.velocity.X * 0.2f * (float)num614;
				float num616 = -(projectile.velocity.Y * 0.2f) * (float)num614;
				int num617 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 59, 0f, 0f, 100, default(Color), 1.3f);
				Main.dust[num617].noGravity = true;
				Main.dust[num617].velocity *= 0f;
				Dust dust61 = Main.dust[num617];
				dust61.position.X = dust61.position.X - num615;
				Dust dust62 = Main.dust[num617];
				dust62.position.Y = dust62.position.Y - num616;
			}
		}
	}
}