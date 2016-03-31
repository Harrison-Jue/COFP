using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Pets
{
	public class PDTCore : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.name = "PDT Core";
			item.toolTip = "Summons a Point Defense Turret that defends you from enemy projectiles.";
			item.shoot = mod.ProjectileType("PDT");
			item.buffType = mod.BuffType("PDT");
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
			else
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ItemID.Seaweed, 1);
				recipe.AddIngredient(ItemID.XenoStaff, 1);
				recipe.AddIngredient(ItemID.LunarBar, 10);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}