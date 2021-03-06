using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace COFP.Items.Ammo.Solutions
{
	public class STSolution : ModItem
	{
		public override void SetDefaults()
		{
			item.magic = true;
			item.width = 20;
			item.height = 28;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 5;
			item.shoot = mod.ProjectileType("STSolution");
			item.shootSpeed = 5f;
			item.ammo = mod.ItemType("CTSolution");
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ST Solution");
			Tooltip.SetDefault("The solution to make everything into silver or tungsten!");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 8));
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
			else
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.SilverOre, 11);
				recipe.AddIngredient(mod.ItemType("MiningSolution"), 111);
				recipe.SetResult(this, 111);
				recipe.AddRecipe();
				recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.TungstenOre, 11);
				recipe.AddIngredient(mod.ItemType("MiningSolution"), 111);
				recipe.SetResult(this, 111);
				recipe.AddRecipe();
			}
		}
	}
}