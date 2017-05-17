using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Ranged
{
    public class AncientBow : ModItem
    {
		public override void SetDefaults()
		{
			item.name = "Ancient Bow";
			item.damage = 20;
			item.ranged = true;
			item.width = 60;
			item.height = 60;
			item.maxStack = 1;
			item.toolTip = "Fires an arrow that rains arrows after it is destroyed.";
			item.useTime = 30;
			item.useAnimation = 30;
			item.knockBack = 7f;
			item.UseSound = SoundID.Item11;
			item.noMelee = true;
			item.useStyle = 5;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.rare = 3;
			item.shoot = mod.ProjectileType("AncientArrow");
			item.shootSpeed = 8f;
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
				recipe.AddIngredient(ItemID.WoodenBow, 1);
				recipe.AddIngredient(ItemID.Bone, 30);
				recipe.AddRecipeGroup("T2Bar", 20);
				recipe.AddTile(TileID.Anvils);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
    }
}