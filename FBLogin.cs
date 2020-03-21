using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using UnityEngine.UI;

public class FBLogin : MonoBehaviour
{
    //private void Awake()
    //{
    //    if (FB.IsInitialized)
    //    {
    //        FB.ActivateApp();
    //    }
    //    else
    //    {
    //        //Handle FB.Init
    //        FB.Init(() => {
    //            FB.ActivateApp();
    //        });
    //    }
    //}

    private void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            FB.ActivateApp();
            Debug.Log("FBSDK_DEBUG: FB is logged in.");
        }
        else
        {
            Debug.Log("FBSDK_DEBUG: FB is NOT logged in.");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public IEnumerator Login()
    {
        FB.Init(SetInit, OnHideUnity);
        yield return new WaitForSeconds(.2f);
        List<string> permissions = new List<string>();
        //можно менять список разрешений, к которым нужен доступ
        permissions.Add("public_profile");
        permissions.Add("email");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
        AppManager.Instance.SwitchScreen(3);
    }

    void AuthCallBack(IResult resault)
    {
        if (resault.Error != null)
        {
            Debug.Log(resault.Error);
            if (AccessToken.CurrentAccessToken != null)
            {
                FB.LogOut();
                StartCoroutine(Login());
            }
        }
        else if (resault.Cancelled)
        {
            FB.LogOut();
            Debug.Log("FB login canceled");
            return;
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("PERMISSIONS: " + AccessToken.CurrentAccessToken.Permissions.ToString());
                UserTokens.UpdateToken("FB", AccessToken.CurrentAccessToken.TokenString);
                if (string.IsNullOrEmpty(AppManager.Instance.userInfo.accessToken))
                {
                    string userToSent = UserTokens.PrepareUserToSent(AccessToken.CurrentAccessToken.TokenString, "facebook");
                    WebHandler.Instance.LoginWrapper((resp) =>
                    {
                        AppManager.Instance.userInfo = JsonUtility.FromJson<UserInfo>(resp);
                        Debug.Log(resp);
                        PlayerPrefs.SetString("Token", AppManager.Instance.userInfo.accessToken);
                    }, userToSent);
                }
                Debug.Log("FBLogin " + AppManager.Instance.userInfo.accessToken);
                PlayerPrefs.SetString("Token", AppManager.Instance.userInfo.accessToken);
                //else ///Илья сказал закоментить, если что он во всём виноват =D
                //{
                //    string dataToSend = "{\"accessToken\":\"" + AccessToken.CurrentAccessToken.TokenString + "\", \"social\":\"facebook\"}";
                //    WebHandler.Instance.LinkSocialWrapper((resp) =>
                //    {
                //        Debug.Log("FB LINK");
                //        JsonUtility.FromJsonOverwrite(resp, MainScript.Instance.storage.user);
                //        if (MainScript.Instance.activeScreenIndex != 0)
                //            MainScript.Instance.ReloadScreen();
                //        else
                //            MainScript.Instance.SwitchScreen(1);
                //    }, dataToSend);
                //}
            }
            else
            {
                Debug.Log("FB is not loged in");
            }
        }
    }

    public void FBLogout()
    {
        FB.LogOut();
    }
}