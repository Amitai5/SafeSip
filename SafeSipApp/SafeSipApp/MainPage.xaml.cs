using Android.OS;
using System.Diagnostics;

namespace SafeSipApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		string FullName = FullNameEntry.Text;
		string PersonalPhone = PersonalPhoneEntry.Text;
		string EmeregencyContact = EmergnecyContactEntry.Text;
        System.Diagnostics.Debug.WriteLine(FullName);
    }
}

