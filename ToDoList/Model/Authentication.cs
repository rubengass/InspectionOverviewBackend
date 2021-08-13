using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ToDoList.Model
{
    public class Authentication
    {
        private string _Username;
        private string _Password;
        private string AuthenticationKey;
        private int _UserId;

        public string Username
        {
            get => _Username;
            set { _Username = value; }
        }
        public string Password
        {
            get => _Password;
            set { _Password = value; }
        }

        private Boolean PerformLogin()
        {
            string Query = "SELECT * FROM login WHERE Username = '" + _Username + "' AND Password = '"+_Password+"';";
            SelectReference reference = new SelectReference();
            reference.LoginReference();
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = DBM.fetch(new DatabaseCommandSelect(), Query, reference);
            _UserId = Convert.ToInt32(response.Data.Value[0]);
            _Username = response.Data.Value[1];
            _Password = response.Data.Value[2];
            return response.Success;
        }

        public string Login()
        {
            if (PerformLogin())
            {
                if (AuthenticationKeyExistsForUser())
                {
                    RemoveDuplicateKeys();
                }
                GenerateAuthenticationKey();
                SaveAuthenticationKey();
            }
            return AuthenticationKey;
        }

        private Boolean AuthenticationKeyExistsForUser()
        {
            string Query = "SELECT COUNT(*) FROM tempkeys WHERE UserId = '" + _UserId + "';";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), Query);
            if (response.Data == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateAuthenticationKey()
        {
            int size = 10;
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }
            AuthenticationKey = result.ToString();
        }

        private Boolean SaveAuthenticationKey()
        {
            string Query = "INSERT INTO tempkeys(AuthenticationKey, Expiry, UserId) values('" + AuthenticationKey + "','" + DateTime.UtcNow.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ssZ") + "','" + _UserId+"');";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandInsert(), Query);
            if (response.Data == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean RemoveExpiredKeys()
        {
            string Query = "DELETE FROM tempkeys WHERE Expiry < '" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "';";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandDelete(), Query);
            if (response.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean RemoveDuplicateKeys()
        {
            string Query = "DELETE FROM tempkeys WHERE UserId = " + _UserId + ";";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandDelete(), Query);
            if (response.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean AuthenticateUser(string AuthKey)
        {
            RemoveExpiredKeys();
            return AuthenticationKeyIsValid(AuthKey);
        }

        private bool AuthenticationKeyIsValid(string AuthKey)
        {
            string Query = "SELECT COUNT(*) FROM tempkeys WHERE AuthenticationKey = '" + AuthKey + "';";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandCount(), Query);
            if(response.Data == "1")
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool DeleteAuthenticationKey(string AuthKey)
        {
            RemoveExpiredKeys();
            string Query = "DELETE FROM tempkeys WHERE AuthenticationKey = '" + AuthKey + "';";
            InspectionDatabaseManager DBM = InspectionDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = DBM.execute(new DatabaseCommandDelete(), Query);
            if (response.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
