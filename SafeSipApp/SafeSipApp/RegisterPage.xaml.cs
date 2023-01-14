namespace SafeSipApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage()
		{
			InitializeComponent();
			NextButton.IsEnabled = false;

		}

		private async void ImageButton_Clicked(object sender, EventArgs e)
		{
			if (!CanNavigate())
			{
				await DisplayAlert("Error", "You must fill out all the fields before continuing.", "Ok");
				return;
			}

			AppInstance.Instance.SetUserInfo(FullNameEntry.Text, PersonalPhoneEntry.Text, EmergnecyContactEntry.Text);
			await Shell.Current.GoToAsync(nameof(ScanCoasterPage));
		}

		private void FullNameEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			NextButton.IsEnabled = CanNavigate();
		}

		private void PersonalPhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			NextButton.IsEnabled = CanNavigate();
		}

		private void EmergnecyContactEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			NextButton.IsEnabled = CanNavigate();
		}

		private bool CanNavigate()
		{
			return !(string.IsNullOrEmpty(FullNameEntry.Text) 
				|| string.IsNullOrEmpty(PersonalPhoneEntry.Text)
				|| string.IsNullOrEmpty(PersonalPhoneEntry.Text));

		}
	}
}