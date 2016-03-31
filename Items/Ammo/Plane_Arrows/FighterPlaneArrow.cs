using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Ammo.Plane_Arrows
{
	public class FighterPlaneArrow : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Fighter Plane Arrow";
			item.damage = 30;
			item.summon = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.toolTip = "Arrows that turn into Fighters.";
			item.consumable = true;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 2, 0);
			item.rare = 2;
			item.shoot = mod.ProjectileType("FighterPlaneArrow");
			item.shootSpeed = 9f;
			item.ammo = mod.ItemType("PaperAirplaneArrow");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PaperAirplaneArrow"), 25);
			recipe.AddCraftGroup(mod.GetCraftGroup("T2Bar"), 5);
			recipe.AddIngredient(ItemID.PixieDust, 5);
			recipe.SetResult(this, 25);
			recipe.AddRecipe();
		}
	}
}