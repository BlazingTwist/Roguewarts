using System.IO;
using BepInEx.Logging;
using UnityEngine;
using YamlDotNet.Serialization;

namespace Roguewarts.Localization
{
	public class LocalizationManager
	{
		private static readonly ManualLogSource logger = RWLogger.GetLogger();

		public static LocalizationManager Instance => _instance ?? (_instance = new LocalizationManager());
		private static LocalizationManager _instance;

		public AbilityLocalization AbilityLocalization { get; }
		public TraitLocalization TraitsLocalization { get; }

		private static ConfigType ReadFromFile<ConfigType>(IDeserializer deserializer, string path)
		{
			path = path.Replace('/', Path.DirectorySeparatorChar);
			if (!File.Exists(path))
			{
				logger.LogWarning($"Localization file not found at path: '{path}'");
				return default;
			}
			using (StreamReader reader = new StreamReader(path))
			{
				return deserializer.Deserialize<ConfigType>(reader);
			}
		}

		private LocalizationManager()
		{
			IDeserializer deserializer = new DeserializerBuilder().Build();

			string configBasePath = Application.dataPath + "/../BepInEx/config/";
			AbilityLocalization = ReadFromFile<AbilityLocalization>(deserializer, configBasePath + "Roguewarts_Abilities.yaml");
			TraitsLocalization = ReadFromFile<TraitLocalization>(deserializer, configBasePath + "Roguewarts_Traits.yaml");
		}
	}
}