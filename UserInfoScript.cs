using System;

[Serializable]
public class User
{
    public string phone;
    public string pass;
    public string lang;



    public bool firstTime = true;//?

    public string order_fail;//?

    public string order_success;//?

    public string push_token;//?
}

[Serializable]
public class UserInfo
{
    public string access_token;
    public string accessToken;
    public User user;
}
