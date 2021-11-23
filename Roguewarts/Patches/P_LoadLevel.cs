using BepInEx.Logging;
using HarmonyLib;
using Roguewarts.Abilities.Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguewarts.Patches
{
	class P_LoadLevel
	{
		private static readonly ManualLogSource logger = RWLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Chronomantic Dilation Timescale reset
		/// </summary>
		/// <param name="__instance"></param>
		[HarmonyPostfix, HarmonyPatch(methodName: "SetupMore5_2", new Type[] { })]
		public static void SetupMore5_2_Postfix(LoadLevel __instance)
		{
			ChronomanticDilation.baseTimeScale = GameController.gameController.selectedTimeScale;
		}
	}
}
