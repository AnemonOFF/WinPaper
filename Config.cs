using GlobalHotKeys.Native.Types;
using System.Text.Json;

namespace WinPaper;

public class Config
{
    public VirtualKeyCode Key { get; private set; } = VirtualKeyCode.KEY_J;
    public Modifiers Mods { get; private set; } = Modifiers.Control | Modifiers.Shift;

    public Config()
    {
        UpdateConfig();
    }

    public void UpdateConfig()
    {
        var dir = Directory.GetCurrentDirectory();
        if (!File.Exists("config.json")) return;

        var file = File.ReadAllText("config.json");
        if (file is null) return;
        try
        {
            var json = JsonSerializer.Deserialize<ConfigFile>(file);
            if (json is null || json.Key is null || json.Mods is null) return;
            var key = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), json.Key);
            var mods = json.Mods.Split(",").Select(m => (Modifiers)Enum.Parse(typeof(Modifiers), m));
            Key = key;
            Mods = (Modifiers)mods.Cast<int>().Aggregate(0, (c, n) => c |= n);
        }
        catch { }
    }

    private record ConfigFile(string Key, string Mods);
}
