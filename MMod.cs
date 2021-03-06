using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP
{
	public class MMod : Mod
	{
		public static bool testMode = false;
		public static bool tileDestruction = true;
		
		public MMod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
		
		public override void AddRecipeGroups()
		{
			RecipeGroup group;
			group = new RecipeGroup(() => Lang.misc[37].Value + " Tier 2 Bars", new int[] 
			{
				ItemID.IronBar, 
				ItemID.LeadBar
			});
			RecipeGroup.RegisterGroup("T2Bar", group);
			group = new RecipeGroup(() => Lang.misc[37].Value + " Tier 4 Bars", new int[] 
			{
				ItemID.GoldBar, 
				ItemID.PlatinumBar
			});
			RecipeGroup.RegisterGroup("T4Bar", group);
			group = new RecipeGroup(() => Lang.misc[37].Value + " Evil Bars", new int[] 
			{
				ItemID.DemoniteBar, 
				ItemID.CrimtaneBar
			});
			RecipeGroup.RegisterGroup("EvilBar", group);
			group = new RecipeGroup(() => Lang.misc[37].Value + " Hardmode Evil Substance", new int[] 
			{
				ItemID.Ichor, 
				ItemID.CursedFlame
			});
			RecipeGroup.RegisterGroup("EvilSubstanceHM", group);
			group = new RecipeGroup(() => Lang.misc[37].Value + " Evil Substance", new int[] 
			{
				ItemID.RottenChunk, 
				ItemID.Vertebrae
			});
			RecipeGroup.RegisterGroup("EvilSubstance", group);
			group = new RecipeGroup(() => Lang.misc[37].Value + " Evil Powder", new int[] 
			{
				ItemID.VilePowder, 
				ItemID.ViciousPowder
			});
			RecipeGroup.RegisterGroup("EvilPowder", group);
		}
		
		//Simple way to add explosions to anyone since Entity is the base class for all things, you can use any extended classes on it
		public static void explosionEffect(Entity entity, float scale)
		{
			Main.PlaySound(2, (int)entity.position.X, (int)entity.position.Y, 14);
            for (int num369 = 0; num369 < 20; num369++)
            {
                int num370 = Dust.NewDust(new Vector2(entity.position.X, entity.position.Y), entity.width, entity.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num370].velocity *= 1.4f;
            }
            for (int num371 = 0; num371 < 10; num371++)
            {
                int num372 = Dust.NewDust(new Vector2(entity.position.X, entity.position.Y), entity.width, entity.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num372].noGravity = true;
                Main.dust[num372].velocity *= 5f;
                num372 = Dust.NewDust(new Vector2(entity.position.X, entity.position.Y), entity.width, entity.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num372].velocity *= 3f;
            }
            int num373 = Gore.NewGore(new Vector2(entity.position.X, entity.position.Y), default(Vector2), Main.rand.Next(61, 64), scale);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore85 = Main.gore[num373];
            gore85.velocity.X = gore85.velocity.X + 1f;
            Gore gore86 = Main.gore[num373];
            gore86.velocity.Y = gore86.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(entity.position.X, entity.position.Y), default(Vector2), Main.rand.Next(61, 64), scale);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore87 = Main.gore[num373];
            gore87.velocity.X = gore87.velocity.X - 1f;
            Gore gore88 = Main.gore[num373];
            gore88.velocity.Y = gore88.velocity.Y + 1f;
            num373 = Gore.NewGore(new Vector2(entity.position.X, entity.position.Y), default(Vector2), Main.rand.Next(61, 64), scale);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore89 = Main.gore[num373];
            gore89.velocity.X = gore89.velocity.X + 1f;
            Gore gore90 = Main.gore[num373];
            gore90.velocity.Y = gore90.velocity.Y - 1f;
            num373 = Gore.NewGore(new Vector2(entity.position.X, entity.position.Y), default(Vector2), Main.rand.Next(61, 64), scale);
            Main.gore[num373].velocity *= 0.4f;
            Gore gore91 = Main.gore[num373];
            gore91.velocity.X = gore91.velocity.X - 1f;
            Gore gore92 = Main.gore[num373];
            gore92.velocity.Y = gore92.velocity.Y - 1f;
		}
	}
}