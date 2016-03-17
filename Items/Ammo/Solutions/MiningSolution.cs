using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace COFP.Items.Ammo.Solutions
{
	public class MiningSolution : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Mining Solution";
			item.magic = true;
			item.width = 20;
			item.height = 28;
			item.maxStack = 999;
			item.toolTip = "The solution to mine efficiently!";
			item.consumable = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 5;
			item.shoot = mod.ProjectileType("MiningSolution");
			item.shootSpeed = 5f;
			item.ammo = mod.ItemType("CTSolution");
		}
		
		public override DrawAnimation GetAnimation()
		{
			//Animate item
			return new DrawAnimationVertical(7, 8);
		}
		
		public override void AddRecipes()
		{
			if(MMod.testMode)
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.DirtBlock);
				recipe.SetResult(this, 111);
				recipe.AddRecipe();
			}
		}
	}
}