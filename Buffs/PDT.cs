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
			DisplayName.SetDefault("Point Defense Turret");
			Description.SetDefault("A turret is protecting you from projectiles.");
			Main.vanityPet[this.Type] = true;
			MPlayer.pdt = true;
		}
	}
}