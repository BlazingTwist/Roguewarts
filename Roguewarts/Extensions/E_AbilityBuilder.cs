using RogueLibsCore;
using Roguewarts.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguewarts.Extensions
{
	public static class E_AbilityBuilder
	{
		public static AbilityBuilder Localize<AbilityType>(this AbilityBuilder builder) where AbilityType : CustomAbility
		{
			AbilityLocalization abilityLocalization = LocalizationManager.Instance.AbilityLocalization;
			Dictionary<LanguageCode, AbilityLocalization.LocalizedAbility> localizedAbilities = abilityLocalization.GetLocalization<AbilityType>();
			builder.WithName(new CustomNameInfo(localizedAbilities.ToDictionary(entry => entry.Key, entry => entry.Value.Name)));
			builder.WithDescription(new CustomNameInfo(localizedAbilities.ToDictionary(entry => entry.Key, entry => entry.Value.Desc)));
			return builder;
		}
	}
}
