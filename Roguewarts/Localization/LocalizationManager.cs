using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Roguewarts.Localization
{
	public class LocalizationManager
	{
		public static LocalizationManager Instance => _instance ?? (_instance = new LocalizationManager());
		private static LocalizationManager _instance;

		public AbilityLocalization AbilityLocalization { get; }
		public TraitLocalization TraitsLocalization { get; }

		private LocalizationManager()
		{
			IDeserializer deserializer = new DeserializerBuilder().Build(); 

			AbilityLocalization = deserializer.Deserialize<AbilityLocalization>(new StreamReader("BepInEx/config/Roguewarts_Abilities.yaml"));
			TraitsLocalization = deserializer.Deserialize<TraitLocalization>(new StreamReader("BepInEx/config/Roguewarts_Traits.yaml"));
		}
	}
} 