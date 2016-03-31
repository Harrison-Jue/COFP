using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Lyndwave : ModProjectile
	{
		
		public override void SetDefaults()
		{
			projectile.name = "Lyndwave";
			projectile.width = 60;
			projectile.height = 32;
			projectile.scale = 1f;
			projectile.timeLeft = 180;
			projectile.penetrate = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			
			for(int i = 0; i < 1000; i++)
			{
				Projectile p = Main.projectile[i];
				Rectangle mb = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
				Rectangle nb = new Rectangle((int)p.position.X, (int)p.position.Y, p.width, p.height); 
					
				//If the two cross together
				if (mb.Intersects(nb) && p.friendly)
				{
					p.Kill();
				}
			}
		}
	}
}