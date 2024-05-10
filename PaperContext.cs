using WinPaper.Properties;

namespace WinPaper;

public class PaperContext : ApplicationContext
{
    private NotifyIcon trayIcon;

    public PaperContext()
    {
        // Initialize Tray Icon
        var menu = new ContextMenuStrip();
        menu.Items.Add(text: "Exit", image: null, onClick: Exit);
        trayIcon = new NotifyIcon()
        {
            Icon = Resources.ico,
            Visible = true,
            ContextMenuStrip = menu,
        };
    }

    void Exit(object? sender, EventArgs e)
    {
        // Hide tray icon, otherwise it will remain shown until user mouses over it
        trayIcon.Visible = false;

        Application.Exit();
    }
}
