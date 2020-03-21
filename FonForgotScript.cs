using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonForgotScript : MonoBehaviour
{
    public TMP_InputField login;
    public Image mail;
    public GameObject confirmed;
    public GameObject invalid;
    public string userLogin;
    public bool sended = false;

    public void OnEnable()
    {
        sended = false;
        AppManager.Instance.BottomBar.SetActive(false);
        userLogin = PlayerPrefs.GetString("userLogin");
        Verify();
        if (login.text == "")
        {
            Selected();
        }
    }

    async public void Restore()
    {
        var AM = AppManager.Instance;
        if (!sended && login.text != "")
        {
            sended = true;
            AM.userInfo.user.phone = login.text;
            PlayerPrefs.SetString("userLogin", AM.userInfo.user.phone.ToString());
            string postData = JsonUtility.ToJson(AM.userInfo.user);
            await WebRequests.Request.RequestPost("user/register", postData, (code, response) =>
            {
                if (code == 200)
                {
                    AM.SwitchScreen(11);
                }
            }, false);
        }
    }

    public void Verify()
    {
        if (login.text == userLogin)
        {
            confirmed.SetActive(true);
            invalid.SetActive(false);
        }
        else
        {
            confirmed.SetActive(false);
            invalid.SetActive(true);
        }

        if (login.text == "")
        {
            Selected();
        }
    }

    public async void Deselect()
    {
        var AM = AppManager.Instance;
        if (login.text == "")
        {
            mail.color = new Color32(61, 61, 61, 255);
            login.GetComponent<Image>().color = new Color32(246, 249, 244, 255);
            invalid.SetActive(false);
            confirmed.SetActive(false);
        }
        else
        {
            await WebHandler.Instance.GetDataWrapper((repl) =>
            {
                JsonUtility.FromJsonOverwrite(repl, AM);
                for (int i = 0; i < AM.res.emails.Count; i++)
                    if (AM.res.emails[i] == login.text)
                    {
                        confirmed.SetActive(true);
                        invalid.SetActive(false);
                    }
            });
        }
    }

    public void Selected()
    {
        mail.color = new Color32(61, 61, 61, 255);
        login.GetComponent<Image>().color = new Color32(246, 249, 244, 255);
        invalid.SetActive(false);
        confirmed.SetActive(false);
    }

    public void Changed()
    {
        if (login.text.Length > 0)
        {
            mail.color = new Color32(101, 143, 32, 255);
            login.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            mail.color = new Color32(61, 61, 61, 255);
            login.GetComponent<Image>().color = new Color32(246, 249, 244, 255);
        }
    }
}
