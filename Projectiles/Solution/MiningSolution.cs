using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Solution
{
	public class MiningSolution : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "MiningSolution";
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 90;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 2;
		}
		public override void AI()
		{
			Player p = Main.player[projectile.owner];
			
			for(int i = 0; i < 3; i++)
			{
				int DustID = Dust.NewDust(projectile.position, projectile.width + 2, projectile.height + 2, 54, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
				Main.dust[DustID].noGravity = true;
				Main.dust[DustID].scale *= 1.75f;
				Dust Dust1 = Main.dust[DustID];
				Dust1.velocity.X = Dust1.velocity.X * 2f;
				Dust Dust2 = Main.dust[DustID];
				Dust2.velocity.Y = Dust2.velocity.Y * 2f;
			}
			
			if(projectile.ai[0] == 0f)
			{
				int tPosX = (int)(projectile.position.X / 16f);
				int tEndPointX = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
				int tPosY = (int)(projectile.position.Y / 16f);
				int tEndPointY = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
				if (tPosX < 0)
				{
					tPosX = 0;
				}
				if (tEndPointX > Main.maxTilesX)
				{
					tEndPointX = Main.maxTilesX;
				}
				if (tPosY < 0)
				{
					tPosY = 0;
				}
				if (tEndPointY > Main.maxTilesY)
				{
					tEndPointY = Main.maxTilesY;
				}
				for (int x = tPosX; x < tEndPointX; x++)
				{
					for (int y = tPosY; y < tEndPointY; y++)
					{
							WorldGen.KillTile(x, y);
					}
				}
			}
			else
			{
				for(int n = 0; n < 401; n++)
				{
					Item i = Main.item[n];
					
					Rectangle MB = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
					Rectangle NB = new Rectangle((int)i.position.X + (int)i.velocity.X, (int)i.position.Y + (int)i.velocity.Y, i.width, i.height); 
					
					if(MB.Intersects(NB))
					{	
						i.position.X = p.position.X;
						i.position.Y = p.position.Y;
					}
				}
			}
		}
	}
}