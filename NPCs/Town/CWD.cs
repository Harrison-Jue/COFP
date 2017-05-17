using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.NPCs.Town
{
	public class CWD : ModNPC
	{
		public override void SetDefaults()
		{
			npc.name = "Custom Weapons Developer";
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			Main.npcFrameCount[npc.type] = 16;
			animationType = 28;
		}
		public override bool CanTownNPCSpawn(int numTownNPCs, int money) 
		{
			return true;
		}
		public override string TownNPCName()
		{
			return "Ed";
		}
		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Shop";
		}
		public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
		{
			if (firstButton)
			{	
				openShop = true;
			}
		}
		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(mod.ItemType("ControllableSaw"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("AncientSpear"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("AncientBow"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("BottledFairy"));
			nextSlot++;
			if(Main.player[Main.myPlayer].HasItem(mod.ItemType("CarrierBow")))
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("PaperAirplaneArrow"));
				nextSlot++;
			}
			if(Main.player[Main.myPlayer].HasItem(mod.ItemType("MidasSprayer")))
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("MiningSolution"));
				nextSlot++;
			}
		}
		public override string GetChat()
		{
			string chat = "I sell basic COFP supplies.";
			return chat;
		}
	}
}