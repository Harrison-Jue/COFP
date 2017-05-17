using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Ranged
{
    public class Ichiival : ModItem
    {
		public override void SetDefaults()
		{
			item.name = "Ichiival";
			item.damage = 100;
			item.ranged = true;
			item.width = 60;
			item.height = 60;
			item.maxStack = 1;
			item.toolTip = "Fires 5 bolts that rains beams of lights and produces fireballs on impacts after it is destroyed.";
			item.useTime = 60;
			item.useAnimation = 60;
			item.knockBack = 7f;
			item.UseSound = SoundID.Item12;
			item.noMelee = true;
			item.useStyle = 5;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.rare = 9;
			item.shoot = mod.ProjectileType("BoltOfIchiival");
			item.shootSpeed = 8f;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float num70 = (float)Main.mouseX + Main.screenPosition.X - position.X;
			float num71 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
            float num87 = 0.314159274f;
			int num88 = 5;
			Vector2 vector = new Vector2(num70, num71);
			vector.Normalize();
			vector *= 40f;
			for (int num89 = 0; num89 < num88; num89++)
			{
				float num90 = (float)num89 - ((float)num88 - 1f) / 2f;
				Vector2 vector2 = vector.RotatedBy((double)(num87 * num90), default(Vector2));
				int num91 = Projectile.NewProjectile(position.X + vector2.X, position.Y + vector2.Y, speedX - num89, speedY - num89, type, damage, knockBack, Main.myPlayer);
			}
            return false;
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
				recipe.AddIngredient(mod.ItemType("AncientBow"), 1);
				recipe.AddIngredient(ItemID.DaedalusStormbow, 1);
				recipe.AddIngredient(ItemID.Tsunami, 1);
				recipe.AddIngredient(ItemID.Ectoplasm, 10);
				recipe.AddTile(TileID.Anvils);
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
    }
}