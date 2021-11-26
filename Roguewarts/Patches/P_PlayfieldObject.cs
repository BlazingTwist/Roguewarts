using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using Roguewarts.Abilities.Magic;
using Roguewarts.Traits.ChronomanticDilation;
using HarmonyLib;
using JetBrains.Annotations;

namespace Roguewarts.Patches
{
	[HarmonyPatch(declaringType: typeof(PlayfieldObject))]
	public static class P_PlayfieldObject
	{
		private static readonly ManualLogSource logger = RWLogger.GetLogger();

		[UsedImplicitly]
		private static int ApplyLuckBonus(int baseBonus, string luckType, PlayfieldObject playfieldObject)
		{
			Agent agent = playfieldObject.playfieldObjectAgent;
			int ratsMultiplier = RATS_1.GetLuckMultiplier(luckType, agent) + RATS_2.GetLuckMultiplier(luckType, agent);
			int cdMultiplier = ChronomanticDilation.GetLuckMultiplier(agent);
			return baseBonus * ratsMultiplier * cdMultiplier;
		}
		
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(PlayfieldObject.DetermineLuck), argumentTypes: new[] { typeof(int), typeof(string), typeof(bool) })]
		private static IEnumerable<CodeInstruction> DetermineLuck_Transpiler(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			MethodInfo method_applyLuckBonus = SymbolExtensions.GetMethodInfo(() => ApplyLuckBonus(0, null, null));

			CodeReplacementPatch luckBonusPatch = new CodeReplacementPatch(
					expectedMatches: 19,
					prefixInstructionSequence: new List<CodeInstruction>
					{
							new CodeInstruction(OpCodes.Ldc_R4),
							new CodeInstruction(OpCodes.Stloc_S, 1),
							new CodeInstruction(OpCodes.Ldc_R4),
							new CodeInstruction(OpCodes.Stloc_S, 2),
							new CodeInstruction(OpCodes.Ldc_R4),
							new CodeInstruction(OpCodes.Stloc_S, 3),
							new CodeInstruction(OpCodes.Ldc_R4),
							new CodeInstruction(OpCodes.Stloc_S, 4),
							new CodeInstruction(OpCodes.Ldc_R4),
							new CodeInstruction(OpCodes.Stloc_S, 5),
					},
					insertInstructionSequence: new List<CodeInstruction>
					{
							new CodeInstruction(OpCodes.Ldloc_S, 5), // num6
							new CodeInstruction(OpCodes.Ldarg_2), // num6, luckType
							new CodeInstruction(OpCodes.Ldarg_0), // num6, luckType, playFieldObject
							new CodeInstruction(OpCodes.Call, method_applyLuckBonus), // luckBonus
							new CodeInstruction(OpCodes.Ldloc_0), // luckBonus, num
							new CodeInstruction(OpCodes.Add), // summed
							new CodeInstruction(OpCodes.Stloc_0)
					}
			);
			luckBonusPatch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}