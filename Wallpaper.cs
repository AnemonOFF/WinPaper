using System.Runtime.InteropServices;

namespace WinPaper;

public class Wallpaper
{
    private string _currentWallpaper = string.Empty;

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

    private const uint SPI_SETDESKWALLPAPER = 0x14;
    private const uint SPIF_UPDATEINIFILE = 0x1;
    private const uint SPIF_SENDWININICHANGE = 0x2;

    public void SetNextWallpaper()
    {
        var dir = Directory.GetCurrentDirectory();
        if (!Directory.Exists("Images")) Directory.CreateDirectory("Images");

        var files = Directory.GetFiles("Images");
        if (files is null || files.Length == 0) return;

        if (files.Last() == _currentWallpaper) _currentWallpaper = files.First();
        else _currentWallpaper = files[Array.IndexOf(files, _currentWallpaper) + 1];

        if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Path.Combine(dir, _currentWallpaper), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE))
            MessageBox.Show("Error on wallpaper setting", ":)", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
