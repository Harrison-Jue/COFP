using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
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
			npc.soundHit = 1;
			npc.soundKilled = 0;
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
			shop.item[nextSlot].SetDefaults(mod.ItemType("ShadowBloodVeil"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("BomberPlaneArrow"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("FighterPlaneArrow"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("PaperAirplaneArrow"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("ControllableSaw"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("BurningSaw"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("HallowedSaw"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("SpectralSaw"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("AncientSpear"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("GaeBolg"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("AncientBow"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("Ichiival"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("RainbowDevastation"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("PDTCore"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("CarrierBow"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("BottledFairy"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("MidasSprayer"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("MiningSolution"));
			nextSlot++;
		}
		public override string GetChat()
		{
			string chat = "I sell things.";
			return chat;
		}
	}
}