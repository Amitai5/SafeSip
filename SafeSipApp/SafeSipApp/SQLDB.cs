using Microsoft.Data.SqlClient;
using System.Data;

namespace SafeSipApp
{
    public class SQLDB
    {
        private readonly SqlConnection connection;

        public SQLDB()
        {
            connection = new SqlConnection("Server=tcp:safesip.database.windows.net,1433;Database=safesip;User ID=safesip-admin@safesip;Password=Cumstain45*;Trusted_Connection=False;Encrypt=True;");
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

        public string LogIn(int coasterID)
        {
            SqlCommand cmd = new SqlCommand("usp_UserLogIn", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = AppInstance.Instance.FullName;
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = AppInstance.Instance.PersonalPhone;
            cmd.Parameters.Add("@EmergencyPhoneNumber", SqlDbType.VarChar).Value = (object)AppInstance.Instance.EmergnecyContact ?? DBNull.Value;
            cmd.Parameters.Add("@CoasterID", SqlDbType.Int).Value = coasterID;
            AppInstance.Instance.CoasterID = coasterID;

            connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            dataReader.Read();
            string firstColumnName = dataReader.GetName(0);
            if (firstColumnName != "ErrorCode")
            {
                int userID = dataReader.GetInt32(0);
                AppInstance.Instance.UserID = userID;
                _ = dataReader.DisposeAsync();
                connection.Close();
                return null;
            }

            string message = dataReader.GetString(1);
            _ = dataReader.DisposeAsync();
            connection.Close();
            return message;
        }

        public string SetActive()
        {
            SqlCommand cmd = new SqlCommand("usp_SetActive", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = AppInstance.Instance.UserID;

            connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (!dataReader.Read())
            {
                _ = dataReader.DisposeAsync();
                connection.Close();
                return null;
            }

            string message = dataReader.GetString(1);
            _ = dataReader.DisposeAsync();
            connection.Close();
            return message;
        }

        public string SetInctive()
        {
            SqlCommand cmd = new SqlCommand("usp_SetInactive", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = AppInstance.Instance.UserID;

            connection.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();

            if (!dataReader.Read())
            {
                _ = dataReader.DisposeAsync();
                connection.Close();
                return null;
            }

            string message = dataReader.GetString(1);
            _ = dataReader.DisposeAsync();
            connection.Close();
            return message;
        }
    }
}
