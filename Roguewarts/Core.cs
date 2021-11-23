using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using Roguewarts.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace Roguewarts
{
	[BepInPlugin(ModInfo.BepInExPluginId, ModInfo.Title, ModInfo.Version)]
	[BepInProcess("StreetsOfRogue.exe")]
	[BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
	public class Core : BaseUnityPlugin
    {
		public const bool debugMode = true;

		public static ManualLogSource ConsoleMessage;
		public static BaseUnityPlugin MainInstance;

		public void Awake()
		{
			MainInstance = this;
			ConsoleMessage = Logger;

			new Harmony(ModInfo.BepInExPluginId).PatchAll(GetType().Assembly);

			RogueLibs.LoadFromAssembly();
		}

		public static void Log(string logMessage) =>
			ConsoleMessage.LogMessage(logMessage);
	}
	public static class HeaderTools
	{
		private static GameController GC => GameController.gameController;
		private static void Log(string logMessage) => Core.Log(logMessage);

		public static void AddDictionary(Dictionary<PlayfieldObject, bool> dict, PlayfieldObject objectReal, bool defaultValue)
		{
			Log("AddDictionaryBool");

			// May need to force types here

			if (!dict.ContainsKey(objectReal))
				dict.Add(objectReal, defaultValue);
			else
				dict[objectReal] = defaultValue;
		}

		public static void AddDictionary(Dictionary<PlayfieldObject, string> dict, PlayfieldObject objectReal, string defaultValue)
		{
			Log("AddDictionary");

			// May need to force types here

			if (!dict.ContainsKey(objectReal))
				dict.Add(objectReal, defaultValue);
			else
				dict[objectReal] = defaultValue;
		}

		public static T GetMethodWithoutOverrides<T>(this MethodInfo method, object callFrom)
				where T : Delegate
		{
			IntPtr ptr = method.MethodHandle.GetFunctionPointer();
			return (T)Activator.CreateInstance(typeof(T), callFrom, ptr);
		}

		public static void InvokeRepeating(object instance, string method, float delay, float interval)
		{
			MethodInfo methodAccessed = AccessTools.Method(instance.GetType(), method);
			_ = InvokeRepeatingAsync(instance, methodAccessed, (int)Mathf.Floor(delay * 1000), (int)Mathf.Floor(interval * 1000));
		}

		private static async Task InvokeRepeatingAsync(object instance, MethodInfo method, int delay, int interval)
		{
			await Task.Delay(delay);

			while (true)
			{
				method.Invoke(instance, new object[0]);
				await Task.Delay(interval);
			}
		}

		public static T RandomFromList<T>(List<T> list)
		{
			System.Random rnd = new System.Random();

			return list[rnd.Next(list.Count)];
		}

		public static void Set(this object obj, params Func<string, object>[] hash)
		{
			foreach (Func<string, object> member in hash)
			{
				var propertyName = member.Method.GetParameters()[0].Name;
				var propertyValue = member(string.Empty);
				obj.GetType()
						.GetProperty(propertyName)
						.SetValue(obj, propertyValue, null);
			}
		}

		public static void SayDialogue(Agent agent, string customNameInfo, string vNameType)
		{
			string text = GC.nameDB.GetName(customNameInfo, vNameType);
			agent.Say(text);
		}

		public static void SayDialogue(ObjectReal objectReal, string customNameInfo, string vNameType)
		{
			string text = GC.nameDB.GetName(customNameInfo, vNameType);
			objectReal.Say(text);
		}
	}
	public static class RWLogger
	{
		private static string GetLoggerName(Type containingClass) =>
			$"Roguewarts_{containingClass.Name}";

		public static ManualLogSource GetLogger()
		{
			Type containingClass = new StackFrame(1, false).GetMethod().ReflectedType;
			return BepInEx.Logging.Logger.CreateLogSource(GetLoggerName(containingClass));
		}
	}
}
