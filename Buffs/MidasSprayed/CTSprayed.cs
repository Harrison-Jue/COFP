using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Buffs.MidasSprayed
{
	public class CTSprayed : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("CTSprayed");
			Description.SetDefault("Solidifying to Copper or Tin");
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			MBuff.MidasDeBuff(npc, npc.buffTime[buffIndex], 1, 9, 81);
		}
		
		public override bool ReApply(NPC npc, int time, int buffIndex)
		{
			npc.buffTime[buffIndex] += 5;
			return true;
		}
	}
}