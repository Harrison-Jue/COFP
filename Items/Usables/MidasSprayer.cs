using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Usables
{
    public class MidasSprayer : ModItem
    {
		public override void SetDefaults()
		{
			item.name = "Midas Sprayer";
			item.summon = true;
			item.width = 40;
			item.height = 24;
			item.scale = 0.75f;
			item.maxStack = 1;
			item.toolTip = "A clentimator for quality of life!";
			item.useTime = 4;
			item.useAnimation = 12;
			item.knockBack = 5f;
			item.useSound = 11;
			item.noMelee = true;
			item.useStyle = 5;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.rare = 9;
			item.shoot = mod.ProjectileType("CTSolution");
			item.shootSpeed = 1f;
			item.autoReuse = true;
			item.useAmmo = mod.ItemType("CTSolution");
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float altFunction = 0f;
			if(player.altFunctionUse == 2 && type != mod.ProjectileType("MSolution"))
			{
				altFunction = 1f;
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, altFunction, 0f);
			return false;
		}
		public override bool ConsumeAmmo(Player p) //Tells the game whether the item consumes ammo or not
        {
			//If the animation reaches less than the initial useAnimation - 8 ticks
            if(p.itemAnimation < p.inventory[p.selectedItem].useAnimation - 8)
            {
					return true; //Ammo is consumed
            }
            else
            {
                return false; //Ammo is not consumed before animation is finished
            }
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
				recipe.AddIngredient(ItemID.Clentaminator, 1);
				recipe.AddIngredient(ItemID.Picksaw, 1);
				recipe.AddIngredient(ItemID.Ectoplasm, 20);
				recipe.AddIngredient(ItemID.GoldBar, 20);
				recipe.AddIngredient(ItemID.GreenSolution, 999);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
    }
}