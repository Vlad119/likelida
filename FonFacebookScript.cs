using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonFacebookScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        if (AppManager.Instance.userInfo.accessToken.Length > 0)
        {
            AppManager.Instance.userInfo.access_token = AppManager.Instance.userInfo.accessToken;
            AppManager.Instance.SwitchScreen(3);
            Debug.Log("accessToken " + AppManager.Instance.userInfo.accessToken);
            Debug.Log("access_token " + AppManager.Instance.userInfo.access_token);
        }
        else
        {
            AppManager.Instance.FBLoginWrapper();
        }
    }
}
