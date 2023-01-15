using Microsoft.Maui.ApplicationModel.Communication;
using Plugin.NFC;
using SafeSipApp.Pages;

namespace SafeSipApp;

public partial class MainPage : ContentPage
{
    private bool active = false;

    public MainPage()
    {
        InitializeComponent();
    }

    private void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        CrossNFC.Current.StartListening();
        CrossNFC.Current.OnMessageReceived += Current_OnMessageReceived;
    }

    private void ToggleStatus()
    {
        string active_color = "#ed2024";
        string inactive_color = "#1575c5";

        active = !active;
        if (active)
        {
            StatusToggle.BackgroundColor = Color.FromArgb(active_color);
            StatusToggle.Text = "Deactivate";
            StatusText.Text = "Active";
            StatusText.TextColor = Color.FromArgb(active_color);
            StatusIcon.Source = "active_icon.png";
            SQLDB.Instance.SetActive();
        }
        else
        {
            StatusToggle.BackgroundColor = Color.FromArgb(inactive_color);
            StatusToggle.Text = "Activate";
            StatusText.TextColor = Color.FromArgb(inactive_color);
            StatusText.Text = "Inactive";
            StatusIcon.Source = "inactive_icon.png";
            SQLDB.Instance.SetInctive();
        }
    }

    private void StatusToggle_Clicked(object sender, EventArgs e)
    {
        ToggleStatus();
    }

    private void Current_OnMessageReceived(ITagInfo tagInfo)
    {
        int coasterID = Convert.ToInt32(tagInfo.Records[0].Message);
        if (AppInstance.Instance.CoasterID == coasterID)
        {
            ToggleStatus();
        }
    }
}