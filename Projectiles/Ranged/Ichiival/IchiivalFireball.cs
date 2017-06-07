using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Ranged.Ichiival
{
	public class IchiivalFireball : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 16;
			projectile.scale = 0.5f;
			projectile.aiStyle = 1;
			projectile.timeLeft = 180;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.extraUpdates = 1;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichiival");
		}
		
		public override void AI()
		{
			projectile.light = 0.9f;
			int DustID1 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.9f);
			int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.9f);
			int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 64, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 120, default(Color), 0.9f);
			Main.dust[DustID1].noGravity = true;
			Main.dust[DustID2].noGravity = true;
			Main.dust[DustID3].noGravity = true;
		}
		
		public override bool OnTileCollide(Vector2 velocityChange) 
		{
			if (projectile.velocity.X != velocityChange.X) 
			{
				projectile.velocity.X = -velocityChange.X/2; 
			}
			if (projectile.velocity.Y != velocityChange.Y) 
			{ 
				projectile.velocity.Y = -velocityChange.Y/2; 
			}
			projectile.timeLeft -= 30;
			return false;
		}
	}
}