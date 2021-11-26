using RogueLibsCore;
using Google2u;
using JetBrains.Annotations;
using Roguewarts.Extensions;

namespace Roguewarts.Traits.ChronomanticDilation
{
	public class RATS_2 : CustomTrait
	{
		private const string name = nameof(RATS_2);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<RATS_2>()
					.Localize<RATS_2>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailable(false)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(true)
							.SetCharacterCreationCost(12)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<RATS_2>(new TraitInfo(name, traitBuilder));
		}

		public override void OnAdded() { }

		public override void OnRemoved() { }
		
		public static int GetLuckMultiplier(string luckType, Agent agent)
		{
			if (agent.HasTrait<RATS_1>()
					&& (luckType == "CritChance"
							|| luckType == nameof(StatusEffectNameDB.rowIds.ChanceToSlowEnemies)
							|| luckType == nameof(StatusEffectNameDB.rowIds.ChanceAttacksDoZeroDamage)
							|| luckType == nameof(StatusEffectNameDB.rowIds.ChanceToKnockWeapons)
							|| luckType == "GunAim"))
			{
				return 2;
			}
			return 0;
		}
	}
}
