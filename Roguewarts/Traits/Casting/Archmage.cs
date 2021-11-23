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
	public class Archmage : CustomTrait
	{
		private const string name = nameof(Archmage);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<Archmage>()
					.Localize<Archmage>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(true)
							.SetCharacterCreationCost(3)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<Archmage>(new TraitInfo(name, traitBuilder)
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
