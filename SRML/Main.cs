﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using SRML.Utils;
using UnityEngine;

namespace SRML
{
    internal static class Main
    {
        private static bool isPreInitialized;
        static void PreLoad()
        {
            if (isPreInitialized) return;
            isPreInitialized = true;
            Debug.Log("VeesusTools has successfully invaded the game!");
            HarmonyPatcher.PatchAll();
            HarmonyPatcher.Instance.Patch(typeof(GameContext).GetMethod("Start"),
                new HarmonyMethod(typeof(Main).GetMethod("PostLoad")));
        }
        
        private static bool isPostInitialized;

        static void PostLoad()
        {
            if (isPostInitialized) return;
            isPostInitialized = true;
            HarmonyPatcher.Instance.Patch(typeof(AutoSaveDirector).GetMethod("PushSavedGame",BindingFlags.NonPublic|BindingFlags.Instance), null,
                new HarmonyMethod(typeof(Main).GetMethod("OnGameLoad"))); 

        }

    }
}
