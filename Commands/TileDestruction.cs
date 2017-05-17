using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Commands
{
	public class TileDestruction : ModCommand
	{
		public override CommandType Type
		{
			get { return CommandType.Chat; }
		}

		public override string Command
		{
			get { return "tiledestruction"; }
		}

		public override string Description 
		{
			get { return "Whether COFP projectiles destroy tiles or not"; }
		}

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if(MMod.tileDestruction)
			{
				MMod.tileDestruction = false;
				Main.NewText("Tile Destruction disabled.");
			}
			else
			{
				MMod.tileDestruction = true;
				Main.NewText("Tile Destruction enabled.");
			}
		}
	}
}