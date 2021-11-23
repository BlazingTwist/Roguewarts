using JetBrains.Annotations;
using RogueLibsCore;
using Roguewarts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
