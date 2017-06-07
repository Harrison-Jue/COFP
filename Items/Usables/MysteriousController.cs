using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Items.Usables
{
    public class MysteriousController : ModItem
    {
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 42;
			item.scale = 0.75f;
			item.maxStack = 1;
			item.useTime = 45;
			item.useAnimation = 45;
			item.UseSound = SoundID.Item11;
			item.useStyle = 1;
			item.value = Item.buyPrice(1, 0, 0, 0);
			item.rare = 9;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mysterious Controller");
			Tooltip.SetDefault("What a mysterious thing!");
		}
		
		public override bool CanUseItem(Player player)
		{
			if(NPC.AnyNPCs(mod.NPCType("Berramyr")))
			{
				Main.NewText("It's probably a bad idea to spawn another one of them...");
				return false;
			}
			else if(player.position.Y > Main.worldSurface * 16)
			{
				Main.NewText("Seems this thing needs to be on the surface to do anything.");
				return false;
			}
			return !NPC.AnyNPCs(mod.NPCType("Berramyr")) && player.position.Y < Main.worldSurface * 16;
		}
		
		public override bool UseItem(Player player)
		{
			int[] direction = {-1, 1};
			int rand1 = direction[Main.rand.Next(0, 1)] * Main.rand.Next(1200, 1300);
			int rand2 = direction[Main.rand.Next(0, 1)] * Main.rand.Next(1200, 1300);
			int n = NPC.NewNPC((int)player.Center.X + rand1, (int)player.Center.Y + rand2, mod.NPCType("Berramyr"), 0, 0f, 0f, 0f, 0f, 255);
			Main.NewText("Berramyr has awoken!", (byte)255, (byte)255, (byte)255, false);
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
			return true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
    }
}