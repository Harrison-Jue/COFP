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
    public class MBuff : GlobalBuff
    {
		//Static method to help a buff use the Midas debuff
		public static void MidasDeBuff(NPC npc, int buffTime, int dot, int dType1, int dType2)
		{
			//Every 5 ticks damage the npc regardless of defense
			if(buffTime % 5 == 0)
			{
				npc.StrikeNPC(dot + (npc.defense / 2), 0f, npc.direction, false, false, false); //Shows the damage on the npc
			}
			int dustType1 = dType1;
			int dustType2 = dType2;
			for(int i = 0; i < 2; i++)
			{
				int DustID = Dust.NewDust(npc.position, npc.width + 2, npc.height + 2, dustType1, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), 1f);
				Main.dust[DustID].noGravity = true;
				Main.dust[DustID].velocity *= 0.2f;
				if(dType2 != -1)
				{
					DustID = Dust.NewDust(npc.position, npc.width + 2, npc.height + 2, dustType2, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), 1f);
					Main.dust[DustID].noGravity = true;
					Main.dust[DustID].velocity *= 0.2f;
				}
			}
			npc.checkDead(); //Important, allows the game to know the npc is dead and make it drop loot
		}
	}
}