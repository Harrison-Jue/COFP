using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Melee.Spears
{
	public class GaeBolg : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Gae Bolg";
			item.damage = 125;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.scale = 1.1f;
			item.maxStack = 1;
			item.toolTip = "A legendary spear launches a mini spear that produces infernos in its wake that produces barbs.";
			item.useTime = 45;
			item.useAnimation = 45;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useTurn = true;
			item.useStyle = 5;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.rare = 3;
			item.shoot = mod.ProjectileType("GaeBolg");
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
				recipe.AddIngredient(mod.ItemType("AncientSpear"), 1);
				recipe.AddIngredient(ItemID.Gungnir, 1);
				recipe.AddIngredient(ItemID.InfernoFork, 1);
				recipe.AddIngredient(ItemID.Ectoplasm, 10);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}