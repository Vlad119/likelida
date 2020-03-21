using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonSettingsScript : MonoBehaviour
{
    public GameObject enable;
    public GameObject disable;
    public GameObject s11;
    public GameObject s12;
    public GameObject s21;
    public GameObject s22;
    public GameObject s31;
    public GameObject s32;
    public GameObject setting1;
    public GameObject setting2;
    public GameObject setting3;
    public TMP_Text s1value;
    public TMP_Text s2value;
    public TMP_Text s3value;
    public TMP_Text s4value;
    public Image img;
    public bool activated;

    private void Start()
    {
        activated = false;
    }

    public void OnEnable()
    {
        AppManager.Instance.BottomBar.SetActive(false);
        if(Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            ChangeS31();
        }
        else
        {
            ChangeS32();
        }
    }


    public void NotificationON()
    {
        activated = true;
        enable.SetActive(true);
        disable.SetActive(false);
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            s3value.text = "Enabled";
        }
        else
        {
            s3value.text = "Включены";
        }
        img.color = new Color32(101, 143, 32, 255);
    }


    public void NotificationOFF()
    {
        activated = false;
        disable.SetActive(true);
        enable.SetActive(false);
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            s3value.text = "Disabled";
        }
        else
        {
            s3value.text = "Отключены";
        }
        img.color = new Color32(227, 66, 65, 255);
    }

    public void ChangeNotificationSettings()
    {
        if (!activated)
        {
            NotificationON();
        }
        else
        {
            NotificationOFF();
        }
    }

    public void ChangeS11()
    {
        s11.SetActive(true);
        s12.SetActive(false);
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            s1value.text = "None";
        }
        else
        {
            s1value.text = "Нет";
        }
    }

    public void ChangeS12()
    {
        s12.SetActive(true);
        s11.SetActive(false);
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            s1value.text = "Vegetarian";
        }
        else
        {
            s1value.text = "Для вегетарианцев";
        }
    }

    public void ChangeS21()
    {
        s21.SetActive(true);
        s22.SetActive(false);
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            s2value.text = "US Only";
        }
        else
        {
            s2value.text = "Американская";
        }
    }

    public void ChangeS22()
    {
        s22.SetActive(true);
        s21.SetActive(false);
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            s2value.text = "Metric + US";
        }
        else
        {
            s2value.text = "Метрическая + Американская";
        }
    }

    public void ChangeS31()
    {
        s31.SetActive(true);
        s32.SetActive(false);
        s4value.text = "English";
        Lean.Localization.LeanLocalization.CurrentLanguage = "English";
    }

    public void ChangeS32()
    {
        s32.SetActive(true);
        s31.SetActive(false);
        s4value.text = "Русский";
        Lean.Localization.LeanLocalization.CurrentLanguage = "Russian";
    }

    public void Done()
    {
        setting1.SetActive(false);
        setting2.SetActive(false);
        setting3.SetActive(false);
    }

    public void OpenSetting1()
    {
        setting1.SetActive(true);
    }

    public void OpenSetting2()
    {
        setting2.SetActive(true);
    }

    public void OpenSetting3()
    {
        setting3.SetActive(true);
    }

    public void Feedback()
    {
        Application.OpenURL("mailto:" + "rasodallas@gmail.com" + "?subject=");
    }

    public void Legal()
    {
        //
    }


    public void SignOut()
    {
        PlayerPrefs.DeleteAll();
        AppManager.Instance.SwitchScreen(0);
    }

    public void Delete()
    {

    }
}
