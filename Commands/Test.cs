using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace COFP.Commands
{
	public class Test : ModCommand
	{
		public override CommandType Type
		{
			get { return CommandType.Chat; }
		}

		public override string Command
		{
			get { return "test"; }
		}

		public override string Description 
		{
			get { return "Admin test for COFP, all recipes are dirt and instant respawns"; }
		}

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if(MMod.tileDestruction)
			{
				MMod.tileDestruction = false;
				Main.NewText("Test mode disabled.");
			}
			else
			{
				MMod.tileDestruction = true;
				Main.NewText("Test mode enabled.");
			}
		}
	}
}