using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using BepInEx.Logging;

namespace Roguewarts.Localization
{
	public class TraitLocalization
	{
		private static readonly ManualLogSource logger = RWLogger.GetLogger();
		[UsedImplicitly] public Dictionary<string, Dictionary<LanguageCode, LocalizedTrait>> traits;

		public Dictionary<LanguageCode, LocalizedTrait> GetLocalization<TraitType>()
		{
			string id = typeof(TraitType).Name;
			if (!traits.ContainsKey(id))
			{
				logger.LogWarning($"TraitLocalization did not find Localization for ID: '{id}'");
				return null;
			}
			return traits[id];
		}

		[UsedImplicitly]
		public class LocalizedTrait
		{
			public string Name { get; set; }
			public string Desc { get; set; }
		}
	}
}