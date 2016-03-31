using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Ammo.Plane_Arrows
{
	public class PaperAirplaneArrow : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Paper Airplane Arrow";
			item.damage = 60;
			item.summon = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.toolTip = "Arrows that turn into Paper Airplanes.";
			item.consumable = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 2;
			item.shoot = mod.ProjectileType("PaperAirplaneArrow");
			item.shootSpeed = 5f;
			item.ammo = mod.ItemType("PaperAirplaneArrow");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WoodenArrow, 25);
			recipe.AddIngredient(ItemID.Wood, 5);
			recipe.AddIngredient(ItemID.PixieDust, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 25);
			recipe.AddRecipe();
		}
	}
}