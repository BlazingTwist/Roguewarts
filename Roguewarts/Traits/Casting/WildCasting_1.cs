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
	public class WildCasting_1 : CustomTrait
	{
		private const string name = nameof(WildCasting_1);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<WildCasting_1>()
					.Localize<WildCasting_1>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(true)
							.SetCharacterCreationCost(3)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<WildCasting_1>(new TraitInfo(name, traitBuilder)
					.WithUpgrade(typeof(WildCasting_2))
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
