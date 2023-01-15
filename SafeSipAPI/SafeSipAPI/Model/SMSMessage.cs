using System.Net;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SafeSipAPI.Model
{
    public class SMSMessage
    {
        public string FullName { get; init; }
        public List<string> PhoneNumbers { get; init; }
        public string PersonalPhoneNumber { get; init; }

        public SMSMessage(string fullname, string personal, List<string> phoneNumbers) 
        {
            FullName = fullname;
            PhoneNumbers = phoneNumbers;
            PersonalPhoneNumber= personal;
        }

        public bool SendNow()
        {
            //Send Personal Text
            sendMessage(PersonalPhoneNumber, getPersonalString());

            //Send Emergency Texts
            foreach (string phoneNumber in PhoneNumbers)
            {
                sendMessage(phoneNumber, GetStandardBody());
            }

            return true;
        }

        private void sendMessage(string phoneNumber, string bodyText)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber));
            messageOptions.MessagingServiceSid = "MG2a8018b7ee58011327b0c5d8898e2780";
            messageOptions.Body = bodyText;

            var message = MessageResource.Create(messageOptions);
        }

        public string GetStandardBody()
        {
            return $"SAFESIP: {FullName}'s coaster has been moved. Their drink has likely been tampered with.";
        }

        private string getPersonalString()
        {
            return "SAFESIP: Your coaster has been moved. Your drink has likely been tampered with.";
        }
    }
}
