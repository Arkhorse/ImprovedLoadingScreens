﻿using Il2CppSystem.Collections.Generic;
using JetBrains.Annotations;
using ModSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImprovedLoadingScreens
{
    public enum Active
    {
        Disabled, Enabled
    }

    class CustomSettings : JsonModSettings
    {

        [Section("Mod Options")]

        [Name("Mod Active")]
        [Description("Enable or Disable this mod")]
        [Choice("Disabled", "Enabled")]
        public Active active = Active.Enabled;

        [Section("Hints")]

        [Name("Enable Hints")]
        [Description("Enable or Disable loading screen hints altogether.")]
        [Choice("Disabled", "Enabled")]
        public bool hints = true;

        [Name("Custom Hints")]
        [Description("Enabled or Disable custom loading screen hints from this mod.")]
        [Choice("Disabled", "Enabled")]
        public bool cHints = true;

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            if (field.Name == nameof(active) ||
               field.Name == nameof(hints))
            {
                RefreshSections();
            }
        }

        internal void RefreshSections()
        {


            SetFieldVisible(nameof(hints), Settings.settings.active != Active.Disabled);

            SetFieldVisible(nameof(cHints), Settings.settings.active != Active.Disabled && hints);


        }

    }

    internal static class Settings
    {

        public static CustomSettings settings = new CustomSettings();

        public static void onLoad()
        {
            settings.AddToModSettings("Improved Loading Screens", MenuType.Both);
            settings.RefreshSections();
        }


    }


}
