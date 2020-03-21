using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonFilterScript : MonoBehaviour
{
    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public Image minutes;
    public Image watches;
    public TMP_Text time;
    public Slider slider;
    public TMP_Text kcallVal;
    public int minutess = 0;
    public int kcall;

    private void Start()
    {
        AppManager.Instance.hard = "1";
    }

    public void OnEnable()
    {
        AppManager.Instance.BottomBar.SetActive(false);
    }

    public void Plus()
    {
        var AM = AppManager.Instance;
        AM.filter = true;
        AM.changedMinutes = true;
        if (minutess < 480)
        {
            if (minutess % 60 != 0)
            {
                Pl();
            }
            else
            {
                watches.fillAmount = 0f;
                Pl();
            }
        }
        AM.minutes = minutess;
        AM.changedDifficult = true;
    }

    public void Pl()
    {
        minutess = minutess + 10;
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            time.text = minutess.ToString() + " min";
        }
        else
        {
            time.text = minutess.ToString() + " минут";
        }
        watches.fillAmount += 0.1666666667f;
        minutes.rectTransform.Rotate(0, 0, -60);
    }

    public void Minus()
    {
        var AM = AppManager.Instance;
        if (minutess > 0)
        {
            if (minutess % 60 != 0)
            {
                Min();
            }
            else
            {
                watches.fillAmount = 1f;
                Min();
            }
        }
        AM.minutes = minutess;
        AM.changedDifficult = true;
    }


    public void Min()
    {
        minutess = minutess - 10;
        if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
        {
            time.text = minutess.ToString() + " min";
        }
        else
        {
            time.text = minutess.ToString() + " минут";
        }
        watches.fillAmount -= 0.1666666667f;
        minutes.rectTransform.Rotate(0, 0, 60);
    }

    public void ChoiseOne()
    {
        var AM = AppManager.Instance;
        AM.filter = true;
        AM.changedDifficult = true;
        Defualt();
        c1.SetActive(true);
        AM.hard = "1";
    }

    public void ChoiseTwo()
    {
        var AM = AppManager.Instance;
        AM.filter = true;
        AM.changedDifficult = true;
        Defualt();
        c2.SetActive(true);
        AM.hard = "2";
    }

    public void ChoiseThree()
    {
        var AM = AppManager.Instance;
        AM.filter = true;
        AM.changedDifficult = true;
        Defualt();
        c3.SetActive(true);
        AM.hard = "3";
    }

    public void Defualt()
    {
        c1.SetActive(false);
        c2.SetActive(false);
        c3.SetActive(false);
    }

    public void ReturnBottom()
    {
        AppManager.Instance.BottomBar.SetActive(true);
    }

    public void ValueChange()
    {
        var AM = AppManager.Instance;
        AM.filter = true;
        kcall = Convert.ToInt32(slider.value);
        AM.kcall = kcall;
        kcallVal.text = kcall.ToString();
    }

    public void Confirm()
    {
        var screen = AppManager.Instance.screens;
        screen[3].GetComponent<FonReceiptesScript>().ClearAllCells();
        screen[3].GetComponent<FonReceiptesScript>().FilteredDishesPref();
        screen[8].SetActive(false);
    }
}
