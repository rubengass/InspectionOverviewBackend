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
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = UDBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if(response.Data.Value.Count() != 0)
            {
                _UserId = Convert.ToInt32(response.Data.Value[0]);
                _Username = response.Data.Value[1];
                _Password = response.Data.Value[2];
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetUserId(string AuthKey)
        {
            string Query = "SELECT UserId FROM tempkeys WHERE AuthenticationKey = '" + AuthKey + "';";
            SelectReference reference = new SelectReference();
            reference.SingleReturnReference();
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<SelectReference> response = new Response<SelectReference>();
            response = UDBM.fetch(new DatabaseCommandSelect(), Query, reference);
            if (response.Data.Value.Count() != 0)
            {
                return response.Data.Value[0];
            }
            else
            {
                return null;
            }
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
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = UDBM.execute(new DatabaseCommandCount(), Query);
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
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = UDBM.execute(new DatabaseCommandInsert(), Query);
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
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = UDBM.execute(new DatabaseCommandDelete(), Query);
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
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = UDBM.execute(new DatabaseCommandDelete(), Query);
            if (response.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Response<string> AuthenticateUser(string AuthKey)
        {
            Response<string> response = new Response<string>();
            if (AuthenticationKeyIsValid(AuthKey))
            {
                RemoveExpiredKeys();
                if (AuthenticationKeyIsValid(AuthKey))
                {
                    response.GenericSuccessCode();
                    return response;
                } else
                {
                    response.ExpiredAuthentication();
                    return response;
                }
            } else
            {
                RemoveExpiredKeys();
                response.FailedToAuthenticate();
                return response;
            }
        }

        private bool AuthenticationKeyIsValid(string AuthKey)
        {
            string Query = "SELECT COUNT(*) FROM tempkeys WHERE AuthenticationKey = '" + AuthKey + "';";
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = UDBM.execute(new DatabaseCommandCount(), Query);
            if(response.Data == "1")
            {
                return true;
            } else
            {
                return false;
            }
        }

        public Response<string> DeleteAuthenticationKey(string AuthKey)
        {
            RemoveExpiredKeys();
            string Query = "DELETE FROM tempkeys WHERE AuthenticationKey = '" + AuthKey + "';";
            UserDatabaseManager UDBM = UserDatabaseManager.getInstance();
            Response<string> response = new Response<string>();
            response = UDBM.execute(new DatabaseCommandDelete(), Query);
            if (response.Success)
            {
                response.GenericSuccessCode();
                return response;
            }
            else
            {
                response.FailedToContactServer();
                return response;
            }
        }
    }
}
