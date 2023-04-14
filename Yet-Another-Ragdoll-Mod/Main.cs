using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.UI;
using Yet_Another_Ragdoll_Mod.Config;

namespace Yet_Another_Ragdoll_Mod
{
    public class Main : Script
    {
        private bool _runRagdoll;
        private bool _isSupposedToBeRagdolled;

        private readonly RagdollConfig _ragdollConfig;
        private readonly DebugConfig _debugConfig;

        public Main()
        {
            _ragdollConfig = new RagdollConfig(Settings);
            _debugConfig = new DebugConfig(Settings);

            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (_runRagdoll || (_isSupposedToBeRagdolled && !Game.Player.Character.IsRagdoll))
            {
                RunRagdoll();
            }

            if ((_isSupposedToBeRagdolled && _ragdollConfig.CancelSkydive) 
                || (Game.Player.Character.IsRagdoll && _ragdollConfig.CancelSkydive))
            {
                Game.Player.Character.Weapons.Remove(WeaponHash.Parachute);
            }

            if ((Game.Player.Character.IsDead 
                || Function.Call<bool>(Hash.IS_CUTSCENE_ACTIVE) 
                || Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Game.Player.Character, true))
                && _isSupposedToBeRagdolled)
            {
                ThrowNotification("Caught ragdoll attempt?");
                _isSupposedToBeRagdolled = false;
            }
        }

        private void RunRagdoll()
        {
            _runRagdoll = false;

            if (!Game.Player.Character.IsRagdoll)
            {
                ThrowNotification("Ragdolling...");
                Ragdoll();
            }
            else
            {
                ThrowNotification("Cancelling Ragdoll...");
                CancelRagdoll();
            }
        }

        private void Ragdoll()
        {
            Wait((int)(_ragdollConfig.RagdollStartDelay * 1000));
            Game.Player.Character.Ragdoll(-1, RagdollType.ScriptControl);
            _isSupposedToBeRagdolled = true;
        }

        private void CancelRagdoll()
        {
            _isSupposedToBeRagdolled = false;
            Wait((int)(_ragdollConfig.RagdollEndDelay * 1000));
            Game.Player.Character.CancelRagdoll();
        }

        private void OnKeyDown(object sender, KeyEventArgs key)
        {
            if (key.KeyCode == _ragdollConfig.RagdollKeybind 
                && !Function.Call<bool>(Hash.IS_CUTSCENE_ACTIVE) 
                && !Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Game.Player.Character, true)
                && !Game.Player.Character.IsDead)
            {
                _runRagdoll = true;
                ThrowNotification("Keybind passed");
            }
            else if (Function.Call<bool>(Hash.IS_CUTSCENE_ACTIVE) 
                     || Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Game.Player.Character, true) 
                     || Game.Player.Character.IsDead)
            {
                ThrowNotification("Keybind failed");
            }
        }

        private void ThrowNotification(string message)
        {
            if (_debugConfig.ShowDebugNotifications)
            {
                Notification.Show(message);
            }
        }
    }
}
