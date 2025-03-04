﻿using Microsoft.Data.SqlClient;
using SafeSipAPI.Model;
using System.Data;
using Twilio.Types;
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
                    object? phoneNumber = dataReader.GetValue(i);

                    if (phoneNumber != DBNull.Value)
                    {
                        phoneNumbers.Add(Convert.ToString(phoneNumber)!.Trim());
                    }
                }

                messagesToSend = new SMSMessage(fullName, personalNumber, phoneNumbers);
            }

            dataReader.Close();
            connection.Close();
            return messagesToSend;
        }

        public void ResetCoastersTampered()
        {
            SqlCommand cmd = new SqlCommand("usp_resetCoastersTamper", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void SetCoasterTampered(int coasterID)
        {
            SqlCommand cmd = new SqlCommand("usp_CoasterTrigger", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@CoasterID", SqlDbType.Int).Value = coasterID;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public bool GetIsActive(int coasterID)
        {
            SqlCommand cmd = new SqlCommand("usp_getActive", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@CoasterID", SqlDbType.Int).Value = coasterID;

            connection.Open();
            bool isActive = Convert.ToBoolean(cmd.ExecuteScalar());
            connection.Close();
            return isActive;
        }
    }
}
