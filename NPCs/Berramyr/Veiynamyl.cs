using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace COFP.NPCs.Berramyr
{
	public class Veiynamyl : ModNPC
	{
		public override void SetDefaults()
		{
			npc.friendly = false;
			npc.width = 62;
			npc.height = 60;
			npc.scale = 1.5f;
			npc.aiStyle = -1;
			npc.damage = 0;
			npc.defense = 50;
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
			DisplayName.SetDefault("Veiynamyl");
		}
		
		public override void AI()
		{
			bool exists = false;
			int Berramyr = 0;
			
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
			
			double deg = (double) npc.ai[0];
			double rad = deg * (Math.PI / 180);
			double dist = 150; 
			
			if(!npc.dontTakeDamage)
			{
				if(npc.ai[0] % 120 == 0)
				{
					MNPC.healNPC(Main.npc[Berramyr], 1000);
				}
				if(npc.ai[0] % 300 == 0 && Main.netMode != 1)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("Berraheal"), 0, 0f, Main.myPlayer, Berramyr, 5000);
				}
			}
			
			npc.position.X = Main.npc[Berramyr].Center.X - (int)(Math.Cos(rad) * dist) - npc.width/2;
			npc.position.Y = Main.npc[Berramyr].Center.Y - (int)(Math.Sin(rad) * dist) - npc.height/2;

			if(npc.ai[2] == 1)
			{
				if(npc.ai[0] % 30 == 0)
				{
					for(int i = 225; i <= 315; i += 15)
					{
						float shotAngle = (float)(i*(Math.PI/180));
						float speedY = (float)Math.Sin(shotAngle) * 10f;
						float speedX = (float)Math.Cos(shotAngle) * 10f;
						int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speedX, -speedY, mod.ProjectileType("Berrabullet"), 10, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[proj1].timeLeft = 9999;
						Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
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