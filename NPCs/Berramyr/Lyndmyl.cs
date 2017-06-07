using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP.NPCs.Berramyr
{
	public class Lyndmyl : ModNPC
	{
		public override void SetDefaults()
		{
			npc.friendly = false;
			npc.width = 62;
			npc.height = 60;
			npc.scale = 1.5f;
			npc.aiStyle = -1;
			npc.damage = 0;
			npc.defense = 100;
			npc.lifeMax = 10000;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0f;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lyndmyl");
		}
		
		public override void AI()
		{
			bool exists = false;
			bool hasFired = false;
			int Berramyr = 0;
			int friendProjCount = 0;
			
			for(int i = 0; i < Main.npc.Length - 1; i++)
			{
				if(Main.npc[i].type == mod.NPCType("Berramyr"))
				{
					Berramyr = i;
					exists = true;
					break;
				}
			}
			
			if(Main.npc[Berramyr].life < 1 || !exists)
			{
				npc.life -= npc.life;
				npc.checkDead();
			}
			
			if(!npc.dontTakeDamage)
			{
				for(int i = 0; i < 255; i++)
				{
					Player p = Main.player[i];
					if(p.channel)
					{
						p.channel = false;
					}
				}
				for(int i = 0; i < 1000; i++)
				{
					Projectile p = Main.projectile[i];
					if(p.friendly && p.active && !p.melee && !p.minion)
					{
						if(npc.ai[0] % 300 == 0 && !hasFired)
						{
							float shootToX = p.Center.X - npc.Center.X; 
							float shootToY = p.Center.Y - npc.Center.Y; 
							float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
							float speed = 10f / distance;
							shootToX *= speed; 
							shootToY *= speed; 
							Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootToX, shootToY, mod.ProjectileType("Lyndwave"), 0, 0, Main.myPlayer, 0f, 0f);
							hasFired = true;
						}
						friendProjCount++;
					}
				}
				if(friendProjCount > 5)
				{
					for(int i = 0; i < 1000; i++ )
					{
						Projectile p = Main.projectile[i];
						if(p.friendly && p.active && !p.melee && !p.minion)
						{
							p.aiStyle = -2;
							p.penetrate = 1;
							p.tileCollide = true;
						}
					}
				}
			}
			
			double deg = (double) npc.ai[0];
			double rad = deg * (Math.PI / 180);
			double dist = 150; 
			
			if(npc.ai[2] == 0)
			{
				npc.position.X = Main.npc[Berramyr].Center.X - (int)(Math.Cos(rad) * dist) - npc.width/2;
				npc.position.Y = Main.npc[Berramyr].Center.Y - (int)(Math.Sin(rad) * dist) - npc.height/2;
			}
			if(npc.ai[2] == 1)
			{
				npc.position.Y = Main.npc[Berramyr].Center.Y - (npc.height / 2);
				npc.position.X = npc.ai[3];
				
				for(int i = 0; i < 255; i++)
				{
					Player p = Main.player[i];
					if(p.active && !p.dead)
					{
						if(p.position.X > npc.position.X)
						{
							p.position = p.oldPosition;
						}
					}
				}
			}
			if(npc.ai[2] == 2)
			{
				MNPC.entrapment(npc, Berramyr, rad);
			}
			
			npc.ai[0] += 1f;
			npc.netUpdate = true;
		}
		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if((int)npc.life - (int)damage < 1)
			{
				damage = 0;
				npc.life = 100;
				npc.dontTakeDamage = true;
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}