using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Buffs.MidasSprayed
{
	public class STSprayed : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("STSprayed");
			Description.SetDefault("Solidifying to Silver or Tungsten");
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			MBuff.MidasDeBuff(npc, npc.buffTime[buffIndex], 3, 11, 83);
		}
		
		public override bool ReApply(NPC npc, int time, int buffIndex)
		{
			npc.buffTime[buffIndex] += 5;
			return true;
		}
	}
}