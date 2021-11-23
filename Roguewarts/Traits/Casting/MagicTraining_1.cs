using JetBrains.Annotations;
using RogueLibsCore;
using Roguewarts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguewarts.Traits.Casting
{
	public class MagicTraining_1 : CustomTrait
	{
		private const string name = nameof(MagicTraining_1);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<MagicTraining_1>()
					.Localize<MagicTraining_1>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(true)
							.SetCharacterCreationCost(3)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<MagicTraining_1>(new TraitInfo(name, traitBuilder)
					.WithUpgrade(typeof(MagicTraining_2))
					.WithConflictGroup(
							ETraitConflictGroup.Archmage_WildCasting,
							ETraitConflictGroup.MagicCasting
					)
			);
		}

		public override void OnAdded() { }

		public override void OnRemoved() { }
	}
}
