using RogueLibsCore;
using Roguewarts.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Roguewarts.Extensions
{
	public static class E_AbilityBuilder
	{
		public static AbilityBuilder Localize<AbilityType>(this AbilityBuilder builder) where AbilityType : CustomAbility
		{
			Dictionary<LanguageCode, AbilityLocalization.LocalizedAbility> localizedAbilities = LocalizationManager.Instance.AbilityLocalization?.GetLocalization<AbilityType>();
			if (localizedAbilities != null)
			{
				builder.WithName(new CustomNameInfo(localizedAbilities.ToDictionary(entry => entry.Key, entry => entry.Value?.Name)));
				builder.WithDescription(new CustomNameInfo(localizedAbilities.ToDictionary(entry => entry.Key, entry => entry.Value?.Desc)));
			}
			return builder;
		}
	}
}