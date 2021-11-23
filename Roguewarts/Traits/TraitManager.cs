using BepInEx.Logging;
using RogueLibsCore;
using Roguewarts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Roguewarts.Traits
{
	public static class TraitManager
	{
		private static readonly ManualLogSource logger = RWLogger.GetLogger();

		private static readonly Dictionary<Type, TraitInfo> registeredTraits = new Dictionary<Type, TraitInfo>();

		/// <summary>
		/// mapping of a TraitType 'a' to the TraitTypes 'b[]' that use 'a' as their upgrade
		/// </summary>
		private static readonly Dictionary<Type, List<Type>> upgradeDowngradeDict = new Dictionary<Type, List<Type>>();

		/// <summary>
		/// mapping of ConflictGroups to the TraitTypes in that ConflictGroup
		/// </summary>
		private static readonly Dictionary<ETraitConflictGroup, List<Type>> conflictGroupDict = new Dictionary<ETraitConflictGroup, List<Type>>();

		public static void RegisterTrait<TraitType>(TraitInfo info)
		{
			info.FinalizeInfo();
			registeredTraits.Add(typeof(TraitType), info);
			RegisterTraitUpgrades<TraitType>(info);
			RegisterTraitConflictGroup<TraitType>(info);
		}

		private static void RegisterTraitUpgrades<TraitType>(TraitInfo info)
		{
			if (info.Upgrade != null)
			{
				if (!upgradeDowngradeDict.ContainsKey(info.Upgrade))
				{
					upgradeDowngradeDict[info.Upgrade] = new List<Type>();
				}

				upgradeDowngradeDict[info.Upgrade].Add(typeof(TraitType));
			}
		}

		private static void RegisterTraitConflictGroup<TraitType>(TraitInfo info)
		{
			foreach (ETraitConflictGroup conflictGroup in info.ConflictGroups)
			{
				if (!conflictGroupDict.ContainsKey(conflictGroup))
				{
					conflictGroupDict[conflictGroup] = new List<Type>();
				}

				conflictGroupDict[conflictGroup].Add(typeof(TraitType));
			}
		}

		public static TraitInfo GetTraitInfo<TraitType>()
		{
			return GetTraitInfo(typeof(TraitType));
		}

		public static TraitInfo GetTraitInfo(Type traitType)
		{
			return registeredTraits.ContainsKey(traitType)
					? registeredTraits[traitType]
					: null;
		}

		/// <summary>
		/// Should be called *after* all of the custom Traits have been registered.
		/// </summary>
		public static void FinalizeTraits()
		{
			foreach (KeyValuePair<Type, TraitInfo> traitEntry in registeredTraits)
			{
				RegisterUpgrades(traitEntry.Key, traitEntry.Value);
				RegisterCancellations(traitEntry.Key, traitEntry.Value);
				RegisterRecommendations(traitEntry.Key, traitEntry.Value);
			}
		}

		/// <summary>
		/// Sets the `isUpgrade` and `upgrade` fields for the given trait.
		/// </summary>
		private static void RegisterUpgrades(Type traitType, TraitInfo traitInfo)
		{
			TraitUnlock unlock = traitInfo.TraitBuilder.Unlock;
			TraitInfo upgradeTraitInfo = GetTraitInfo(traitInfo.Upgrade);
			unlock.SetUpgrade(
					upgradeDowngradeDict.ContainsKey(traitType),
					upgradeTraitInfo?.Name
			);
		}

		/// <summary>
		/// Sets the cancellations for the given trait.
		/// </summary>
		private static void RegisterCancellations(Type traitType, TraitInfo traitInfo)
		{
			TraitUnlock unlock = traitInfo.TraitBuilder.Unlock;
			HashSet<string> cancellations = new HashSet<string>();

			// cancel all traits in this conflictGroup 
			if (traitInfo.ConflictGroups.Count > 0)
			{
				foreach (string cancelTrait in traitInfo.ConflictGroups
						.SelectMany(group => conflictGroupDict[group])
						.Where(type => type != traitType) // prevent trait from cancelling itself
						.Select(GetTraitInfo)
						.Where(info => info != null)
						.Select(info => info.Name))
				{
					cancellations.Add(cancelTrait);
				}
			}

			// make sure this trait cancels any downgrade-traits
			if (upgradeDowngradeDict.ContainsKey(traitType))
			{
				foreach (string cancelTrait in upgradeDowngradeDict[traitType]
						.Select(type => GetTraitInfo(type)?.Name)
						.Where(name => name != null))
				{
					cancellations.Add(cancelTrait);
				}
			}

			// make sure this trait cancels the upgrade trait
			if (traitInfo.Upgrade != null && registeredTraits.ContainsKey(traitInfo.Upgrade))
			{
				cancellations.Add(registeredTraits[traitInfo.Upgrade].Name);
			}

			// TODO conflicts with vanilla traits

			unlock.SetCancellations(cancellations);
		}

		/// <summary>
		/// Sets the recommendations for the given trait 
		/// </summary>
		private static void RegisterRecommendations(Type traitType, TraitInfo traitInfo)
		{
			TraitUnlock unlock = traitInfo.TraitBuilder.Unlock;
			HashSet<string> recommendations = new HashSet<string>();

			if (traitInfo.Recommendations.Count > 0)
			{
				foreach (string recommendTrait in traitInfo.Recommendations
						.Select(type => GetTraitInfo(type)?.Name)
						.Where(name => name != null))
				{
					recommendations.Add(recommendTrait);
				}
			}

			// TODO recommend vanilla traits

			unlock.SetRecommendations(recommendations);
		}

		public static bool IsPlayerTraitActive<TraitType>() =>
			GameController.gameController.agentList.Any(agent => agent.isPlayer != 0 && agent.HasTrait<TraitType>());

		public static bool IsPlayerTraitActive(string trait) =>
			GameController.gameController.agentList.Any(agent => agent.isPlayer != 0 && agent.HasTrait(trait));

		// TODO to be removed soon (tm)
		public static bool DoesPlayerHaveTraitFromList(Agent agent, List<string> traits)
		{
			logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} ( agent = '{agent.agentName}', traits = '{string.Join(", ", traits)}'");
			return traits.Any(agent.HasTrait);
		}
	}
}
