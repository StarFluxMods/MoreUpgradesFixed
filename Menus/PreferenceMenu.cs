using System.Collections.Generic;
using Kitchen;
using Kitchen.Modules;
using KitchenLib;
using KitchenLib.Preferences;
using UnityEngine;

namespace MoreUpgradesFixed.Menus
{
    public class PreferenceMenu : KLMenu<MenuAction>
    {
        public PreferenceMenu(bool isMainMenu, Transform container, ModuleList module_list) : base(container, module_list)
        {
            this.isMainMenu = isMainMenu;
        }

        private bool isMainMenu;

        private Option<bool> WORKSTATION_TO_FREEZER = new Option<bool>(new List<bool> { true, false }, Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.WORKSTATION_TO_FREEZER).Value, new List<string> { "Enabled", "Disabled" });

        private Option<bool> COMBINER_TO_PORTIONER = new Option<bool>(new List<bool> { true, false }, Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.COMBINER_TO_PORTIONER).Value, new List<string> { "Enabled", "Disabled" });

        //private Option<bool> TABLE_CYCLE = new Option<bool>(new List<bool> { true, false }, Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.TABLE_CYCLE).Value, new List<string> { "Enabled", "Disabled" });
        private Option<bool> SHOE_CYCLE = new Option<bool>(new List<bool> { true, false }, Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.SHOE_CYCLE).Value, new List<string> { "Enabled", "Disabled" });
        private Option<bool> UTILITY_CYCLE = new Option<bool>(new List<bool> { true, false }, Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.UTILITY_CYCLE).Value, new List<string> { "Enabled", "Disabled" });
        private Option<bool> MOP_CYCLE = new Option<bool>(new List<bool> { true, false }, Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.MOP_CYCLE).Value, new List<string> { "Enabled", "Disabled" });

        public override void Setup(int player_id)
        {
            base.Setup(player_id);

            if (!isMainMenu)
            {
                AddInfo("Unfortunately these preferences can only be changed from the Main Menu.");
            }
            else
            {
                AddLabel("Workstation <-> Freezer");
                AddSelect(WORKSTATION_TO_FREEZER);
                WORKSTATION_TO_FREEZER.OnChanged += delegate(object _, bool result)
                {
                    Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.WORKSTATION_TO_FREEZER).Set(result);
                    Mod.preferenceManager.Save();
                };

                New<SpacerElement>(true);

                AddLabel("Combiner <-> Portioner");
                AddSelect(COMBINER_TO_PORTIONER);
                COMBINER_TO_PORTIONER.OnChanged += delegate(object _, bool result)
                {
                    Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.COMBINER_TO_PORTIONER).Set(result);
                    Mod.preferenceManager.Save();
                };

                /*
                New<SpacerElement>(true);

                AddLabel("Table Cycle");
                AddSelect(TABLE_CYCLE);
                TABLE_CYCLE.OnChanged += delegate(object _, bool result)
                {
                    Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.TABLE_CYCLE).Set(result);
                    Mod.preferenceManager.Save();
                };
                */

                New<SpacerElement>(true);

                AddLabel("Shoe Cycle");
                AddSelect(SHOE_CYCLE);
                SHOE_CYCLE.OnChanged += delegate(object _, bool result)
                {
                    Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.SHOE_CYCLE).Set(result);
                    Mod.preferenceManager.Save();
                };

                New<SpacerElement>(true);

                AddLabel("Utility Cycle");
                AddSelect(UTILITY_CYCLE);
                UTILITY_CYCLE.OnChanged += delegate(object _, bool result)
                {
                    Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.UTILITY_CYCLE).Set(result);
                    Mod.preferenceManager.Save();
                };

                New<SpacerElement>(true);

                AddLabel("Mop Cycle");
                AddSelect(MOP_CYCLE);
                MOP_CYCLE.OnChanged += delegate(object _, bool result)
                {
                    Mod.preferenceManager.GetPreference<PreferenceBool>(Mod.MOP_CYCLE).Set(result);
                    Mod.preferenceManager.Save();
                };
            }

            New<SpacerElement>(true);
            New<SpacerElement>(true);

            AddButton(base.Localisation["MENU_BACK_SETTINGS"], delegate(int i)
            {
                Mod.preferenceManager.Save();
                RequestPreviousMenu();
            }, 0, 1f, 0.2f);
        }
    }
}