using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class UserAuth
    {
        public string social = "email";
        public string accessToken;

        public UserAuth(string _social, string _access)
        {
            social = _social;
            accessToken = _access;
        }

        public UserAuth(string _access)
        {
            accessToken = _access;
        }
    }
}
