﻿using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Roguewarts.Localization
{
	public class AbilityLocalization
	{
		[UsedImplicitly] public Dictionary<string, Dictionary<LanguageCode, LocalizedAbility>> abilities;

		public Dictionary<LanguageCode, LocalizedAbility> GetLocalization<AbilityType>()
		{
			string id = typeof(AbilityType).Name;
			if (!abilities.ContainsKey(id))
			{
				Debug.LogWarning("TraitsLocalization did not find Localization for ID: '" + id + "'");
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
