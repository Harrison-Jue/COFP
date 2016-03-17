using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Usables
{
    public class BottledFairy : ModItem
    {
		public override void SetDefaults()
		{
			item.name = "Bottled Fairy";
			item.summon = true;
			item.width = 40;
			item.height = 24;
			item.scale = 0.75f;
			item.maxStack = 1;
			item.toolTip = "Let the Fairy out and it will carry you in real fast speeds!";
			item.useTime = 15;
			item.useAnimation = 15;
			item.knockBack = 5f;
			item.useSound = 11;
			item.noMelee = true;
			item.useStyle = 5;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = 9;
			item.shoot = mod.ProjectileType("FleeingFairy");
			item.shootSpeed = 40f;
			item.noUseGraphic = true;
			item.useTurn = true;
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
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			//Just set the player to have no falling damage on use
			player.noFallDmg = true;
			
			//Shoot the projectile
			return true;
        }
    }
}