using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.FighterPlane
{
	public class FighterPlaneArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Fighter Plane";
			projectile.width = 14;
			projectile.height = 32;
			projectile.aiStyle = 1;
			projectile.timeLeft = 20;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.minion = true;
		}
		public override void Kill(int timeLeft)
        {
			if(Main.netMode != 1)
			{
				int proj1 = Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 32, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FighterPlane"), projectile.damage, projectile.knockBack, Main.myPlayer);
				int proj2 = Projectile.NewProjectile(projectile.position.X, projectile.position.Y + 32, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FighterPlane"), projectile.damage, projectile.knockBack, Main.myPlayer);
				int proj3 = Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y - 16, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FighterPlane"), projectile.damage, projectile.knockBack, Main.myPlayer);
				int proj4 = Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y + 16, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FighterPlane"), projectile.damage, projectile.knockBack, Main.myPlayer);
				int proj5 = Projectile.NewProjectile(projectile.position.X - 32, projectile.position.Y , projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FighterPlane"), projectile.damage, projectile.knockBack, Main.myPlayer);
			}
		}
	}
}