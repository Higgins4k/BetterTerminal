using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using BepInEx;

namespace BetterTerminal
{
    [Serializable]
    public class BetterTerminalConfig : SyncedInstance<BetterTerminalConfig>
    {
        public ConfigEntry<bool> restartCommandDisable;

        public ConfigEntry<bool> itpDisable, itpHost;
        public ConfigEntry<bool> launchDisable, launchHost;
        public ConfigEntry<bool> doorDisable, doorHost;
        public ConfigEntry<bool> teleportDisable, teleportHost;
        public ConfigEntry<bool> lightsDisable, lightsHost;
        public ConfigEntry<bool> cancelDeliveryDisable, cancelDeliveryHost;
        public ConfigEntry<bool> itemsDisable, itemsHost;
        public ConfigEntry<bool> scanInsideDisable, scanInsideHost;

        public BetterTerminalConfig(ConfigFile cfg)
        {
            InitInstance(this);

            restartCommandDisable = cfg.Bind(
              "Commands.Restart",
              "Disable",
              false,
              "Disables the restart command"
            );
            itpDisable = cfg.Bind(
              "Commands.itp",
              "Disable",
              false,
              "Disables the ITP (Inverse Teleporter) command"
            );
            itpHost = cfg.Bind(
              "Commands.itp",
              "Host Only",
              false,
              "Makes the ITP (Inverse Teleporter) command for hosts only"
            );
            launchDisable = cfg.Bind(
              "Commands.Launch",
              "Disable",
              false,
              "Disables the Launch command"
            );
            launchHost = cfg.Bind(
              "Commands.Launch",
              "Host Only",
              false,
              "Makes the Launch command for hosts only"
            );
            doorDisable = cfg.Bind(
              "Commands.Door",
              "Disable",
              false,
              "Disables the Door command"
            );
            doorHost = cfg.Bind(
              "Commands.Door",
              "Host Only",
              false,
              "Makes the Door command for hosts only"
            );
            teleportDisable = cfg.Bind(
              "Commands.Teleport",
              "Disable",
              false,
              "Disables the Door command"
            );
            teleportHost = cfg.Bind(
              "Commands.Teleport",
              "Host Only",
              false,
              "Makes the Door command for hosts only"
            );
            lightsDisable = cfg.Bind(
              "Commands.Lights",
              "Disable",
              false,
              "Disables the Lights command"
            );
            lightsHost = cfg.Bind(
              "Commands.Lights",
              "Host Only",
              false,
              "Makes the Lights command for hosts only"
            );
            cancelDeliveryDisable = cfg.Bind(
              "Commands.CancelDelivery",
              "Disable",
              false,
              "Disables the Cancel Delivery command"
            );
            cancelDeliveryHost = cfg.Bind(
              "Commands.Lights",
              "Host Only",
              false,
              "Makes the Cancel Delivery command for hosts only"
            );
            itemsDisable = cfg.Bind(
              "Commands.Items",
              "Disable",
              false,
              "Disables the Items command"
            );
            itemsHost = cfg.Bind(
              "Commands.Items",
              "Host Only",
              false,
              "Makes the Items command for hosts only"
            );
            scanInsideDisable = cfg.Bind(
              "Commands.ScanInside",
              "Disable",
              false,
              "Disables the Scan Inside command"
            );
            scanInsideHost = cfg.Bind(
              "Commands.ScanInside",
              "Host Only",
              false,
              "Makes the Scan Inside command for hosts only"
            );
        }

    }
}