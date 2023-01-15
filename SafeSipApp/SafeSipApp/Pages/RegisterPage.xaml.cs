using System.Text.RegularExpressions;

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

			string personalNumber = PersonalPhoneEntry.Text;
			if (!validPhoneNumber(ref personalNumber))
			{
				await DisplayAlert("Error", "You must enter a valid personal phone number before continuing.", "Ok");
				return;
			}

			string emergencyContactNumber = EmergnecyContactEntry.Text;
			if (!string.IsNullOrEmpty(emergencyContactNumber) && !validPhoneNumber(ref emergencyContactNumber))
			{
				await DisplayAlert("Error", "You must enter a valid personal emergency contact number before continuing.", "Ok");
				return;
			}

			AppInstance.Instance.SetUserInfo(FullNameEntry.Text, personalNumber, emergencyContactNumber);
			await Shell.Current.GoToAsync(nameof(ScanCoasterPage));
		}

		private bool validPhoneNumber(ref string phoneNumber)
		{
			bool valid = Regex.IsMatch(phoneNumber, @"\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})");
			string justDigits = phoneNumber.Replace(" ", "").Replace("+", "").Replace("-", "").Replace("(", "").Replace(")", "");

			if (phoneNumber.Length == 10)
			{
				phoneNumber = $"+1{justDigits}";
			}
			Console.WriteLine(phoneNumber);
			return valid;
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
			return !(string.IsNullOrEmpty(FullNameEntry.Text) || string.IsNullOrEmpty(PersonalPhoneEntry.Text));

		}
	}
}