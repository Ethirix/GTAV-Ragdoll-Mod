using GTA;

namespace Yet_Another_Ragdoll_Mod.Config
{
    public class DebugConfig
    {
        public DebugConfig(ScriptSettings settings)
        {
            ShowDebugNotifications = settings.GetValue("DebugConfig", "ShowDebugNotifications", false);
        }

        public readonly bool ShowDebugNotifications;
    }
}