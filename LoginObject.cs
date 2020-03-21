using System;

[Serializable]
public class LoginObject
{
    public string accessToken;
    public string social;

    public LoginObject(string token, string sn)
    {
        accessToken = token;
        social = sn;
    }
}
