using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
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
		
		public override void SetDefaults()
		{
			npc.name = "Berramyr";
			npc.friendly = false;
			npc.scale = 1.5f;
			npc.width = 106;
			npc.height = 106;
			npc.aiStyle = -1;
			npc.damage = 0;
			npc.defense = 40;
			npc.lifeMax = 35000;
			npc.soundHit = 4;
			npc.soundKilled = 14;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0;
			npc.value = Item.buyPrice(0, 10, 0, 0);
			music = MusicID.Boss1;
		}
		public override void AI()
		{
			if(npc.localAI[0] == 0f && Main.netMode != 1)
			{
				fokmyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Fokmyl"), 0, 0f, 0f, 255);
				niremyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Niremyl"), 0, 90f, 0f, 0f,  255);
				lyndmyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Lyndmyl"), 0, 180f, 0f, 0f, 255);
				veiynamyl = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Veiynamyl"), 0, 270f, 0f, 0f, 255);
				coolDown = 0;
				portCoolDown = 0;
				modeTime = 0;
				didOnce = 0;
				firstTimeSet = 0;
				npc.localAI[0] = 1f;
				npc.ai[0] = 0f;
				npc.ai[1] = 0f;
				mode = 3;
			}
			
			npc.TargetClosest(true);
			Player p = Main.player[npc.target];
			
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
				if(coolDown % 60 == 0)
				{
					berrabeam(p);
				}
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
			}
			if(modeTime < 1)
			{
				Main.npc[fokmyl].ai[2] = 0;
				Main.npc[niremyl].ai[2] = 0;
				Main.npc[lyndmyl].ai[2] = 0;
				Main.npc[veiynamyl].ai[2] = 0;
				coolDown--;
				firstTimeSet = 0;
				npc.rotation = 0f;
				npc.ai[1] = 0f;
				mode = -1;
				if(coolDown == 0)
				{
					mode = 3;
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
				for(int i = 0; i < 200; i++)
				{
					Player players = Main.player[i];
					float dX = players.Center.X - npc.Center.X;
					float dY = players.Center.Y - npc.Center.Y;
					float playersDistance = (float)System.Math.Sqrt((double)dX * dX + dY * dY);
					if(playersDistance < 50)
					{
						p.Hurt(30, p.direction);
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
			if(npc.ai[1] < 210)
			{
				npc.position.X = p.position.X - 750;
				npc.position.Y = p.position.Y - (int)((float)npc.height / 2f);
			}
			npc.velocity = new Vector2(0, 0);
			npc.rotation += degree * (float)(Math.PI / 180);
			if(npc.ai[1] > 240)
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
				if(npc.ai[1] % 5 == 0 && Main.netMode != 1)
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
				float shootToX = p.position.X + (float)p.width * 0.5f - npc.Center.X; 
				float shootToY = p.position.Y - npc.Center.Y; 
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				float speed = 15f / distance;
				shootToX *= speed; 
				shootToY *= speed; 
				Projectile.NewProjectile(npc.Center.X + fOne, npc.Center.Y + fTwo, shootToX, shootToY, mod.ProjectileType("HellswarmMissile"), 10, 6f, Main.myPlayer, p.whoAmI, 0f);
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
			if(npc.ai[1] % 90 == 0)
			{
				float shootToX = (npc.Center.X + fOne) - npc.Center.X; 
				float shootToY = (npc.Center.Y + fTwo) - npc.Center.Y; 
				float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
				float speed = 10f / distance;
				shootToX *= speed; 
				shootToY *= speed; 
				Projectile.NewProjectile(npc.Center.X + fOne, npc.Center.Y + fTwo, shootToX, shootToY, mod.ProjectileType("Berraworks"), 10, 6f, Main.myPlayer, p.whoAmI, 0f);
				Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
			}
			
			if(npc.ai[1] % 30 == 0)
			{
				for(int i = 225; i <= 315; i += 5)
				{
					float rad = (float)(i*(Math.PI/180));
					float speedY = (float)Math.Sin(rad) * 10f;
					float speedX = (float)Math.Cos(rad) * 10f;
					int proj1 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speedX, speedY, mod.ProjectileType("Berrabullet"), 10, 0f, Main.myPlayer, 0f, 0f);
					Main.projectile[proj1].timeLeft = 9999;
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
		}
	}
}