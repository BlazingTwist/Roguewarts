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
	public class ManaBattery_2 : CustomTrait
	{
		private const string name = nameof(ManaBattery_2);

		[RLSetup]
		[UsedImplicitly]
		private static void Setup()
		{
			TraitBuilder traitBuilder = RogueLibs.CreateCustomTrait<ManaBattery_2>()
					.Localize<ManaBattery_2>()
					.WithUnlock(new TraitUnlock(name, true)
							.SetAvailableInCharacterCreation(true)
							.SetCantLose(true)
							.SetCantSwap(true)
							.SetCharacterCreationCost(3)
							.SetEnabled(true)
					);

			TraitManager.RegisterTrait<ManaBattery_2>(new TraitInfo(name, traitBuilder)
					.WithConflictGroup(
					)
			);
		}

		public override void OnAdded() { }

		public override void OnRemoved() { }
	}
}
