using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP.NPCs.Berramyr
{
	public class Fokmyl : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "Fokmyl";
			npc.friendly = false;
			npc.width = 62;
			npc.height = 60;
			npc.scale = 1.5f;
			npc.aiStyle = -1;
			npc.damage = 0;
			npc.defense = 200;
			npc.lifeMax = 10000;
			npc.soundHit = 4;
			npc.soundKilled = 14;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0f;
		}
		public override void AI()
		{
			bool exists = false;
			int Berramyr = 0;
			
			for(int i = 0; i < Main.npc.Length; i++)
			{
				if(Main.npc[i].type == mod.NPCType("Berramyr"))
				{
					Berramyr = i;
					exists = true;
					break;
				}
			}
			
			if(Main.npc[Berramyr].life < 1 || !exists)
			{
				npc.life -= npc.life;
				npc.checkDead();
			}
			
			double deg = (double) npc.ai[0];
			double rad = deg * (Math.PI / 180);
			double dist = 150; 
			
			if(npc.ai[2] == 0)
			{
				npc.position.X = Main.npc[Berramyr].Center.X - (int)(Math.Cos(rad) * dist) - npc.width/2;
				npc.position.Y = Main.npc[Berramyr].Center.Y - (int)(Math.Sin(rad) * dist) - npc.height/2;
			}
			if(npc.ai[2] == 1)
			{
				npc.position.Y = Main.npc[Berramyr].Center.Y - (npc.height / 2);
				npc.position.X = npc.ai[3];
			
				for(int i = 0; i < 200; i++)
				{
					Player p = Main.player[i];
					if(p.active && !p.dead)
					{
						if(p.position.X < npc.position.X)
						{
							p.position.X = npc.position.X + 20;
						}
					}
				}
			}
			
			/*if(npc.ai[0] % 60 == 0 && !npc.dontTakeDamage)
			{
				int x = (int)(npc.Center.X / 16f); //Tile position X
				int y = (int)(npc.Center.Y / 16f); //Tile position y
				//If tile position X is less than 0, then set it to 0 since the x position can't be less than 0
				if (x < 0)
				{
					x = 0;
				}
				//Same thing as X
				if (y < 0)
				{
					y = 0;
				}
				
				Tile tile = Main.tile[x, y];
				if(Main.tile[x, y] == null || !Main.tile[x, y].active())
				{
					tile = new Tile();
					Main.tile[x, y] = tile;
					Main.tile[x, y].active(true);
					Main.tile[x, y].type = (ushort)1;
					WorldGen.SquareTileFrame(x, y, true);
					NetMessage.SendTileSquare(-1, x, y, 1);
				}
				else
				{
					Main.tile[x, y].type = (ushort)1;
					WorldGen.SquareTileFrame(x, y, true);
					NetMessage.SendTileSquare(-1, x, y, 1);
				}
			}*/
			
			npc.ai[0] += 1f;
		}
		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if((int)npc.life - (int)damage < 1)
			{
				damage = 0;
				npc.life = 100;
				npc.dontTakeDamage = true;
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}