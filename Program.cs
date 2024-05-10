using GlobalHotKeys;
using System.Reactive.Linq;

namespace WinPaper
{
    internal static class Program
    {
        private static Wallpaper Wallpaper = new();
        private static Config Config = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(false);

            using var hotKeyManager = new HotKeyManager();
            using var hotKey = hotKeyManager.Register(Config.Key, Config.Mods);
            using var subscription = hotKeyManager.HotKeyPressed.Subscribe(OnHotKeyPress);

            Application.Run(new PaperContext());
        }

        private static void OnHotKeyPress(HotKey hotKey)
        {
            Wallpaper.SetNextWallpaper();
        }
    }
}