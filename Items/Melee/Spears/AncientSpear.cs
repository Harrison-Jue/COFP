using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Melee.Spears
{
	public class AncientSpear : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Ancient Spear";
			item.damage = 30;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.scale = 1.1f;
			item.maxStack = 1;
			item.toolTip = "Spear launches a mini spear that produces rust that does a quarter of the damage.";
			item.useTime = 30;
			item.useAnimation = 30;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useTurn = true;
			item.useStyle = 5;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.rare = 3;
			item.shoot = mod.ProjectileType("AncientSpear");
			item.shootSpeed = 5f;
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
				recipe.AddIngredient(ItemID.Spear, 1);
				recipe.AddIngredient(ItemID.Bone, 30);
				recipe.AddRecipeGroup("T2Bar", 20);
				recipe.AddTile(TileID.Anvils);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}