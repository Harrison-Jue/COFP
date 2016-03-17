using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Usables
{
    public class MysteriousController : ModItem
    {
		public override void SetDefaults()
		{
			item.name = "Mysterious Controller";
			item.width = 34;
			item.height = 42;
			item.scale = 0.75f;
			item.maxStack = 1;
			item.toolTip = "What a mysterious thing!";
			item.useTime = 45;
			item.useAnimation = 45;
			item.useSound = 11;
			item.useStyle = 1;
			item.value = Item.buyPrice(1, 0, 0, 0);
			item.rare = 9;
		}
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(mod.NPCType("Berramyr"));
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Berramyr"));
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		public override void AddRecipes()
		{
			if(MMod.testMode)
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.DirtBlock);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
		
    }
}