using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using BepInEx.Logging;

namespace Roguewarts.Localization
{
	public class AbilityLocalization
	{
		private static readonly ManualLogSource logger = RWLogger.GetLogger();
		[UsedImplicitly] public Dictionary<string, Dictionary<LanguageCode, LocalizedAbility>> abilities;

		public Dictionary<LanguageCode, LocalizedAbility> GetLocalization<AbilityType>()
		{
			string id = typeof(AbilityType).Name;
			if (!abilities.ContainsKey(id))
			{
				logger.LogWarning($"AbilityLocalization did not find Localization for ID: '{id}'");
				return null;
			}
			return abilities[id];
		}

		[UsedImplicitly]
		public class LocalizedAbility
		{
			public string Name { get; set; }
			public string Desc { get; set; }
		}
	}
}