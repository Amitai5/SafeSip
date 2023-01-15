using SafeSipApp.Pages;

namespace SafeSipApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CoasterTamperedPage), typeof(CoasterTamperedPage));
        Routing.RegisterRoute(nameof(ScanCoasterPage), typeof(ScanCoasterPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
