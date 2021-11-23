using RogueLibsCore;
using Roguewarts.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguewarts.Extensions
{
	public static class E_TraitBuilder
	{
		public static TraitBuilder Localize<TraitType>(this TraitBuilder builder) where TraitType : CustomTrait
		{
			TraitsLocalization traitsLocalization = LocalizationManager.Instance.TraitsLocalization;
			Dictionary<LanguageCode, TraitsLocalization.LocalizedTrait> localizedTraits = traitsLocalization.GetLocalization<TraitType>();
			builder.WithName(new CustomNameInfo(localizedTraits.ToDictionary(entry => entry.Key, entry => entry.Value.Name)));
			builder.WithDescription(new CustomNameInfo(localizedTraits.ToDictionary(entry => entry.Key, entry => entry.Value.Desc)));
			return builder;
		}
	}
}
