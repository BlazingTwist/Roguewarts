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
			string path = "BepInEx/config/Roguewarts_Traits.yaml";

			using (StreamReader reader = new StreamReader(path))
			{
				AbilityLocalization = deserializer.Deserialize<AbilityLocalization>(reader);
				TraitsLocalization = deserializer.Deserialize<TraitLocalization>(reader);
			}
		}
	}
}