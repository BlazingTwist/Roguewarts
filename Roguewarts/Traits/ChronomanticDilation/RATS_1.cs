using Google2u;
using JetBrains.Annotations;
using RogueLibsCore;
using Roguewarts.Extensions;

namespace Roguewarts.Traits.ChronomanticDilation
{
	public class RATS_1 : CustomTrait
	{
		private const string name = nameof(RATS_1);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<RATS_1>()
					.Localize<RATS_1>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailable(true)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(false)
							.SetCharacterCreationCost(3)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<RATS_1>(new TraitInfo(name, traitBuilder)
					.WithUpgrade(typeof(RATS_2))
			);
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
				return 1;
			}
			return 0;
		}
	}
}