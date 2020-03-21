using UnityEngine;

public class FonLoginScript : MonoBehaviour
{
    public float timeRemaining = 1f;
    public void NoLogin()
    {
        var AM = AppManager.Instance;
        if (AM.userInfo.access_token == "" || AM.userInfo.accessToken == "")
        {
            AM.noLogin = 1;
        }
        AM.SwitchScreen(1);
    }

    public void Facebook()
    {
        AppManager.Instance.SwitchScreen(2);
    }

    public void Mail()
    {
        AppManager.Instance.SwitchScreen(11);
    }

    private void OnEnable()
    {
        var AM = AppManager.Instance;
        AM.noLogin = PlayerPrefs.GetInt("nologin");
        AM.BottomBar.SetActive(false);
        AM.userInfo.access_token = PlayerPrefs.GetString("Token");
        AM.userInfo.accessToken = PlayerPrefs.GetString("Token");
        AM.userInfo.user.phone = PlayerPrefs.GetString("userLogin");
        AM.userInfo.user.pass = PlayerPrefs.GetString("pass");
    }

    void Update()
    {
        var info = AppManager.Instance.userInfo;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0 && AppManager.Instance.noLogin == 1)
            {
                AppManager.Instance.SwitchScreen(3);
            }
            if (timeRemaining < 0 && info.user.phone != "")
            {
                AppManager.Instance.SwitchScreen(11);
            }
            if (timeRemaining < 0 && info.accessToken != "")
            {
                AppManager.Instance.SwitchScreen(2);
            }
        }
    }
}
