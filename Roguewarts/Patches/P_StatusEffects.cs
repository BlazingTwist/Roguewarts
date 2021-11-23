using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Roguewarts.Abilities;

namespace Roguewarts.Patches
{
	[HarmonyPatch(declaringType: (typeof(StatusEffects)))]
	class P_StatusEffects
	{
		[HarmonyPostfix, HarmonyPatch(methodName: nameof(StatusEffects.GiveSpecialAbility), argumentTypes: new[] { typeof(string) })]
		private static void GiveSpecialAbility_Postfix(string abilityName, StatusEffects __instance)
		{
			if (__instance.agent.inventory.equippedSpecialAbility != null)
			{
				InvItem ability = __instance.agent.inventory.equippedSpecialAbility;
				Agent agent = __instance.agent;

				string[] magicAbilities =
				{
					cSpecialAbility.ChronomanticDilation,
					cSpecialAbility.PyromanticJet,
					cSpecialAbility.TelemanticBlink
				};

				if (magicAbilities.Contains(abilityName))
				{
					ability.otherDamage = 0; // Bitwise variables

					ability.initCount = AbilityManager.CalcMaxMana(agent);
					ability.maxAmmo = AbilityManager.CalcMaxMana(agent);
					ability.rechargeAmountInverse = AbilityManager.CalcMaxMana(agent);
				}
			}
		}

	}
}
