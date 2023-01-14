using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSipApp
{
    public sealed class AppInstance
    {
        public static AppInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppInstance();
                }
                return instance;
            }
        }
        private static AppInstance instance = null;

        public int UserID { get; set; }
        public int CoasterID { get; set; }
        public string FullName { get; private set; }
        public string PersonalPhone { get; private set; }
        public string EmergnecyContact { get; private set; }

        public void SetUserInfo(string fullName, string personalPhone, string emergencyContact)
        {
            FullName = fullName;
            PersonalPhone = personalPhone;
            EmergnecyContact = emergencyContact;
        }
    }
}
