using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Melee.Saws
{
	public class HallowedSaw : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 50;
			item.melee = true;
			item.width = 38;
			item.height = 38;
			item.scale = 1.1f;
			item.maxStack = 1;
			item.useTime = 45;
			item.useAnimation = 45;
			item.knockBack = 4f;
			item.UseSound = SoundID.Item23;
			item.noMelee = true;
			item.channel = true;
			item.noUseGraphic = true;
			item.useTurn = true;
			item.useStyle = 5;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 5;
			item.shoot = mod.ProjectileType("HallowedSaw");
			item.shootSpeed = 5f;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Saw");
			Tooltip.SetDefault("A controllable saw that burns, slows, and lowers defense; it breaks after 30 hits.");
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
				recipe.AddIngredient(mod.ItemType("BurningSaw"), 1);
				recipe.AddIngredient(ItemID.HallowedBar, 20);
				recipe.AddIngredient(ItemID.SoulofLight, 10);
				recipe.AddIngredient(ItemID.SoulofNight, 10);
				recipe.AddIngredient(ItemID.Sawmill);
				recipe.AddTile(TileID.MythrilAnvil);
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