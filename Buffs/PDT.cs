using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace COFP.Buffs
{
	public class PDT : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffName[this.Type] = "Point Defense Turret";
			Main.buffTip[this.Type] = "A turret protecting you from projectiles.";
			Main.vanityPet[this.Type] = true;
			MPlayer.pdt = true;
		}
	}
}