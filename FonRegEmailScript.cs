using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonRegEmailScript : MonoBehaviour
{
    public Toggle toggle;
    public bool comfirmed = false;
    public bool sended = false;
    public TMP_InputField login;
    public Image reg;

    public void OnEnable()
    {
        AppManager.Instance.BottomBar.SetActive(false);
        reg.color = new Color32(255, 139, 0, 255);
        sended = false;
        comfirmed = false;
    }

    public void Check()
    {
        if (!comfirmed)
        {
            toggle.GetComponent<Toggle>().isOn = true;
            comfirmed = true;
            if (!string.IsNullOrEmpty(login.text) && login.text.Contains("@") && login.text.Contains("."))
            {
                reg.color = new Color32(0, 176, 44, 255);
            }
        }
        else
        {
            toggle.GetComponent<Toggle>().isOn = false;
            comfirmed = false;
            reg.color = new Color32(255, 139, 0, 255);
        }
    }


    public void InputCheck()
    {
        if (!string.IsNullOrEmpty(login.text) && login.text.Contains("@") && login.text.Contains("."))
        {
            reg.color = new Color32(0, 176, 44, 255);
        }
        else
        {
            reg.color = new Color32(255, 139, 0, 255);
        }
    }

    public async void Create()
    {
        var AM = AppManager.Instance;
        if (comfirmed && !sended && !string.IsNullOrEmpty(login.text) && login.text.Contains("@") && login.text.Contains("."))
        {
            AM.userInfo.user.phone = login.text;
            reg.color = new Color32(0, 176, 44, 255);
            if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
            {
                AM.userInfo.user.lang = "en";
            }
            else
            {
                AM.userInfo.user.lang = "ru";
            }
            PlayerPrefs.SetString("userLogin", AM.userInfo.user.phone.ToString());
            string postData = JsonUtility.ToJson(AM.userInfo.user);
            await WebRequests.Request.RequestPost("user/register", postData, (code, response) =>
            {
                if (code == 200)
                {
                    sended = true;
                    AM.popup.SetActive(true);
                    AM.SwitchScreen(11);
                }
            }, false);
        }
        else
        {
            reg.color = new Color32(255, 139, 0, 255);
        }
    }
}
