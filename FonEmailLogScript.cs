using Assets.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonEmailLogScript : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField pass;
    public Image mail;
    public GameObject confirmed;
    public GameObject invalid;
    public string userLogin;
    private void Start()
    {
        if (AppManager.Instance.userInfo.access_token.Length > 0)
        {
            AppManager.Instance.SwitchScreen(3);
        }
    }

    public void OnEnable()
    {
        pass.text = "";
        AppManager.Instance.BottomBar.SetActive(false);
        login.text = PlayerPrefs.GetString("userLogin");
        userLogin = PlayerPrefs.GetString("userLogin");
        Verify();
        if (login.text == "")
        {
            Selected();
        }
    }

    public async void Login()
    {
        var AM = AppManager.Instance;
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            AM.userInfo.user.lang = "en";
        }
        else
        {
            AM.userInfo.user.lang = "ru";
        }
        UserAuth auth = new UserAuth($"{login.text}:{pass.text}:{AM.userInfo.user.lang}");
        await WebRequests.Request.RequestPost("user/login", JsonUtility.ToJson(auth), (code, response) =>
        {
            if (code == 200)
            {
                Debug.Log(response.Replace('"', ' ').Trim());
                AppManager.Instance.userInfo.access_token = response.Replace('"', ' ').Trim();
                if (AppManager.Instance.userInfo.access_token == "false")
                {
                    pass.contentType = TMP_InputField.ContentType.Standard;
                    pass.ActivateInputField();
                    if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
                    {
                        pass.text = "Wrong password. Try again";
                    }
                    else
                    {
                        pass.text = "Неверный пароль. Повторите попытку";
                    }
                }
                else
                {
                    WebRequests.Request.RequestPost("user/update", JsonUtility.ToJson(AM.userInfo), (code1, respons1e) =>
                    { 
                        Debug.Log("code1 "+ code1);
                        Debug.Log("respons1e " + respons1e); 
                        JsonUtility.FromJsonOverwrite(response, AppManager.Instance);
                    }, true);
                    PlayerPrefs.SetString("Token", response.Replace('"', ' ').Trim());
                    PlayerPrefs.SetString("pass", pass.text);
                    PlayerPrefs.Save();
                    AppManager.Instance.BottomBar.SetActive(true);
                    AppManager.Instance.SwitchScreen(3);
                }
            }
        }, false);
        while (AppManager.Instance.userInfo.access_token.Length == 0)
            await new WaitForSeconds(0.01f);
        await WebRequests.Request.RequestGet("getdata?action=mydata", (code, response) =>
        {
            JsonUtility.FromJsonOverwrite(response, AppManager.Instance);
        }, true);
    }


    public void Registration()
    {
        AppManager.Instance.SwitchScreen(12);
    }

    public void Forgot()
    {
        AppManager.Instance.SwitchScreen(14);
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

    async public void Deselect()
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

    public void ShowPass()
    {
        pass.contentType = TMP_InputField.ContentType.Standard;
        pass.ActivateInputField();
    }
}
