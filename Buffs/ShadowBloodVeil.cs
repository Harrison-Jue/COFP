using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Buffs
{
	public class ShadowBloodVeil : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffName[this.Type] = "Shadow Blood Veil";
			Main.buffTip[this.Type] = "A veil of draining blood surrounds you.";
		}
		public override void Update(Player player, ref int buffIndex)
		{
			//Check for existance of ShadowBloodVeil projectile if there isn't one, spawn it.
			for(int i = 0; i < 1000; i++)
			{
				int type = Main.projectile[i].type;
				if(type != mod.ProjectileType("ShadowBloodVeil") && !Main.projectile[i].active)
				{
					MPlayer.hasProjectile = false;
				}
				else if(type == mod.ProjectileType("ShadowBloodVeil") && Main.projectile[i].active)
				{
					MPlayer.hasProjectile = true;
					break;
				}
			}
			if(!MPlayer.hasProjectile)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ShadowBloodVeil"), 0, 0, Main.myPlayer);
			}
		}
	}
}