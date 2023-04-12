using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace GTAV_Ragdoll_Mod
{
    public struct DebugConfig
    {
        public bool ShowDebugNotifications;
        public int ConstantRagdollRefreshTimer;

        public static DebugConfig GetConfig()
        {
            return new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build()
                .Deserialize<DebugConfig>(File.ReadAllText("GTAV-Ragdoll-Mod.yaml"));
        }
    }
}