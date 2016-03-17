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
			item.name = "Shadow Blood Veil";
			item.width = 20;
			item.height = 20;
			item.toolTip = "Continuously gives a barrier that drains enemy health and gain it as your own.";
			item.value = 11000;
			item.rare = 5;
			item.accessory = true;
			item.value = Item.sellPrice(0, 1, 0, 0);
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