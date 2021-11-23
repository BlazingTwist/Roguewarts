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
	public class FocusedCasting_2 : CustomTrait
	{
		private const string name = nameof(FocusedCasting_2);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<FocusedCasting_2>()
					.Localize<FocusedCasting_2>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(true)
							.SetCharacterCreationCost(3)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<FocusedCasting_2>(new TraitInfo(name, traitBuilder)
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
