using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguewarts.Traits
{
	public class TraitInfo
	{
		public string Name { get; }
		public TraitBuilder TraitBuilder { get; }
		public Type Upgrade { get; private set; }
		public List<ETraitConflictGroup> ConflictGroups { get; } = new List<ETraitConflictGroup>();
		public List<Type> Recommendations { get; } = new List<Type>();

		private bool finalized;

		public TraitInfo(string name, TraitBuilder builder)
		{
			Name = name;
			TraitBuilder = builder;
		}

		private void AssertNotFinalized()
		{
			if (finalized)
				throw new NotSupportedException("cannot modify finalized TraitInfo!");
		}

		public TraitInfo WithUpgrade(Type upgradeTrait)
		{
			AssertNotFinalized();
			Upgrade = upgradeTrait;
			return this;
		}

		public TraitInfo WithConflictGroup(params ETraitConflictGroup[] conflictGroup)
		{
			AssertNotFinalized();
			ConflictGroups.AddRange(conflictGroup);
			return this;
		}

		public TraitInfo WithRecommendation(params Type[] recommendedTrait)
		{
			AssertNotFinalized();
			Recommendations.AddRange(recommendedTrait);
			return this;
		}

		public void FinalizeInfo()
		{
			finalized = true;
		}
	}
}
