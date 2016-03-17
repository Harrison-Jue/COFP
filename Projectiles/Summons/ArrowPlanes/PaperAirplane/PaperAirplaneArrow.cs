using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.PaperAirplane
{
	public class PaperAirplaneArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Paper Airplane";
			projectile.width = 14;
			projectile.height = 32;
			projectile.aiStyle = 1;
			projectile.timeLeft = 20;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.minion = true;
		}
		public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 32, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("PaperAirplane"), projectile.damage, projectile.knockBack, Main.myPlayer);
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y + 32, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("PaperAirplane"), projectile.damage, projectile.knockBack, Main.myPlayer);
			Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y - 16, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("PaperAirplane"), projectile.damage, projectile.knockBack, Main.myPlayer);
			Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y + 16, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("PaperAirplane"), projectile.damage, projectile.knockBack, Main.myPlayer);
			Projectile.NewProjectile(projectile.position.X - 32, projectile.position.Y , projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("PaperAirplane"), projectile.damage, projectile.knockBack, Main.myPlayer);
        }
	}
}