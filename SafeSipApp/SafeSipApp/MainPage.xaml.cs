using Plugin.NFC;

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
		active = !active;
		if (active)
		{
			StatusToggle.BackgroundColor = new Color(255, 0, 0);
			StatusLabel.BackgroundColor = new Color(255, 0, 0);
			StatusIcon.Source = "active_icon.png";
			StatusToggle.Text = "Deactivate";
			StatusLabel.Text = "(Active)";
			SQLDB.Instance.SetActive();
		}
		else
		{
			StatusLabel.BackgroundColor = Color.FromHex("#ff00000");
			StatusToggle.BackgroundColor = new Color(0, 255, 0);
			StatusIcon.Source = "inactive_icon.png";
			StatusLabel.Text = "(Inactive)";
			StatusToggle.Text = "Activate";
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