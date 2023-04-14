using System.Windows.Forms;
using GTA;

namespace GTAV_Ragdoll_Mod.Config
{
    public class RagdollConfig
    {
        public RagdollConfig(ScriptSettings settings)
        {
            RagdollKeybind = settings.GetValue("RagdollConfig", "RagdollKeybind", Keys.Add);
            RagdollStartDelay = settings.GetValue("RagdollConfig", "RagdollStartDelay", 0f);
            RagdollEndDelay = settings.GetValue("RagdollConfig", "RagdollEndDelay", 1f);
            CancelSkydive = settings.GetValue("RagdollConfig", "CancelSkydive", false);
        }

        public readonly Keys RagdollKeybind;
        public readonly float RagdollStartDelay;
        public readonly float RagdollEndDelay;
        public readonly bool CancelSkydive;
    }
}