using RogueLibsCore;
using Roguewarts.Abilities.Magic;
using Roguewarts.Traits.ChronomanticDilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace Roguewarts.Patches
{
	[HarmonyPatch(declaringType: typeof(PlayfieldObject))]
	class P_PlayfieldObject
	{
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(PlayfieldObject.DetermineLuck), new[] { typeof(int), typeof(string), typeof(bool) })]
		public static void PlayfieldObject_DetermineLuck(int originalLuck, string luckType, bool cancelStatusEffects, PlayfieldObject __instance, ref int __result)
		{
			Agent agent = __instance.playfieldObjectAgent;

			if (agent.HasTrait<RATS_1>() || agent.HasTrait<RATS_2>())
			{
				int bonus =
					luckType == "CritChance" ? 3 :
					luckType == vTrait.UnCrits ||
					luckType == vTrait.Kneecapper ? 4 :
					luckType == vTrait.Butterfingerer ||
					luckType == "GunAim" ? 5 :
					0;

				if (agent.HasTrait<RATS_2>())
					bonus *= 2;

				if (agent.isPlayer != 0 && agent.specialAbility == cSpecialAbility.ChronomanticDilation)
					if (ChronomanticDilation.MSA_CD_IsCast(agent))
						bonus *= 2;

				__result = Mathf.Clamp(__result + bonus, 0, 100);
			}
		}
	}
}
