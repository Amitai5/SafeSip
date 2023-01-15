using Plugin.NFC;

namespace SafeSipApp;

public partial class ScanCoasterPage : ContentPage
{
    private bool scannedSuccessfully = false;

    public ScanCoasterPage()
    {
        InitializeComponent();
    }

    private void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        CrossNFC.Current.StartListening();
        CrossNFC.Current.OnMessageReceived += Current_OnMessageReceived;
    }

    private async void Current_OnMessageReceived(ITagInfo tagInfo)
    {
        if (scannedSuccessfully)
        {
            return;
        }

        int coasterID = Convert.ToInt32(tagInfo.Records[0].Message);
        string errorMessage = SQLDB.Instance.LogIn(coasterID);

        if (errorMessage != null)
        {
            await DisplayAlert("Log In Failed", errorMessage, "Ok");
            return;
        }

        scannedSuccessfully = true;
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

    }
}