using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP
{
    public class MProjectile : GlobalProjectile
    {
		public override void AI(Projectile projectile)
		{
			//Setting up trailing for all projectiles, regardless of mod or not
			for (int num46 = projectile.oldPos.Length - 1; num46 > 0; num46--)
			{
				projectile.oldPos[num46] = projectile.oldPos[num46 - 1];
			}
			projectile.oldPos[0] = projectile.position;
			
			if(projectile.aiStyle == -2)
			{
				int lSlot = NPC.FindFirstNPC(mod.NPCType("Lyndmyl"));
				if(lSlot >= 0)
				{
					NPC lyndmyl = Main.npc[lSlot];
					float dX = lyndmyl.Center.X - projectile.Center.X;
					float dY = lyndmyl.Center.Y - projectile.Center.Y;
					float distance = (float)Math.Sqrt((double)(dX * dX + dY * dY));
					float speed = 15f;
					speed /= distance;
					
					projectile.velocity = new Vector2(dX * speed, dY * speed);
				}
			}
		}
		
		//Static method to help mediate AI of the projectile that changes tiles
		public static void MidasAI(Projectile projectile, int tType1, int tType2, int dType1, int dType2)
		{
			projectile.light = 0.5f;
            projectile.alpha = 255;
			projectile.height += 1;
			projectile.width += 1;
			
			if(tType2 == -1 && dType2 == -1)
			{
				tType2 = tType1;
				dType2 = dType1;
			}
			
			//Choosing what type of material to convert
			int type = 0;
			int dustType = 0;
			if(projectile.ai[0] == 0f)
			{
				type = tType1;
				dustType = dType1;
			}
			else
			{
				type = tType2;
				dustType = dType2;
			}
			
			//Converts tiles based on the tile size and position of the projectile
			int tPosX = (int)(projectile.position.X / 16f); //Tile position X
			int tEndPointX = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2; //Tile end point X
			int tPosY = (int)(projectile.position.Y / 16f); //Tile position y
			int tEndPointY = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2; //Tile end point y
			//If tile position X is less than 0, then set it to 0 since the x position can't be less than 0
			if (tPosX < 0)
			{
				tPosX = 0;
			}
			//If the position X is greater than the max tiles, set it to the max tiles since you can't go beyond the max tiles
			if (tEndPointX > Main.maxTilesX)
			{
				tEndPointX = Main.maxTilesX;
			}
			//Same thing as X
			if (tPosY < 0)
			{
				tPosY = 0;
			}
			if (tEndPointY > Main.maxTilesY)
			{
				tEndPointY = Main.maxTilesY;
			}
			//For loops to go through the x positions and the y positions to change it and send the data through the net
			for (int x = tPosX; x < tEndPointX; x++)
			{
				for (int y = tPosY; y < tEndPointY; y++)
				{
                        Main.tile[x, y].type = (ushort) type;
                        WorldGen.SquareTileFrame(x, y, true);
                        NetMessage.SendTileSquare(-1, x, y, 1);
				}
            }
			
			//Spawning dust for projectile
			for(int i = 0; i < 3; i++)
			{
				int DustID = Dust.NewDust(projectile.position, projectile.width + 2, projectile.height + 2, dustType, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
				Main.dust[DustID].noGravity = true;
				Main.dust[DustID].scale *= 1.75f;
				
				//Accessory dust to round out the dust
				Dust Dust1 = Main.dust[DustID];
				Dust1.velocity.X = Dust1.velocity.X * 2f;
				Dust Dust2 = Main.dust[DustID];
				Dust2.velocity.Y = Dust2.velocity.Y * 2f;
			}
			
			//Debuffing
			if(projectile.timeLeft % 2 == 0)
			{
				//For every npc in the array npcs
				foreach (NPC n in Main.npc)
				{
					//Find the rectangles or "hitboxes" of the npc and projectile
					Rectangle mb = new Rectangle((int)projectile.position.X + (int)projectile.velocity.X, (int)projectile.position.Y + (int)projectile.velocity.Y, projectile.width, projectile.height); 
					Rectangle nb = new Rectangle((int)n.position.X, (int)n.position.Y, n.width, n.height); 
					
					//If the two cross together
					if (mb.Intersects(nb) && n.life > 0 && n.active &&!n.friendly)
					{
						n.AddBuff((int)projectile.ai[1], 30, false);
					}
				}
			}
		}
	}
}