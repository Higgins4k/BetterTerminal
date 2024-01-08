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
using LethalAPI.TerminalCommands;
using LethalAPI.TerminalCommands.Models;
using LethalAPI.TerminalCommands.Attributes;
using UnityEngine;
using GameNetcodeStuff;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

namespace BetterTerminal
{

    [BepInPlugin(modGUID, modName, modVersion)]
    [HarmonyPatch(typeof(Terminal))]

    public class MainBetterTerminal : BaseUnityPlugin
    {

        private const string modVersion = "1.0.1";

        private const string modGUID = "zg.BetterTerminal";
        private const string modName = "BetterTerminal";
        private readonly Harmony harmony = new Harmony(modGUID);
        private static MainBetterTerminal instance;
        private TerminalModRegistry Registry;

        internal ManualLogSource pnt;

        void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
            Registry = TerminalRegistry.CreateTerminalRegistry();
            Registry.RegisterFrom(this);


            pnt = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            pnt.LogInfo("BetterTerminal Enabled");
            pnt.LogInfo("Make sure to review BetterTerminal");

            harmony.PatchAll(typeof(MainBetterTerminal));


        }
        [TerminalCommand("ScanInside"), CommandInfo("See how many items are INSIDE the facility")]
        public string ScanInsideCommand()
        {
            Debug.Log("Got to debug 1");
            System.Random random = new System.Random(StartOfRound.Instance.randomMapSeed + 91);
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            GrabbableObject[] objectsOfType = UnityEngine.Object.FindObjectsOfType<GrabbableObject>();
            for (int index = 0; index < objectsOfType.Length; ++index)
            {
                Debug.Log("Got to debug 2");
                if (objectsOfType[index].itemProperties.isScrap && objectsOfType[index].isInFactory)
                {
                    Debug.Log("Got to debug 3");
                    num4 += objectsOfType[index].itemProperties.maxValue - objectsOfType[index].itemProperties.minValue;
                    num3 += Mathf.Clamp(random.Next(objectsOfType[index].itemProperties.minValue, objectsOfType[index].itemProperties.maxValue), objectsOfType[index].scrapValue - 6 * index, objectsOfType[index].scrapValue + 9 * index);
                    ++num2;
                }
                Debug.Log("Got to debug 4");
            }
            Debug.Log("Got to debug 5");
            string displaying = "There are currently " + num2.ToString() + " items inside the facility";
            return displaying;
        }

        [TerminalCommand("Items", true), CommandInfo("See the value of items inside of the ship")]
        public string ItemsCommand()
        {
            GameObject ship = GameObject.Find("/Environment/HangarShip");
            if (ship == null)
            {
                return "0";
            }
            var loot = ship.GetComponentsInChildren<GrabbableObject>()
                .Where(obj => obj.name != "ClipboardManual" && obj.name != "StickyNoteItem" && obj.name != "KeyItem")
                .ToList();

            float totalScrapValue = loot.Sum(scrap => scrap.scrapValue);

            Debug.Log($"There is currently ${totalScrapValue} value of scrap inside the ship");
            //Credit to ShipLoot on grabbing the item values
            return $"There is currently ${totalScrapValue} value of scrap inside of the ship";
        }

