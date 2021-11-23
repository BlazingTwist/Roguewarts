using System;
using System.Collections.Generic;
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
		public TraitsLocalization TraitsLocalization { get; }

		private LocalizationManager()
		{
			IDeserializer deserializer = new DeserializerBuilder().Build();
			// TODO figure out path for localizationFile.
			AbilityLocalization = deserializer.Deserialize<AbilityLocalization>("");
			TraitsLocalization = deserializer.Deserialize<TraitsLocalization>("");
		}
	}
}
