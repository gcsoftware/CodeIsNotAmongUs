using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;

namespace CodeIsNotAmongUs
{
    [BepInPlugin("pl.js6pak.CodeIsNotAmongUs", "Code is not among us", "0.1.0")]
    [BepInProcess("Among Us.exe")]
    public class CodeIsNotAmongUsPlugin : BasePlugin
    {
        public ConfigEntry<string> Name { get; set; }
        public ConfigEntry<string> Ip { get; set; }
        public ConfigEntry<ushort> Port { get; set; }

        public override void Load()
        {
            Name = Config.Bind("Custom region", "Name", "localhost");
            Ip = Config.Bind("Custom region", "Ip", "127.0.0.1");
            Port = Config.Bind("Custom region", "Port", (ushort) 22023);

            var defaultRegions = ServerManager.DefaultRegions.ToList();

            var split = Ip.Value.Split(':');
            var ip = split[0];
            var port = ushort.TryParse(split.ElementAtOrDefault(1), out var p) ? p : (ushort) 22023;

            defaultRegions.Add(new KMDGIDEDGGM(
                Name.Value, ip, new[]
                {
                    new EEKGADNPDBH("Master-1", ip, port)
                })
            );

            ServerManager.DefaultRegions = defaultRegions.ToArray();
        }
    }
}