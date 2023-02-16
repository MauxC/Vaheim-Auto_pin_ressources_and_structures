using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace ValheimMod
{
    [BepInPlugin("AutoPinRessourcesAndStructures", "Auto_pin_ressources_and_structures", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class ValheimMod : BaseUnityPlugin
    {
        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> namePins;
        public static ConfigEntry<bool> savePins;

        public static ConfigEntry<bool> pinTin;
        public static ConfigEntry<bool> saveTinPins;
        public static ConfigEntry<bool> nameTinPins;
        public static ConfigEntry<bool> pinCopper;
        public static ConfigEntry<bool> saveCopperPins;
        public static ConfigEntry<bool> nameCopperPins;
        public static ConfigEntry<bool> pinSilver;
        public static ConfigEntry<bool> saveSilverPins;
        public static ConfigEntry<bool> nameSilverPins;
        public static ConfigEntry<bool> pinIron;
        public static ConfigEntry<bool> saveIronPins;
        public static ConfigEntry<bool> nameIronPins;

        public static ConfigEntry<bool> pinCrypt;
        public static ConfigEntry<bool> saveCryptPins;
        public static ConfigEntry<bool> nameCryptPins;
        public static ConfigEntry<bool> pinSunkenCrypt;
        public static ConfigEntry<bool> saveSunkenCryptPins;
        public static ConfigEntry<bool> nameSunkenCryptPins;
        public static ConfigEntry<bool> pinTrollCave;
        public static ConfigEntry<bool> saveTrollCavePins;
        public static ConfigEntry<bool> nameTrollCavePins;
        public static ConfigEntry<bool> pinMountainCave;
        public static ConfigEntry<bool> saveMountainCavePins;
        public static ConfigEntry<bool> nameMountainCavePins;
        public static ConfigEntry<bool> pinDrakeNest;
        public static ConfigEntry<bool> saveDrakeNestPins;
        public static ConfigEntry<bool> nameDrakeNestPins;
        public static ConfigEntry<bool> pinStoneTower;
        public static ConfigEntry<bool> saveStoneTowerPins;
        public static ConfigEntry<bool> nameStoneTowerPins;
        public static ConfigEntry<bool> pinTarPit;
        public static ConfigEntry<bool> saveTarPitPins;
        public static ConfigEntry<bool> nameTarPitPins;
        public static ConfigEntry<bool> pinGoblinCamp;
        public static ConfigEntry<bool> saveGoblinCampPins;
        public static ConfigEntry<bool> nameGoblinCampPins;
        public static ConfigEntry<bool> pinStoneHenge;
        public static ConfigEntry<bool> saveStoneHengePins;
        public static ConfigEntry<bool> nameStoneHengePins;
        public static ConfigEntry<bool> pinDvergrTownEntrance;
        public static ConfigEntry<bool> saveDvergrTownEntrancePins;
        public static ConfigEntry<bool> nameDvergrTownEntrancePins;
        public static ConfigEntry<bool> pinGuardTower;
        public static ConfigEntry<bool> saveGuardTowerPins;
        public static ConfigEntry<bool> nameGuardTowerPins;
        public static ConfigEntry<bool> pinViaduct;
        public static ConfigEntry<bool> saveViaductPins;
        public static ConfigEntry<bool> nameViaductPins;
        public static ConfigEntry<bool> pinExcavation;
        public static ConfigEntry<bool> saveExcavationPins;
        public static ConfigEntry<bool> nameExcavationPins;
        public static ConfigEntry<bool> pinHarbour;
        public static ConfigEntry<bool> saveHarbourPins;
        public static ConfigEntry<bool> nameHarbourPins;
        public static ConfigEntry<bool> pinSkeleton;
        public static ConfigEntry<bool> saveSkeletonPins;
        public static ConfigEntry<bool> nameSkeletonPins;
        public static ConfigEntry<bool> pinDraugr;
        public static ConfigEntry<bool> saveDraugrPins;
        public static ConfigEntry<bool> nameDraugrPins;
        public static ConfigEntry<bool> pinGreydwarf;
        public static ConfigEntry<bool> saveGreydwarfPins;
        public static ConfigEntry<bool> nameGreydwarfPins;
        public static ConfigEntry<bool> pinSurtling;
        public static ConfigEntry<bool> saveSurtlingPins;
        public static ConfigEntry<bool> nameSurtlingPins;


        public static long counter = 0;

        private readonly Harmony harmony = new Harmony("AutoPinRessourcesAndStructures");

        public static Dictionary<Vector3, string> itemsPins = new Dictionary<Vector3, string>();

        public static List<Pin> pinsInfo = new List<Pin>();

        void Awake()
        {
            modEnabled = base.Config.Bind("General", "Enabled", defaultValue: true, "Enable the mod");
            namePins = base.Config.Bind("General", "NamePins", defaultValue: true, "Name pins according to their type");
            savePins = base.Config.Bind("General", "SavePins", defaultValue: true, "Save pins");
            if (!modEnabled.Value)
            {
                base.enabled = false;
                return;
            }

            pinTin = base.Config.Bind("Minerals - Tin", "PinTin", defaultValue: true, "Pin tin deposits");
            saveTinPins = base.Config.Bind("Minerals - Tin", "SaveTinPin", defaultValue: true, "Save tin deposits pins");
            nameTinPins = base.Config.Bind("Minerals - Tin", "NameTinPin", defaultValue: true, "Name tin deposits pins");
            pinCopper = base.Config.Bind("Minerals - Copper", "PinCopper", defaultValue: true, "Pin copper deposits");
            saveCopperPins = base.Config.Bind("Minerals - Copper", "SaveCopperPin", defaultValue: true, "Save copper deposits pins");
            nameCopperPins = base.Config.Bind("Minerals - Copper", "NameCopperPin", defaultValue: true, "Name copper deposits pins");
            pinSilver = base.Config.Bind("Minerals - Silver", "PinSilver", defaultValue: true, "Pin Silver deposits");
            saveSilverPins = base.Config.Bind("Minerals - Silver", "SaveSilverPin", defaultValue: true, "Save Silver deposits pins");
            nameSilverPins = base.Config.Bind("Minerals - Silver", "NameSilverPin", defaultValue: true, "Name Silver deposits pins");
            pinIron = base.Config.Bind("Minerals - Iron", "PinIron", defaultValue: true, "Pin Iron deposits");
            saveIronPins = base.Config.Bind("Minerals - Iron", "SaveIronPin", defaultValue: true, "Save Iron deposits pins");
            nameIronPins = base.Config.Bind("Minerals - Iron", "NameIronrPin", defaultValue: true, "Name Iron deposits pins");

            pinCrypt = base.Config.Bind("Structures - Crypt", "PinCrypt", defaultValue: true, "Pin Crypts");
            saveCryptPins = base.Config.Bind("Structures - Crypt", "SaveCryptPin", defaultValue: true, "Save Crypt pins");
            nameCryptPins = base.Config.Bind("Structures - Crypt", "NameCryptPin", defaultValue: true, "Name Crypt pins");
            pinSunkenCrypt = base.Config.Bind("Structures - Sunken Crypt", "PinSunkenCrypt", defaultValue: true, "Pin Sunken Crypts");
            saveSunkenCryptPins = base.Config.Bind("Structures - Sunken Crypt", "SaveSunkenCryptPin", defaultValue: true, "Save Sunken Crypt pins");
            nameSunkenCryptPins = base.Config.Bind("Structures - Sunken Crypt", "NameSunkenCryptPin", defaultValue: true, "Name Sunken Crypt pins");
            pinTrollCave = base.Config.Bind("Structures - Troll Cave", "PinTrollCave", defaultValue: true, "Pin Troll Caves");
            saveTrollCavePins = base.Config.Bind("Structures - Troll Cave", "SaveTrollCavePin", defaultValue: true, "Save Troll Cave pins");
            nameTrollCavePins = base.Config.Bind("Structures - Troll Cave", "NameTrollCavePin", defaultValue: true, "Name Troll Cave pins");
            pinMountainCave = base.Config.Bind("Structures - Mountain Cave", "PinMountainCave", defaultValue: true, "Pin Mountain Caves");
            saveMountainCavePins = base.Config.Bind("Structures - Mountain Cave", "SaveMountainCavePin", defaultValue: true, "Save Mountain Cave pins");
            nameMountainCavePins = base.Config.Bind("Structures - Mountain Cave", "NameMountainCavePin", defaultValue: true, "Name Mountain Cave pins");
            pinDrakeNest = base.Config.Bind("Structures - Drake Nest", "PinDrakeNest", defaultValue: true, "Pin Drake Nests");
            saveDrakeNestPins = base.Config.Bind("Structures - Drake Nest", "SaveDrakeNestPin", defaultValue: true, "Save Drake Nest pins");
            nameDrakeNestPins = base.Config.Bind("Structures - Drake Nest", "NameDrakeNestPin", defaultValue: true, "Name Drake Nest pins");
            pinStoneTower = base.Config.Bind("Structures - Stone Tower", "PinStoneTower", defaultValue: true, "Pin Stone Towers");
            saveStoneTowerPins = base.Config.Bind("Structures - Stone Tower", "SaveStoneTowerPin", defaultValue: true, "Save Stone Tower pins");
            nameStoneTowerPins = base.Config.Bind("Structures - Stone Tower", "NameStoneTowerPin", defaultValue: true, "Name Stone Tower pins");
            pinTarPit = base.Config.Bind("Structures - Tar Pit", "PinTarPit", defaultValue: true, "Pin Tar Pits");
            saveTarPitPins = base.Config.Bind("Structures - Tar Pit", "SaveTarPitPin", defaultValue: true, "Save Tar Pit pins");
            nameTarPitPins = base.Config.Bind("Structures - Tar Pit", "NameTarPitPin", defaultValue: true, "Name Tar Pit pins");
            pinGoblinCamp = base.Config.Bind("Structures - Goblin Camp", "PinGoblinCamp", defaultValue: true, "Pin Goblin Camps");
            saveGoblinCampPins = base.Config.Bind("Structures - Goblin Camp", "SaveGoblinCampPin", defaultValue: true, "Save Goblin Camp pins");
            nameGoblinCampPins = base.Config.Bind("Structures - Goblin Camp", "NameGoblinCampPin", defaultValue: true, "Name Goblin Camp pins");
            pinStoneHenge = base.Config.Bind("Structures - StoneHenge", "PinStoneHenge", defaultValue: true, "Pin StoneHenge");
            saveStoneHengePins = base.Config.Bind("Structures - StoneHenge", "SaveStoneHengePin", defaultValue: true, "Save StoneHenge pins");
            nameStoneHengePins = base.Config.Bind("Structures - StoneHenge", "NameStoneHengePin", defaultValue: true, "Name StoneHenge pins");
            pinDvergrTownEntrance = base.Config.Bind("Structures - Mine", "PinMine", defaultValue: true, "Pin Mines");
            saveDvergrTownEntrancePins = base.Config.Bind("Structures - Mine", "SaveMinePin", defaultValue: true, "Save Mine pins");
            nameDvergrTownEntrancePins = base.Config.Bind("Structures - Mine", "NameMinePin", defaultValue: true, "Name Mine pins");
            pinGuardTower = base.Config.Bind("Structures - Guard Tower", "PinGuardTower", defaultValue: true, "Pin Guard Towers");
            saveGuardTowerPins = base.Config.Bind("Structures - Guard Tower", "SaveGuardTowerPin", defaultValue: true, "Save Guard Tower pins");
            nameGuardTowerPins = base.Config.Bind("Structures - Guard Tower", "NameGuardTowerPin", defaultValue: true, "Name Guard Tower pins");
            pinViaduct = base.Config.Bind("Structures - Viaduct", "PinViaduct", defaultValue: true, "Pin Viaducts");
            saveViaductPins = base.Config.Bind("Structures - Viaduct", "SaveViaductPin", defaultValue: true, "Save Viaduct pins");
            nameViaductPins = base.Config.Bind("Structures - Viaduct", "NameViaductPin", defaultValue: true, "Name Viaduct pins");
            pinExcavation = base.Config.Bind("Structures - Excavation", "PinExcavation", defaultValue: true, "Pin Excavations");
            saveExcavationPins = base.Config.Bind("Structures - Excavation", "SaveExcavationPin", defaultValue: true, "Save Excavation pins");
            nameExcavationPins = base.Config.Bind("Structures - Excavation", "NameExcavationPin", defaultValue: true, "Name Excavation pins");
            pinHarbour = base.Config.Bind("Structures - Harbour", "PinHarbour", defaultValue: true, "Pin Harbour");
            saveHarbourPins = base.Config.Bind("Structures - Harbour", "SaveHarbourPin", defaultValue: true, "Save Harbour pins");
            nameHarbourPins = base.Config.Bind("Structures - Harbour", "NameHarbournPin", defaultValue: true, "Name Harbour pins");

            pinSkeleton = base.Config.Bind("Spawners - Skeleton", "PinSkeleton", defaultValue: true, "Pin Skeleton spawners");
            saveSkeletonPins = base.Config.Bind("Spawners - Skeleton", "SaveSkeletonPin", defaultValue: true, "Save Skeleton spawners pins");
            nameSkeletonPins = base.Config.Bind("Spawners - Skeleton", "NameSkeletonPin", defaultValue: true, "Name Skeleton spawners pins");
            pinDraugr = base.Config.Bind("Spawners - Draugr", "PinDraugr", defaultValue: true, "Pin Draugr spawners");
            saveDraugrPins = base.Config.Bind("Spawners - Draugr", "SaveDraugrPin", defaultValue: true, "Save Draugr spawners pins");
            nameDraugrPins = base.Config.Bind("Spawners - Draugr", "NameDraugrPin", defaultValue: true, "Name Draugr spawners pins");
            pinGreydwarf = base.Config.Bind("Spawners - Greydwarf", "PinGreydwarf", defaultValue: true, "Pin Greydwarf spawners");
            saveGreydwarfPins = base.Config.Bind("Spawners - Greydwarf", "SaveGreydwarfPin", defaultValue: true, "Save Greydwarf spawners pins");
            nameGreydwarfPins = base.Config.Bind("Spawners - Greydwarf", "NameGreydwarfPin", defaultValue: true, "Name Greydwarf spawners pins");
            pinSurtling = base.Config.Bind("Spawners - Surtling", "PinSurtling", defaultValue: true, "Pin Surtling spawners");
            saveSurtlingPins = base.Config.Bind("Spawners - Surtling", "SaveSurtlingPin", defaultValue: true, "Save Surtling spawners pins");
            nameSurtlingPins = base.Config.Bind("Spawners - Surtling", "NameSurtlingPin", defaultValue: true, "Name Surtling spawners pins");

            // Init pinsInfo
            pinsInfo.Add(new Pin("Copper", "$piece_deposit_copper", saveCopperPins.Value, nameCopperPins.Value, pinCopper.Value, Pin.PinType.Mineral));
            pinsInfo.Add(new Pin("Tin", "$piece_deposit_tin", saveTinPins.Value, nameTinPins.Value, pinTin.Value, Pin.PinType.Mineral));
            pinsInfo.Add(new Pin("Silver", "$piece_deposit_silvervein", saveSilverPins.Value, nameSilverPins.Value, pinSilver.Value, Pin.PinType.Mineral));
            pinsInfo.Add(new Pin("Silver", "$piece_deposit_silver", saveSilverPins.Value, nameSilverPins.Value, pinSilver.Value, Pin.PinType.Mineral));
            pinsInfo.Add(new Pin("Iron", "$piece_mudpile", saveIronPins.Value, nameIronPins.Value, pinIron.Value, Pin.PinType.Mineral));
            pinsInfo.Add(new Pin("Crypt", "Crypt", saveCryptPins.Value, nameCryptPins.Value, pinCrypt.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("SunkenCrypt", "SunkenCrypt", saveSunkenCryptPins.Value, nameSunkenCryptPins.Value, pinSunkenCrypt.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("TrollCave", "TrollCave", saveTrollCavePins.Value, nameTrollCavePins.Value, pinTrollCave.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("MountainCave", "MountainCave", saveMountainCavePins.Value, nameMountainCavePins.Value, pinMountainCave.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("DrakeNest", "DrakeNest", saveDrakeNestPins.Value, nameDrakeNestPins.Value, pinDrakeNest.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("StoneTower", "StoneTower", saveStoneTowerPins.Value, nameStoneTowerPins.Value, pinStoneTower.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("TarPit", "TarPit", saveTarPitPins.Value, nameTarPitPins.Value, pinTarPit.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("GoblinCamp", "GoblinCamp", saveGoblinCampPins.Value, nameGoblinCampPins.Value, pinGoblinCamp.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("StoneHenge", "StoneHenge", saveStoneHengePins.Value, nameStoneHengePins.Value, pinStoneHenge.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("Mine", "Mistlands_DvergrTownEntrance", saveDvergrTownEntrancePins.Value, nameDvergrTownEntrancePins.Value, pinDvergrTownEntrance.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("GuardTower", "Mistlands_GuardTower", saveGuardTowerPins.Value, nameGuardTowerPins.Value, pinGuardTower.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("Viaduct", "Mistlands_Viaduct", saveViaductPins.Value, nameViaductPins.Value, pinViaduct.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("Excavation", "Mistlands_Excavation", saveExcavationPins.Value, nameExcavationPins.Value, pinExcavation.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("Harbour", "Mistlands_Harbour", saveHarbourPins.Value, nameHarbourPins.Value, pinHarbour.Value, Pin.PinType.Structure));
            pinsInfo.Add(new Pin("Skeleton", "$enemy_skeletonspawner", saveSkeletonPins.Value, nameSkeletonPins.Value, pinSkeleton.Value, Pin.PinType.Spawner));
            pinsInfo.Add(new Pin("Draugr", "$enemy_draugrspawner", saveDraugrPins.Value, nameDraugrPins.Value, pinDraugr.Value, Pin.PinType.Spawner));
            pinsInfo.Add(new Pin("Greydwarf", "$enemy_greydwarfspawner", saveGreydwarfPins.Value, nameGreydwarfPins.Value, pinGreydwarf.Value, Pin.PinType.Spawner));
            pinsInfo.Add(new Pin("Surtling", "FireHole", saveSurtlingPins.Value, nameSurtlingPins.Value, pinSurtling.Value, Pin.PinType.Structure));

            harmony.PatchAll();
        }
        public static bool HasSimilarPin(List<Minimap.PinData> ___m_pins, Vector3 pos, Minimap.PinType type, string name, bool save)
        {
            foreach (Minimap.PinData pin in ___m_pins)
            {
                Vector3 posNew = pos;
                Vector3 pinPos = pin.m_pos;
                posNew.y = 0;
                pinPos.y = 0;
                if (pin.m_name == name && pin.m_type == type && pin.m_save == save && Vector3.Distance(posNew, pinPos) < 1f)
                {
                    return true;
                }
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(Minimap), "AddPin")]
    class Minimap_patch
    {
        private static bool Prefix(ref Minimap __instance, List<Minimap.PinData> ___m_pins, Vector3 pos, Minimap.PinType type, string name, bool save, bool isChecked)
        {
            if (ValheimMod.HasSimilarPin(___m_pins, pos, type, name, save))
                return false;
            else
                return true;
        }
    }

}