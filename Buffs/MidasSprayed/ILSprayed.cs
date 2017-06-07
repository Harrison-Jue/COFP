using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Buffs.MidasSprayed
{
	public class ILSprayed : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("ILSprayed");
			Description.SetDefault("Solidifying to Iron or Lead");
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			MBuff.MidasDeBuff(npc, npc.buffTime[buffIndex], 2, 8, 82);
		}
		
		public override bool ReApply(NPC npc, int time, int buffIndex)
		{
			npc.buffTime[buffIndex] += 5;
			return true;
		}
	}
}