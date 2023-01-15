using Microsoft.Data.SqlClient;
using SafeSipAPI.Model;
using System.Data;
using Twilio;

namespace SafeSipAPI
{
    public class SQLDB
    {
        private readonly SqlConnection connection;

        public SQLDB()
        {
            connection = new SqlConnection("Server=tcp:safesip.database.windows.net,1433;Database=safesip;User ID=safesip-admin@safesip;Password=Cumstain45*;Trusted_Connection=False;Encrypt=True;");
            TwilioClient.Init("AC645861ecdc6b721779c8e1f1a2998ff3", "ec8e0991020c329b52a37994b743560b");
        }

        public static SQLDB Instance
        {
            get
            {
                instance ??= new SQLDB();
                return instance;
            }
        }
        private static SQLDB instance = null;

        public SMSMessage? GetTamperedMessage()
        {
            SMSMessage? messagesToSend = null;
            if (connection.State != ConnectionState.Closed)
            {
                return messagesToSend;
            }

            SqlCommand cmd = new SqlCommand("usp_GetTampered", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                string fullName = dataReader.GetString(0);
                string personalNumber = dataReader.GetString(1).Trim();

                List<string> phoneNumbers = new List<string>();
                for (int i = 2; i < dataReader.FieldCount; i++)
                {
                    string phoneNumber = dataReader.GetString(i).Trim();
                    phoneNumbers.Add(phoneNumber);
                }

                messagesToSend = new SMSMessage(fullName, personalNumber, phoneNumbers);
            }

            dataReader.Close();
            connection.Close();
            return messagesToSend;
        }
    }
}
