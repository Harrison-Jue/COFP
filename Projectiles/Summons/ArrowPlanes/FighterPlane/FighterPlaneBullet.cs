using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.FighterPlane
{
	public class FighterPlaneBullet : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Fighter Plane";
			projectile.width = 4;
			projectile.height = 4;
			projectile.scale = 0.75f;
			projectile.aiStyle = 0;
			projectile.timeLeft = 60;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			projectile.minion = true;
		}
		public override void AI()
		{
			projectile.light = 0.6f;
		}
	}
}