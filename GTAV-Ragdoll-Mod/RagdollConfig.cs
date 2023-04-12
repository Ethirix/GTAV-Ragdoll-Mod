using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace GTAV_Ragdoll_Mod
{
    public struct RagdollConfig
    {
        public string RagdollKeybind;
        public float RagdollStartDelay;
        public float RagdollEndDelay;

        public static RagdollConfig GetConfig()
        {
            return new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build()
                .Deserialize<RagdollConfig>(File.ReadAllText("GTAV-Ragdoll-Mod.yaml"));
        }
    }
}