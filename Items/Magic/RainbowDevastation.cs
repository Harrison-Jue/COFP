using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace COFP.Items.Magic
{
	public class RainbowDevastation : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 180;
			item.magic = true;
			item.width = 38;
			item.height = 38;
			item.scale = 1.1f;
			item.maxStack = 1;
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
			item.channel = true;
			item.shoot = mod.ProjectileType("RainbowDevastationRod");
			item.shootSpeed = 5f;
		}
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rainbow Devastation");
			Tooltip.SetDefault("A powerful and colorfull staff, capable of destroying everything.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 7));
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
				recipe.AddIngredient(ItemID.RainbowRod, 1);
				recipe.AddIngredient(ItemID.RainbowGun, 1);
				recipe.AddIngredient(ItemID.RainbowCrystalStaff, 1);
				recipe.AddIngredient(ItemID.LunarBar, 10);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//Making sure the player channels
			player.channel = true;
			
			//Spawn the rod projectile, set the first counter to 30 updates or 1/2 a second
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 0, knockBack, player.whoAmI, 30f, 0f);
			
			//Spawn the vortex
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("RainbowDevastationVortex"), damage, knockBack, player.whoAmI, 0f, 0f);
			
			//Makes sure original projectile doesn't spawn
			return false;
		}
	}
}