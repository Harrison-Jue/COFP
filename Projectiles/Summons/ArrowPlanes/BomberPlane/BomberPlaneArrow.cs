using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.BomberPlane
{
	public class BomberPlaneArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 32;
			projectile.scale = 0.5f;
			projectile.aiStyle = 1;
			projectile.timeLeft = 20;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.minion = true;
			projectile.penetrate = -1;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bomber Plane Arrow");
		}
		
		public override void Kill(int timeLeft)
        {
			if(Main.netMode != 1)
			{
				int proj1 = Projectile.NewProjectile(projectile.position.X, projectile.position.Y - 32, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BomberPlane"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
				int proj2 = Projectile.NewProjectile(projectile.position.X, projectile.position.Y + 32, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BomberPlane"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
				int proj3 = Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y - 16, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BomberPlane"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
				int proj4 = Projectile.NewProjectile(projectile.position.X - 16, projectile.position.Y + 16, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BomberPlane"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
				int proj5 = Projectile.NewProjectile(projectile.position.X - 32, projectile.position.Y , projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("BomberPlane"), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
				
				//Makes sure projectiles AI counters are 0
				Main.projectile[proj1].ai[0] = 0;
				Main.projectile[proj2].ai[0] = 0;
				Main.projectile[proj3].ai[0] = 0;
				Main.projectile[proj4].ai[0] = 0;
				Main.projectile[proj5].ai[0] = 0;
			}
        }
	}
}