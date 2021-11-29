using RogueLibsCore;
using Roguewarts.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Roguewarts.Extensions
{
	public static class E_TraitBuilder
	{
		public static TraitBuilder Localize<TraitType>(this TraitBuilder builder) where TraitType : CustomTrait
		{
			Dictionary<LanguageCode, TraitLocalization.LocalizedTrait> localizedTraits = LocalizationManager.Instance.TraitsLocalization?.GetLocalization<TraitType>();
			if (localizedTraits != null)
			{
				builder.WithName(new CustomNameInfo(localizedTraits.ToDictionary(entry => entry.Key, entry => entry.Value?.Name)));
				builder.WithDescription(new CustomNameInfo(localizedTraits.ToDictionary(entry => entry.Key, entry => entry.Value?.Desc)));
			}
			return builder;
		}
	}
}