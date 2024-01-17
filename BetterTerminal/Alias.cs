using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GameNetcodeStuff;
using UnityEngine.Events;
using System.Runtime.CompilerServices;
using LethalAPI.LibTerminal.Models;
using LethalAPI.LibTerminal;
using LethalAPI.LibTerminal.Attributes;
using LethalAPI.LibTerminal.Models.Enums;
using static BetterTerminal.MainBetterTerminal;

namespace BetterTerminal
{
    internal class Alias
    {
        MainBetterTerminal instance = new MainBetterTerminal();

        [TerminalCommand("scan", true)]
        public string ScanAlias(string scanspot)
        {

            if (scanspot == "inside")
            {
                return instance.ScanInsideCommand();
            }else if (scanspot == "ship")
            {
                return instance.ItemsCommand();
            }
            else
            {
                return "[There was no scan type supplied with that word.]";
            }
            }

        [TerminalCommand("light", true)]
        public string LightAlias()
        {
            return instance.LightsCommand();
        }

        [TerminalCommand("teleport", true)]
        public string TpAlias()
        {
            return instance.TpCommand();
        }

        [TerminalCommand("doors", true)]
        public string DoorAlias()
        {
            return instance.ToggleDoorCommand();
        }

        [TerminalCommand("Land", true)]
        public string LaunchAlias()
        {
            return instance.LaunchCommand();
        }

        [TerminalCommand("Go", true)]
        public string LaunchAlias2()
        {
            return instance.LaunchCommand();
        }

        [TerminalCommand("InverseTp", true)]
        public string InverseAlias()
        {
            return instance.InverseCommand();
        }


    }
}
