using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace COFP.Projectiles.NPCProj.Berramyr
{
	public class HellswarmMissile : ModProjectile
	{
		private int extraAI;
		
		public override void SetDefaults()
		{
			projectile.width = 15;
			projectile.height = 32;
			projectile.scale = 0.5f;
			projectile.timeLeft = 180;
			projectile.penetrate = 1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			extraAI = 0;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellswarm Missile");
		}
		
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			
			Player target = Main.player[(int)projectile.ai[0]];
			float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X; 
			float shootToY = target.position.Y - projectile.Center.Y; 
			float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
			float speed = 15f/distance;
			
			if(extraAI > 4)
			{
				projectile.alpha = 0;
			}
			else
			{
				projectile.alpha = 255;
			}
			
			if(extraAI > 10 && extraAI < 20)
			{
				shootToX *= speed;
				shootToY *= speed;
				projectile.velocity = new Vector2(shootToX, shootToY);
			}
			if(distance < 30)
			{
				projectile.Kill();
			}
			
			extraAI += 1;
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
			MMod.explosionEffect(projectile, 1f);
			
			Player target = Main.player[(int)projectile.ai[0]];
			float dX = target.position.X + (float)target.width * 0.5f - projectile.Center.X; 
			float dY = target.position.Y - projectile.Center.Y; 
			float distance = (float)System.Math.Sqrt((double)(dX * dX + dY * dY));
			if(distance < 50)
			{
				for(int i = 0; i < Main.npc.Length - 1; i++)
				{
					NPC berramyr = Main.npc[i];
					if(berramyr.type == mod.NPCType("Berramyr"))
					{
						target.Hurt(PlayerDeathReason.ByNPC(i), 10, projectile.direction);
					}
				}
			}
		}
		
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write((short)extraAI);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			extraAI = reader.ReadInt16();
		}
	}
}