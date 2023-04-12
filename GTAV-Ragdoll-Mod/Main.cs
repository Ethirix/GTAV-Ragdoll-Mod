using System;
using System.Windows.Forms;
using GTA;
using GTA.UI;

namespace GTAV_Ragdoll_Mod
{
    public class Main : Script
    {
        private bool _ragdollRunning;
        private bool _runRagdoll;

        public Main()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (_runRagdoll)
            {
                _ragdollRunning = true;
                RunRagdoll();
                _ragdollRunning = false;
            }
        }

        private void RunRagdoll()
        {
            RagdollConfig config = RagdollConfig.GetConfig();
            _runRagdoll = false;

            if (Game.Player.Character.CanRagdoll && !Game.Player.Character.IsRagdoll)
            {
                Wait((int)(config.RagdollStartDelay * 1000));
                Game.Player.Character.Ragdoll(-1, RagdollType.ScriptControl);
            }
            else if (Game.Player.Character.IsRagdoll)
            {
                Wait((int)(config.RagdollEndDelay * 1000));
                Game.Player.Character.CancelRagdoll();
            }

            ThrowNotification("Is Ragdoll: " + Game.Player.Character.IsRagdoll);
        }

        private void OnKeyDown(object sender, KeyEventArgs key)
        {
            RagdollConfig config = RagdollConfig.GetConfig();
            Enum.TryParse(config.RagdollKeybind, out Keys ragdollKeybind);

            if (key.KeyCode == ragdollKeybind)
            {
                if (!_ragdollRunning)
                {
                    _runRagdoll = true;
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs key)
        {
        }

        private void ThrowNotification(string message)
        {
            if (DebugConfig.GetConfig().ShowDebugNotifications)
            {
                Notification.Show(message);
            }
        }
    }
}
