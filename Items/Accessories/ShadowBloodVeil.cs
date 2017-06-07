using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Accessories
{
    public class ShadowBloodVeil : ModItem
    {
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 11000;
			item.rare = 5;
			item.accessory = true;
			item.value = Item.sellPrice(0, 1, 0, 0);
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Blood Veil");
			Tooltip.SetDefault("Continuously gives a barrier that drains enemy health and gain it as your own.");
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddRecipeGroup("EvilBar", 10);
			recipe.AddRecipeGroup("EvilSubstance", 30);
			recipe.AddRecipeGroup("EvilPowder", 30);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}