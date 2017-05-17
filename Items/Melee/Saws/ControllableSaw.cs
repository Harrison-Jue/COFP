using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Melee.Saws
{
	public class ControllableSaw : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Controllable Saw";
			item.damage = 13;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.scale = 1.1f;
			item.maxStack = 1;
			item.toolTip = "A controllable saw that breaks after 10 hits.";
			item.useTime = 45;
			item.useAnimation = 45;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item23;
			item.noMelee = true;
			item.channel = true;
			item.noUseGraphic = true;
			item.useTurn = true;
			item.useStyle = 5;
			item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = 5;
			item.shoot = mod.ProjectileType("Saw");
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
				recipe.AddRecipeGroup("T2Bar", 10);
				recipe.AddIngredient(ItemID.Sawmill, 1);
				recipe.AddTile(TileID.Anvils);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.channel = true;
			return true;
		}
	}
}