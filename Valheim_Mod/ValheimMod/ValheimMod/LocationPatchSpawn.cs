using HarmonyLib;
using UnityEngine;
using System.Text.RegularExpressions;

namespace ValheimMod
{
	[HarmonyPatch(typeof(Location), "Awake")]
	class LocationPatchSpawn
	{
		private static void Postfix(ref Location __instance)
		{
			Location comp = __instance.GetComponent<Location>();
			if (!comp)
				return;
			string compName = comp.name;
			bool found = false;
			Pin pin = new Pin();

			foreach (Pin pinInfo in ValheimMod.pinsInfo)
			{
				Regex rx = new Regex(@"^" + pinInfo.ComponentName + ".*");
				if (pinInfo.Type != Pin.PinType.Structure || !pinInfo.Enabled)
					continue;
				if (rx.Match(compName).Success)
				{
					found = true;
					pin = pinInfo;
					break;
				}
			}
			if (found && !ValheimMod.itemsPins.ContainsKey(comp.transform.position)) {
				Minimap.instance.AddPin(comp.transform.position, Minimap.PinType.Icon3, ValheimMod.namePins.Value ? pin.Label : null, ValheimMod.savePins.Value && pin.Save, false, 0L);
				ValheimMod.itemsPins.Add(comp.transform.position, pin.Label);
			}

		}
	}
}
