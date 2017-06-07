using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class Berraworks : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.scale = 2;
			projectile.timeLeft = 30;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berraworks");
		}
		
		public override void AI()
		{
			if(projectile.ai[0] > 4f)
			{
				projectile.alpha = 0;
			}
			else
			{
				projectile.alpha = 255;
			}
			projectile.ai[0] += 1f;
		}
		
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			for(int i = 0; i < Main.npc.Length - 1; i++)
			{
				NPC niremyl = Main.npc[i];
				if(niremyl.type == mod.NPCType("Niremyl"))
				{
					if(!niremyl.dontTakeDamage)
					{
						target.AddBuff(BuffID.OnFire, 180, false);
					}
				}
			}
		}
		
		public override void Kill(int timeLeft)
		{
			for(int i = 0; i <= 360; i += 10)
			{
				float rad = i * (float)(Math.PI/180);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)Math.Cos(rad) * 10, (float)Math.Sin(rad) * 10, mod.ProjectileType("Berrabullet"), 10, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}