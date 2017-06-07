//I don't feel like explaining this.... At least it is self explanitory!

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace COFP.NPCs.Berramyr
{
	public class Berramyr : ModNPC
	{
		private Vector2 moveTo = new Vector2(0, 0);
		private int didOnce = 0;
		private int firstTimeSet = 0;
		private int mode = 3;
		private int modeTime = 0;
		private int coolDown = 0;
		private int portCoolDown = 0;
		private int fokmyl = 0;
		private int lyndmyl = 0;
		private int niremyl = 0;
		private int veiynamyl = 0;
		private int hellHoleMode = 0;
		private int defaultDefense = 40;
		
		public override void SetDefaults()
		{
			npc.friendly = false;
			npc.scale = 1.5f;
			npc.width = 106;
			npc.height = 106;
			npc.aiStyle = -1;
			npc.damage = 0;
			npc.defense = defaultDefense;
			npc.lifeMax = 35000;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0;
			npc.value = Item.buyPrice(0, 10, 0, 0);
			music = MusicID.Boss1;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berramyr");
		}
		
		public override void AI()
		{
			int maxDefense = 0;
			int target = 0;
			npc.TargetClosest(false);
			for(int i = 0; i < 255; i++)
			{
				Player pTarget = Main.player[i];
				if(pTarget.statDefense > maxDefense)
				{
					maxDefense = pTarget.statDefense;
					target = i;
				}
			}
			Player p = Main.player[target];
			
			if(npc.localAI[0] == 0f && Main.netMode != 1)
			{
				fokmyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Fokmyl"), 0, 0f, 0f, 255);
				niremyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Niremyl"), 0, 90f, 0f, 0f,  255);
				lyndmyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Lyndmyl"), 0, 180f, 0f, 0f, 255);
				veiynamyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Veiynamyl"), 0, 270f, 0f, 0f, 255);
				coolDown = 300;
				portCoolDown = 0;
				modeTime = 0;
				didOnce = 0;
				firstTimeSet = 0;
				npc.localAI[0] = 1f;
				npc.ai[0] = 0f;
				npc.ai[1] = 0f;
				mode = -1;
							
				int randX = Main.rand.Next(-500, 500);
				int randY = Main.rand.Next(-100, 500);
				moveTo = new Vector2(p.Center.X + randX, p.Center.Y + randY); 
			}
			
			if(!Main.npc[fokmyl].dontTakeDamage)
			{
				npc.defense = defaultDefense + 20;
			}
			else
			{
				npc.defense = defaultDefense;
			}
			
			if(npc.ai[0] > 90)
			{				
				int randX = Main.rand.Next(-500, 500);
				int randY = Main.rand.Next(-100, 500);
				moveTo = new Vector2(p.Center.X + randX, p.Center.Y + randY);
				npc.ai[0] = 0;
				didOnce = 0;
			}
			
			if(mode == -1)
			{
				floatAround(moveTo, p);
				if(coolDown % 60 == 0 && hellHoleMode != 1)
				{
					berrabeam(p);
				}
			}
			if(npc.life < npc.lifeMax / 2 && p.position.Y < (float)((Main.maxTilesY - 204) * 16))
			{
				modeTime = 0;
				coolDown = 300;
				if(hellHoleMode == 0)
				{
					Main.NewText("Berramyr has escaped to the Underworld, chase him down the Hell Hole!", (byte)255, (byte)255, (byte)255, false);
					hellHole(p);
					hellHoleMode = 1;
				}
				npc.timeLeft = 300;
				Main.npc[fokmyl].timeLeft = 1200;
				Main.npc[niremyl].timeLeft = 1200;
				Main.npc[lyndmyl].timeLeft = 1200;
				Main.npc[veiynamyl].timeLeft = 1200;
				npc.position = new Vector2(p.position.X - 3000, p.Center.Y);
				npc.velocity = new Vector2(0, 0);
			}
			
			if(p.position.Y >= (float)((Main.maxTilesY - 204) * 16))
			{
				hellHoleMode = 2;
				npc.dontTakeDamage = false;
				npc.alpha = 0;
				Main.npc[fokmyl].alpha = 0;
				Main.npc[niremyl].alpha = 0;
				Main.npc[lyndmyl].alpha = 0;
				Main.npc[veiynamyl].alpha = 0;
			}
				
			if(modeTime > 0 || coolDown < 1)
			{
				if(mode == 0)
				{
					flammenWheel(p);
				}
				if(mode == 1)
				{
					moveTo = new Vector2(p.Center.X, p.Center.Y - 300);
					spinToWin(moveTo, p);
				}
				if(mode == 2)
				{
					hellSwarmMissiles(p);
				}
				if(mode == 3)
				{
					bulletHell(p);
				}
				if(mode == 4)
				{
					entrapment(p);
				}
			}
			if(modeTime < 1)
			{
				Main.npc[fokmyl].ai[2] = 0;
				Main.npc[niremyl].ai[2] = 0;
				Main.npc[lyndmyl].ai[2] = 0;
				Main.npc[veiynamyl].ai[2] = 0;
				if(hellHoleMode != 1)
				{
					coolDown--;
				}
				firstTimeSet = 0;
				npc.rotation = 0f;
				npc.ai[1] = 0f;
				mode = -1;
				if(coolDown < 1)
				{
					if(p.position.Y >= (float)((Main.maxTilesY - 204) * 16))
					{
						mode = Main.rand.Next(0, 5);
					}
					else
					{
						mode = Main.rand.Next(0, 4);
					}
				}
			}
			npc.ai[0] += 1f;
			npc.localAI[0] += 1f;
			npc.netUpdate = true;
		}
		private void floatAround(Vector2 moveTo, Player p)
		{
			float fOne = (float)Main.rand.Next(-100, 100);
			float fTwo = (float)Main.rand.Next(-100, 100);
			float pX = p.Center.X - npc.Center.X;
			float pY = p.Center.Y - npc.Center.Y;
			float pDistance = (float)System.Math.Sqrt((double)(pX * pX + pY * pY));
			if(portCoolDown > 0)
			{
				portCoolDown--;
			}
			if(pDistance < 50 && portCoolDown == 0)
			{
				MMod.explosionEffect(npc, 4f);
				for(int i = 0; i < 255; i++)
				{
					Player players = Main.player[i];
					float dX = players.Center.X - npc.Center.X;
					float dY = players.Center.Y - npc.Center.Y;
					float playersDistance = (float)System.Math.Sqrt((double)dX * dX + dY * dY);
					if(playersDistance < 50)
					{
						p.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), 30, p.direction);
					}
				}
				int[] posNeg = {1, -1};
				float sX = posNeg[Main.rand.Next(0, 1)];
				float sY = posNeg[Main.rand.Next(0, 1)];
				float tDistance = 400f;
				npc.position.X = p.Center.X + (tDistance * sX) + fOne;
				npc.position.Y = p.Center.Y + (tDistance * sY) + fTwo;
				didOnce = 0;
				portCoolDown = 180;
			}
			float moveToX = moveTo.X - npc.Center.X + fOne;
			float moveToY = moveTo.Y - npc.Center.Y + fTwo;
			float distance = (float)System.Math.Sqrt((double)(moveToX * moveToX + moveToY * moveToY));
			float speed = 5f;
			if(pDistance > 500)
			{
				speed = 15f;
			}
			distance = speed / distance; 
			moveToX *= distance;
			moveToY *= distance;
			if(pDistance > 500)
			{
				pX += fOne * 2;
				pY += fTwo * 2;
				pX *= distance;
				pY *= distance;
			}
			if(didOnce == 0 || pDistance > 500)
			{
				npc.velocity.X = moveToX;
				npc.velocity.Y = moveToY;
				if(pDistance > 500)
				{
					npc.velocity.X = pX;
					npc.velocity.Y = pY;
				}
				didOnce = 1;
			}
			if(mode == 4)
			{
				flammenWheel(p);
			}
		}
		private void flammenWheel(Player p)
		{
			if(firstTimeSet == 0)
			{
				modeTime = 360;
				coolDown = 420;
				firstTimeSet = 1;
			}
			float degree = 10;
			double fireAngle1 = (double)((npc.ai[1] * degree) * (float) (Math.PI / 180));
			double fireAngle2 = (double)(((npc.ai[1] * degree) + 90) * (float) (Math.PI / 180));
			double fireAngle3 = (double)(((npc.ai[1] * degree) + 180) * (float) (Math.PI / 180));
			double fireAngle4 = (double)(((npc.ai[1] * degree) + 270) * (float) (Math.PI / 180));
			if(Main.netMode != 1)
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle1) * 12f, (float)Math.Sin(fireAngle1) * 15f, mod.ProjectileType("Berraflame"), 20, 3f, Main.myPlayer);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle2) * 12f, (float)Math.Sin(fireAngle2) * 15f, mod.ProjectileType("Berraflame"), 20, 3f, Main.myPlayer);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle3) * 12f, (float)Math.Sin(fireAngle3) * 15f, mod.ProjectileType("Berraflame"), 20, 3f, Main.myPlayer);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle4) * 12f, (float)Math.Sin(fireAngle4) * 15f, mod.ProjectileType("Berraflame"), 20, 3f, Main.myPlayer);
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 34);
			}
			if(mode == 0)
			{
				if(npc.ai[1] < 210)
				{
					npc.position.X = p.position.X - 750;
					npc.position.Y = p.position.Y - (int)((float)npc.height / 2f);
				}
				npc.velocity = new Vector2(0, 0);
			}
			npc.rotation += degree * (float)(Math.PI / 180);
			if(npc.ai[1] > 240 && mode == 0)
			{
				npc.velocity.X = 20f;
			}
			npc.ai[1] += 1f;
			modeTime--;
		}
		private void spinToWin(Vector2 moveTo, Player p)
		{
			float moveToX = moveTo.X - npc.Center.X;
			float moveToY = moveTo.Y - npc.Center.Y;
			float distance = (float)System.Math.Sqrt((double)(moveToX * moveToX + moveToY * moveToY));
			
			if(firstTimeSet == 0)
			{
				modeTime = 600;
				coolDown = 900;
				firstTimeSet = 1;
			}
			
			if(npc.Center.Y < p.Center.Y - 300 && npc.ai[1] < 241)
			{
				npc.ai[1] = 241;
			}
			else
			{
				if(npc.ai[1] < 240)
				{
					float speed = 15f;
					distance = speed / distance; 
					moveToX *= distance;
					moveToY *= distance;
					npc.velocity.X = moveToX;
					npc.velocity.Y = moveToY;
				}
			}
			
			if(npc.ai[1] > 240)
			{
				if(distance > 1000)
				{
					modeTime = 0;
					coolDown /= 2;
				}
				float degree = 3;
				double fireAngle1 = (double)((npc.ai[1] * degree) * (float) (Math.PI / 180));
				double fireAngle2 = (double)(((npc.ai[1] * degree) + 45) * (float) (Math.PI / 180));
				double fireAngle3 = (double)(((npc.ai[1] * degree) + 90) * (float) (Math.PI / 180));
				double fireAngle4 = (double)(((npc.ai[1] * degree) + 135) * (float) (Math.PI / 180));
				double fireAngle5 = (double)(((npc.ai[1] * degree) + 180) * (float) (Math.PI / 180));
				double fireAngle6 = (double)(((npc.ai[1] * degree) + 225) * (float) (Math.PI / 180));
				double fireAngle7 = (double)(((npc.ai[1] * degree) + 270) * (float) (Math.PI / 180));
				double fireAngle8 = (double)(((npc.ai[1] * degree) + 315) * (float) (Math.PI / 180));
				double fireAngle9 = (double)(((npc.ai[1] * degree) + 360) * (float) (Math.PI / 180));
				if(npc.ai[1] % 10 == 0 && Main.netMode != 1)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle1) * 12f, (float)Math.Sin(fireAngle1) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle2) * 12f, (float)Math.Sin(fireAngle2) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle3) * 12f, (float)Math.Sin(fireAngle3) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle4) * 12f, (float)Math.Sin(fireAngle4) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle5) * 12f, (float)Math.Sin(fireAngle5) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle6) * 12f, (float)Math.Sin(fireAngle6) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle7) * 12f, (float)Math.Sin(fireAngle7) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle8) * 12f, (float)Math.Sin(fireAngle8) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)Math.Cos(fireAngle9) * 12f, (float)Math.Sin(fireAngle9) * 15f, mod.ProjectileType("Berrashot"), 20, 3f, Main.myPlayer);
					Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
				}
				
				npc.velocity = new Vector2(0, 0);
				npc.rotation += degree * (float)(Math.PI / 180);
			}
			
			npc.ai[1] += 1f;
			modeTime--;
		}
		private void berrabeam(Player p)
		{
			float shootToX = p.position.X + (float)p.width * 0.5f - npc.Center.X; 
			float shootToY = p.position.Y - npc.Center.Y; 
			float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
			float speed = 15f / distance;
			shootToX *= speed; 
			shootToY *= speed; 
			if(Main.netMode != 1)
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shootToX, shootToY, mod.ProjectileType("Berrabeam"), 20, 3f, Main.myPlayer);
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 12);
			}
		}
		private void hellSwarmMissiles(Player p)
		{
			if(firstTimeSet == 0)
			{
				modeTime = 300;
				coolDown = 420;
				firstTimeSet = 1;
			}
			
			float fOne = Main.rand.Next(-100, 100);
			float fTwo = Main.rand.Next(-100, 100);
			
			npc.velocity = new Vector2(0, 0);
			
			if(npc.ai[1] % 8 == 0)
			{
				float shootToX = p.Center.X - npc.Center.X; 
				float shootToY = p.Center.Y - npc.Center.Y; 
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				float speed = 15f / distance;
				shootToX *= speed; 
				shootToY *= speed; 
				Projectile.NewProjectile(npc.Center.X + fOne, npc.Center.Y + fTwo, shootToX, shootToY, mod.ProjectileType("HellswarmMissile"), 20, 6f, Main.myPlayer, p.whoAmI, 0f);
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
			}
			
			npc.ai[1] += 1f;
			modeTime--;
		}
		private void bulletHell(Player p)
		{
			if(firstTimeSet == 0)
			{
				modeTime = 600;
				coolDown = 900;
				Main.npc[fokmyl].ai[2] = 1;
				Main.npc[niremyl].ai[2] = 1;
				Main.npc[lyndmyl].ai[2] = 1;
				Main.npc[veiynamyl].ai[2] = 1;
				npc.position.X = p.Center.X - npc.width/2;
				npc.position.Y = p.Center.Y - 900;
				npc.velocity = new Vector2(0, 0);
				
				Main.npc[fokmyl].ai[3] = npc.position.X - 400;
				Main.npc[lyndmyl].ai[3] = npc.position.X + 400;
				
				firstTimeSet = 1;
			}
			
			float fOne = Main.rand.Next(-100, 100);
			float fTwo = Main.rand.Next(-100, 100);
			int rand = Main.rand.Next(0, 5);
			if(npc.ai[1] % 180 == 0)
			{
				float shootToX = (npc.Center.X + fOne) - npc.Center.X; 
				float shootToY = (npc.Center.Y + fTwo) - npc.Center.Y; 
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				float speed = 10f / distance;
				shootToX *= speed; 
				shootToY *= speed; 
				int proj = Projectile.NewProjectile(npc.Center.X + fOne, npc.Center.Y + fTwo, shootToX, shootToY, mod.ProjectileType("Berraworks"), 20, 6f, Main.myPlayer, p.whoAmI, 0f);
				Main.projectile[proj].timeLeft = 90;
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
			}
			if(npc.ai[1] % 120 == 0)
			{
				int proj1 = Projectile.NewProjectile(npc.position.X, npc.Center.Y, 0, 15, mod.ProjectileType("Berrabullet"), 20, 0f, Main.myPlayer, 0f, 0f);
				int proj2 = Projectile.NewProjectile(npc.position.X + npc.width, npc.Center.Y, 0, 15, mod.ProjectileType("Berrabullet"), 20, 0f, Main.myPlayer, 0f, 0f);
				int proj3 = Projectile.NewProjectile(npc.position.X + (npc.width/4), npc.Center.Y, 0, 15, mod.ProjectileType("Berrabullet"), 20, 0f, Main.myPlayer, 0f, 0f);
				int proj4 = Projectile.NewProjectile(npc.position.X + (npc.width - (npc.width/4)), npc.Center.Y, 0, 15, mod.ProjectileType("Berrabullet"), 20, 0f, Main.myPlayer, 0f, 0f);
				Main.projectile[proj1].timeLeft = 90;
				Main.projectile[proj2].timeLeft = 90;
				Main.projectile[proj3].timeLeft = 90;
				Main.projectile[proj4].timeLeft = 90;
			}
			if(npc.ai[1] % 90 == 0)
			{
				float shootToX = p.Center.X - npc.Center.X; 
				float shootToY = p.Center.Y - npc.Center.Y;
				int angle = (int)(Math.Atan2((double)shootToY, (double)shootToX) * 180/Math.PI);
				for(int i = angle - 45; i <= angle + 45; i += 15)
				{
					float rad = (float)(i*(Math.PI/180));
					float speedY = (float)Math.Sin(rad) * 10f;
					float speedX = (float)Math.Cos(rad) * 10f;
					int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speedX, speedY, mod.ProjectileType("Berrabullet"), 20, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[proj1].timeLeft = 90;
					Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
				}
			}
			if(p.position.Y < npc.position.Y)
			{
				p.position.Y += 30;
			}
			
			npc.ai[1] += 1f;
			modeTime--;
		}
		private void entrapment(Player p)
		{
			if(firstTimeSet == 0)
			{
				modeTime = 900;
				coolDown = 1200;
				Main.npc[fokmyl].ai[2] = 2;
				Main.npc[niremyl].ai[2] = 2;
				Main.npc[lyndmyl].ai[2] = 2;
				Main.npc[veiynamyl].ai[2] = 2;
				
				Main.npc[fokmyl].ai[3] = 100;
				Main.npc[niremyl].ai[3] = 200;
				Main.npc[lyndmyl].ai[3] = 300;
				Main.npc[veiynamyl].ai[3] = 400;
				
				firstTimeSet = 1;
			}
			Vector2 moveTo = new Vector2(p.Center.X, p.Center.Y - 300);
			floatAround(moveTo, p);
			
		}
		private void hellHole(Player p)
		{
			int tPosX = (int)((npc.position.X - npc.width) / 16f); //Tile position X
			int tEndPointX = (int)((npc.position.X + (float)npc.width * 2) / 16f); //Tile end point X
				
			npc.dontTakeDamage = true;
			npc.alpha = 255;
			Main.npc[fokmyl].alpha = 255;
			Main.npc[niremyl].alpha = 255;
			Main.npc[lyndmyl].alpha = 255;
			Main.npc[veiynamyl].alpha = 255;
				
			if (tPosX < 0)
			{
				tPosX = 0;
			}
			if (tEndPointX > Main.maxTilesX)
			{
				tEndPointX = Main.maxTilesX;
			}
				
			for (int x = tPosX; x < tEndPointX; x++)
			{
				for (int y = 0; y < Main.maxTilesY; y++)
				{
					WorldGen.KillTile(x, y);
				}
			}
		}
		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if(!Main.npc[fokmyl].dontTakeDamage)
			{
				NPC fNPC = Main.npc[fokmyl];
				fNPC.life -= (int)(damage / 4);
				CombatText.NewText(new Rectangle((int)fNPC.position.X, (int)fNPC.position.Y, fNPC.width, fNPC.height), new Color(255, 60, 70, 255), string.Concat((int)damage / 4), false, true);
				damage -= damage / 4;
			}
			return true;
		}		
		
		
		
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write((short)didOnce);
			writer.Write((short)firstTimeSet);
			writer.Write((short)mode);
			writer.Write((short)modeTime);
			writer.Write((short)coolDown);
			writer.Write((short)portCoolDown);
			writer.Write((short)fokmyl);
			writer.Write((short)niremyl);
			writer.Write((short)lyndmyl);
			writer.Write((short)veiynamyl);
			writer.Write((short)hellHoleMode);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			didOnce = reader.ReadInt16();
			firstTimeSet = reader.ReadInt16();
			mode = reader.ReadInt16();
			modeTime = reader.ReadInt16();
			coolDown = reader.ReadInt16();
			portCoolDown = reader.ReadInt16();
			fokmyl = reader.ReadInt16();
			niremyl = reader.ReadInt16();
			lyndmyl = reader.ReadInt16();
			veiynamyl = reader.ReadInt16();
			hellHoleMode = reader.ReadInt16();
		}
	}
}