using System.Collections.Generic;
using KitchenLib;
using KitchenMods;
using System.Reflection;
using Kitchen;
using KitchenData;
using KitchenLib.Event;
using KitchenLib.Preferences;
using KitchenLib.References;
using MoreUpgradesFixed.Menus;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;

namespace MoreUpgradesFixed
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.moreupgradesfixed";
        public const string MOD_NAME = "More Upgrades Fixed";
        public const string MOD_VERSION = "0.1.2";
        public const string MOD_AUTHOR = "StarFluxGames";
        public const string MOD_GAMEVERSION = ">=1.2.0";

        internal static KitchenLogger Logger;
        internal static PreferenceManager preferenceManager;

        internal static string WORKSTATION_TO_FREEZER = "WORKSTATION_TO_FREEZER";
        internal static string COMBINER_TO_PORTIONER = "COMBINER_TO_PORTIONER";
        internal static string SHOE_CYCLE = "SHOE_CYCLE";
        internal static string UTILITY_CYCLE = "UTILITY_CYCLE";
        internal static string MOP_CYCLE = "MOP_CYCLE";

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly())
        {
        }

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");

            if (preferenceManager.GetPreference<PreferenceBool>(WORKSTATION_TO_FREEZER).Value)
            {
                Appliance Workstation = GameData.Main.Get<Appliance>(ApplianceReferences.Workstation);
                Appliance Freezer = GameData.Main.Get<Appliance>(ApplianceReferences.Freezer);

                Workstation.Upgrades = new List<Appliance> { Freezer };
                Freezer.Upgrades = new List<Appliance> { Workstation };
            }

            if (preferenceManager.GetPreference<PreferenceBool>(COMBINER_TO_PORTIONER).Value)
            {
                Appliance Combiner = GameData.Main.Get<Appliance>(ApplianceReferences.Combiner);
                Appliance Portioner = GameData.Main.Get<Appliance>(ApplianceReferences.Portioner);

                Combiner.Upgrades = new List<Appliance> { Portioner };
                Portioner.Upgrades = new List<Appliance> { Combiner };
            }

            if (preferenceManager.GetPreference<PreferenceBool>(SHOE_CYCLE).Value)
            {
                Appliance ShoeRackWellies = GameData.Main.Get<Appliance>(ApplianceReferences.ShoeRackWellies);
                Appliance ShoeRackTrainers = GameData.Main.Get<Appliance>(ApplianceReferences.ShoeRackTrainers);
                Appliance ShoeRackWorkBoots = GameData.Main.Get<Appliance>(ApplianceReferences.ShoeRackWorkBoots);

                ShoeRackWellies.Upgrades = new List<Appliance> { ShoeRackTrainers };
                ShoeRackTrainers.Upgrades = new List<Appliance> { ShoeRackWorkBoots };
                ShoeRackWorkBoots.Upgrades = new List<Appliance> { ShoeRackWellies };
            }

            if (preferenceManager.GetPreference<PreferenceBool>(UTILITY_CYCLE).Value)
            {
                Appliance RollingPinProvider = GameData.Main.Get<Appliance>(ApplianceReferences.RollingPinProvider);
                Appliance SharpKnifeProvider = GameData.Main.Get<Appliance>(ApplianceReferences.SharpKnifeProvider);
                Appliance ScrubbingBrushProvider = GameData.Main.Get<Appliance>(ApplianceReferences.ScrubbingBrushProvider);

                RollingPinProvider.Upgrades = new List<Appliance> { SharpKnifeProvider };
                SharpKnifeProvider.Upgrades = new List<Appliance> { ScrubbingBrushProvider };
                ScrubbingBrushProvider.Upgrades = new List<Appliance> { RollingPinProvider };
            }

            if (preferenceManager.GetPreference<PreferenceInt>(MOP_CYCLE).Value == 0)
            {
                Appliance MopBucketLasting = GameData.Main.Get<Appliance>(ApplianceReferences.MopBucketLasting);
                Appliance MopBucketFast = GameData.Main.Get<Appliance>(ApplianceReferences.MopBucketFast);
                Appliance RobotMop = GameData.Main.Get<Appliance>(ApplianceReferences.RobotMop);

                Appliance RobotBuffer = GameData.Main.Get<Appliance>(ApplianceReferences.RobotBuffer);
                Appliance FloorBufferStation = GameData.Main.Get<Appliance>(ApplianceReferences.FloorBufferStation);

                MopBucketLasting.Upgrades = new List<Appliance> { MopBucketFast };
                MopBucketFast.Upgrades = new List<Appliance> { RobotMop };
                RobotMop.Upgrades = new List<Appliance> { RobotBuffer };
                FloorBufferStation.Upgrades = new List<Appliance> { RobotBuffer };
                RobotBuffer.Upgrades = new List<Appliance> { MopBucketLasting };
            }

            if (preferenceManager.GetPreference<PreferenceInt>(MOP_CYCLE).Value == 1)
            {
                Appliance MopBucketLasting = GameData.Main.Get<Appliance>(ApplianceReferences.MopBucketLasting);
                Appliance MopBucketFast = GameData.Main.Get<Appliance>(ApplianceReferences.MopBucketFast);
                Appliance RobotMop = GameData.Main.Get<Appliance>(ApplianceReferences.RobotMop);

                Appliance RobotBuffer = GameData.Main.Get<Appliance>(ApplianceReferences.RobotBuffer);
                Appliance FloorBufferStation = GameData.Main.Get<Appliance>(ApplianceReferences.FloorBufferStation);

                MopBucketLasting.Upgrades = new List<Appliance> { MopBucketFast };
                MopBucketFast.Upgrades = new List<Appliance> { RobotMop };
                RobotMop.Upgrades = new List<Appliance> { MopBucketLasting };

                RobotBuffer.Upgrades = new List<Appliance> { FloorBufferStation };
                FloorBufferStation.Upgrades = new List<Appliance> { RobotBuffer };
            }
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();
            preferenceManager = new PreferenceManager(MOD_GUID);
            preferenceManager.RegisterPreference(new PreferenceBool(WORKSTATION_TO_FREEZER, false));
            preferenceManager.RegisterPreference(new PreferenceBool(COMBINER_TO_PORTIONER, false));
            preferenceManager.RegisterPreference(new PreferenceBool(SHOE_CYCLE, false));
            preferenceManager.RegisterPreference(new PreferenceBool(UTILITY_CYCLE, false));
            preferenceManager.RegisterPreference(new PreferenceInt(MOP_CYCLE, 0));
            preferenceManager.Load();
            preferenceManager.Save();

            Events.MainMenuView_SetupMenusEvent += (s, args) => { args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu), new PreferenceMenu(true, args.instance.ButtonContainer, args.module_list) }); };

            Events.PlayerPauseView_SetupMenusEvent += (s, args) => { args.addMenu.Invoke(args.instance, new object[] { typeof(PreferenceMenu), new PreferenceMenu(false, args.instance.ButtonContainer, args.module_list) }); };

            ModsPreferencesMenu<MenuAction>.RegisterMenu(MOD_NAME, typeof(PreferenceMenu), typeof(MenuAction));
        }
    }
}