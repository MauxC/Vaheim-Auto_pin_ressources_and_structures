using HarmonyLib;
using UnityEngine;

namespace ValheimMod
{
	[HarmonyPatch(typeof(SpawnArea), "Awake")]
	class SpawnerPatchSpawn
	{
		private static void Postfix(ref SpawnArea __instance)
		{
			HoverText comp = __instance.GetComponent<HoverText>();
			if (!comp)
				return;
			string compName = comp.m_text;
			bool found = false;
			Pin pin = new Pin();

			foreach (Pin pinInfo in ValheimMod.pinsInfo)
			{
				if (pinInfo.Type != Pin.PinType.Spawner || !pinInfo.Enabled)
					continue;
				if (compName == pinInfo.ComponentName)
				{
					found = true;
					pin = pinInfo;
					break;
				}
			}

			if (found && !ValheimMod.itemsPins.ContainsKey(comp.transform.position))
			{
				Minimap.instance.AddPin(comp.transform.position, Minimap.PinType.Icon3, ValheimMod.namePins.Value ? pin.Label : null, ValheimMod.savePins.Value && pin.Save, false, 0L);
				ValheimMod.itemsPins.Add(comp.transform.position, pin.Label);
			}
		}
	}
}