        [TerminalCommand("CancelDelivery", true), CommandInfo("Cancel purchased items for a refund before delivery")]
        public string CancelDeliveryCommand()
        {
            Terminal terminalInstance = UnityEngine.GameObject.FindObjectOfType<Terminal>();
            float walkie = terminalInstance.buyableItemsList[0].creditsWorth;
            float flashlight = terminalInstance.buyableItemsList[1].creditsWorth;
            float shovel = terminalInstance.buyableItemsList[2].creditsWorth;
            float lockpick = terminalInstance.buyableItemsList[3].creditsWorth;
            float proflash = terminalInstance.buyableItemsList[4].creditsWorth;
            float stun = terminalInstance.buyableItemsList[5].creditsWorth;
            float boombox = terminalInstance.buyableItemsList[6].creditsWorth;
            float tzpin = terminalInstance.buyableItemsList[7].creditsWorth;
            float zapgun = terminalInstance.buyableItemsList[8].creditsWorth;
            float jetpack = terminalInstance.buyableItemsList[9].creditsWorth;
            float extladder = terminalInstance.buyableItemsList[10].creditsWorth;
            float radar = terminalInstance.buyableItemsList[11].creditsWorth;
            float spray = terminalInstance.buyableItemsList[12].creditsWorth;


            if (terminalInstance.numberOfItemsInDropship > 0)
            {
                Debug.LogError(terminalInstance.numberOfItemsInDropship);
                Debug.LogError(terminalInstance.numberOfItemsInDropship);
                int credtoadd = 0;
                foreach (var itemIndex in terminalInstance.orderedItemsFromTerminal)
                {
                    if (itemIndex >= 0 && itemIndex < terminalInstance.buyableItemsList.Count())
                    {
                        credtoadd += terminalInstance.buyableItemsList[itemIndex].creditsWorth;
                        Debug.LogError(credtoadd);
                    }
                    else
                    {
                        Debug.LogError("Why is this happening?!?");
                    }
                }
                int newcreds = terminalInstance.groupCredits += credtoadd;
                Debug.Log(credtoadd);
                credtoadd = 0;
                terminalInstance.orderedItemsFromTerminal.Clear();
                terminalInstance.numberOfItemsInDropship = 0;

                terminalInstance.SyncGroupCreditsClientRpc(newcreds, terminalInstance.numberOfItemsInDropship);
                terminalInstance.SyncGroupCreditsServerRpc(newcreds, terminalInstance.numberOfItemsInDropship);


                return "Purchase cancelled and items refunded";
            }
            else
            {
                return "No items in delivery to cancel.";
            }
        }

        [TerminalCommand("bettermods"), CommandInfo("Info about BetterMods")]
        public string BetterModsCommand()
        {
            return "BetterSpec and BetterTerminal were made by Higgins, You can get them both on Thunderstore";
        }

        [TerminalCommand("lights", true), CommandInfo("Use this to toggle ship lights remotely")]
        public string LightsCommand()
        {
            StartOfRound.Instance.shipRoomLights.ToggleShipLights();
            return "You've toggled the lights inside of the ship";
        }

        [TerminalCommand("tp", true), CommandInfo("Teleport the player currently on the monitor")]
        public string TpCommand()
        {
            ShipTeleporter[] array = UnityEngine.Object.FindObjectsOfType<ShipTeleporter>();

            if (array != null && array.Length > 0)
            {
                ShipTeleporter val = array[0];
                val.PressTeleportButtonOnLocalClient();
                return "Teleporting";
            }
            else
            {
                return "You must purchase a Teleporter to teleport";
            }
        }

        [TerminalCommand("door", true), CommandInfo("Safely toggle the Ship Door")]
        public string ToggleDoorCommand()
        {
            InteractTrigger doorButton = GameObject.Find(StartOfRound.Instance.hangarDoorsClosed ? "StartButton" : "StopButton").GetComponentInChildren<InteractTrigger>();
            doorButton.onInteract.Invoke(GameNetworkManager.Instance.localPlayerController);
            
            if (StartOfRound.Instance.hangarDoorsClosed)
            {
                return "Opened the doors";
            }
            else
            {
                return "Closed the doors, stay safe";
            }

        }

        [TerminalCommand("Launch", true), CommandInfo("Use this to land or launch the ship")]
        public string LaunchCommand()
        {
            if (GameObject.Find("StartGameLever") == null)
            {
                return "Error finding the Lever";
            }
            StartMatchLever theswitch = GameObject.Find("StartGameLever").GetComponent<StartMatchLever>();
            if ((StartOfRound.Instance.shipDoorsEnabled && !StartOfRound.Instance.shipHasLanded && !StartOfRound.Instance.shipIsLeaving))
            {
                return "Contact the Pilot someone's already done this!";
            }
            if ((!StartOfRound.Instance.shipDoorsEnabled && StartOfRound.Instance.travellingToNewLevel))
            {
                return "Contact the Pilot someone's already done this!";
            }
            theswitch.PullLever();
            theswitch.LeverAnimation();

            if(theswitch.leverHasBeenPulled)
            {
                theswitch.StartGame();
                return "Copy that Pilot ship is heading there now";
            }
            if (!theswitch.leverHasBeenPulled)
            {
                theswitch.EndGame();
                return "Copy that Pilot, Leaving the moon now";
            }

            return "If you see this run the command again";

        }



    }





    }


