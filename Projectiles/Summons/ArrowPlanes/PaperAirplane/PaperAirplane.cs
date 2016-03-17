using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Projectiles.Summons.ArrowPlanes.PaperAirplane
{
	public class PaperAirplane : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Paper Airplane";
			projectile.width = 14;
			projectile.height = 32;
			projectile.scale = 0.5f;
			projectile.aiStyle = 36;
			projectile.timeLeft = 20;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.minion = true;
		}
	}
}