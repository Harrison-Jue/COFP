using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP
{
	public class MNPC : GlobalNPC
	{
		//A static method great for healing NPCs
		public static void healNPC(NPC npc, int healPower)
		{
			int heal = (int) ((float)healPower * MPlayer.healPowerMult);
			npc.life += heal;
			if(npc.life > npc.lifeMax)
			{
				npc.life = npc.lifeMax;
			}
			CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), new Color(100, 255, 100, 255), string.Concat(heal), false, false);
		}
		
		//Affects drop of NPCs
		public override void NPCLoot(NPC npc)
		{
			//Drops ore depending on buff
			if(npc.HasBuff(mod.BuffType("CTSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.CopperOre, ItemID.TinOre);
			}
			if(npc.HasBuff(mod.BuffType("ILSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.IronOre, ItemID.LeadOre);
			}
			if(npc.HasBuff(mod.BuffType("STSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.SilverOre, ItemID.TungstenOre);
			}
			if(npc.HasBuff(mod.BuffType("GPSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.GoldOre, ItemID.PlatinumOre);
			}
			if(npc.HasBuff(mod.BuffType("MSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.Meteorite, -1);
			}
			if(npc.HasBuff(mod.BuffType("DCSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.DemoniteOre, ItemID.CrimtaneOre);
			}
			if(npc.HasBuff(mod.BuffType("OHSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.Obsidian, ItemID.Hellstone);
			}
			if(npc.HasBuff(mod.BuffType("CPSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.CobaltOre, ItemID.PalladiumOre);
			}
			if(npc.HasBuff(mod.BuffType("MOSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.MythrilOre, ItemID.OrichalcumOre);
			}
			if(npc.HasBuff(mod.BuffType("ATSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.AdamantiteOre, ItemID.TitaniumOre);
			}
			if(npc.HasBuff(mod.BuffType("CSprayed")) != -1)
			{
				oreDropHelper(npc, ItemID.ChlorophyteOre, -1);
			}
		}
		
		//Static method that can be called on other class files to help drop two types of ores 
		//(though it doesn't have to be ores)
		public static void oreDropHelper(NPC npc, int oType1, int oType2)
		{
			int factor = 0;
			int amount = 0;
			int rng = 0;
			
			//Keep doing this while the amount is less than 0 because you can't have a negative amounts of items
			do
			{
				factor = Main.rand.Next(-10, 10);
				amount = npc.lifeMax/10 + factor;
			} while(amount < 0);
			
			//Determines what to drop
			rng = Main.rand.Next(0, 10);
			if(rng < 5 || oType2 == -1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, oType1, amount, false, 0, false);
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, oType2, amount, false, 0, false);
			}
		}
	}
}