using UnityEngine;
using System;

[Serializable]
public static class UserTokens
{
    public static string facebookToken;
    public static string instagramToken;
    public static string vkToken;

    public static bool firstTime;



    /// <summary>
    /// Update token field using social network name.
    /// <param name="socialNetworkName">Uses shortcuts of social network name (FB, INST, VK) to update it's token.</param>
    /// <param name="token">Token string parameter.</param>
    /// </summary>
    public static void UpdateToken(string socialNetworkName, string token)
    {
        switch(socialNetworkName)
        {
            case "FB":
                facebookToken = token;
                break;
            case "INST":
                instagramToken = token;
                break;
            case "VK":
                vkToken = token;
                break;
            default:
                Debug.LogError("No such token field");
                break;
        }
    }

    public static string PrepareUserToSent(string token, string sn)
    {
        LoginObject userToSent = new LoginObject(token, sn);
        return JsonUtility.ToJson(userToSent);
    }
}
