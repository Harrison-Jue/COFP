using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP.NPCs.Berramyr
{
	public class Veiynamyl : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "Veiynamyl";
			npc.friendly = false;
			npc.width = 62;
			npc.height = 60;
			npc.scale = 1.5f;
			npc.aiStyle = -1;
			npc.damage = 0;
			npc.defense = 50;
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
			
			npc.position.X = Main.npc[Berramyr].Center.X - (int)(Math.Cos(rad) * dist) - npc.width/2;
			npc.position.Y = Main.npc[Berramyr].Center.Y - (int)(Math.Sin(rad) * dist) - npc.height/2;
		  
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