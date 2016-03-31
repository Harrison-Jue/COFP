using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Summon
{
    public class CarrierBow : ModItem
    {
		public override void SetDefaults()
		{
			item.name = "Bow of the Carrier";
			item.damage = 30;
			item.summon = true;
			item.width = 16;
			item.height = 32;
			item.maxStack = 1;
			item.toolTip = "Fires arrows that turn into planes.";
			item.useTime = 45;
			item.useAnimation = 45;
			item.knockBack = 5f;
			item.useSound = 11;
			item.noMelee = true;
			item.useStyle = 5;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.rare = 9;
			item.shoot = mod.ProjectileType("PaperAirplaneArrow");
			item.shootSpeed = 2f;
			item.useAmmo = mod.ItemType("PaperAirplaneArrow");
			item.mana = 20;
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
				recipe.AddIngredient(ItemID.OpticStaff, 1);
				recipe.AddIngredient(ItemID.ShadowFlameBow, 1);
				recipe.AddIngredient(ItemID.Ectoplasm, 10);
				recipe.AddIngredient(ItemID.Wood, 30);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
    }
}