using BepInEx;
using HarmonyLib;

namespace ValheimMod
{
	[HarmonyPatch(typeof(Destructible), "Start")]
	class MinePatchSpawn
	{
		private static void Postfix(ref Destructible __instance)
		{
			HoverText comp = __instance.GetComponent<HoverText>();
			if (!comp)
				return;
			string compName = comp.m_text;
			bool found = false;
			Pin pin = new Pin();

			foreach (Pin pinInfo in ValheimMod.pinsInfo)
            {
				if (pinInfo.Type != Pin.PinType.Mineral || !pinInfo.Enabled)
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
