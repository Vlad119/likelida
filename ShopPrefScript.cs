using System;
using TMPro;
using UnityEngine;

public class ShopPrefScript : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text cName;
    public TMP_Text distance;
    public TMP_Text cDistance;
    public TMP_Text address;
    public GameObject line;
    public GameObject down;
    public GameObject up;
    public GameObject content;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;
    public GameObject s5;
    public GameObject space;
    public int stars;
    private bool selected = false;

    public void Select()
    {
        if (!selected)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        content.SetActive(true);
        up.SetActive(true);
        down.SetActive(false);
        line.SetActive(false);
        selected = true;
    }

    public void Close()
    {
        content.SetActive(false);
        up.SetActive(false);
        down.SetActive(true);
        line.SetActive(true);
        selected = false;
    }

    public void CreatePref(Company company)
    {
        try
        {
            name.text = company.name;
            address.text = company.address;
            if (company.distance<1)
            {
                company.distance *= 1000; 
                distance.text = Convert.ToString(company.distance) + " m";
            }
            else
            {
                distance.text = Convert.ToString(company.distance) + " km";
            }
            stars = company.rating;
            ChangeStars();
        }
        catch { }
    }

    public void ChangeStars()
    {
        AllClose();
        switch (stars)
        {
            case 1:
                { ChangeStar1(); break; }
            case 2:
                { ChangeStar2(); break; }
            case 3:
                { ChangeStar3(); break; }
            case 4:
                { ChangeStar4(); break; }
            case 5:
                { ChangeStar5(); break; }
        }
    }


    public void AllClose()
    {
        s1.SetActive(false);
        s2.SetActive(false);
        s3.SetActive(false);
        s4.SetActive(false);
        s5.SetActive(false);
    }

    public void ChangeStar1()
    {
        s1.SetActive(true);
    }

    public void ChangeStar2()
    {
        s1.SetActive(true);
        s2.SetActive(true);
    }

    public void ChangeStar3()
    {
        s1.SetActive(true);
        s2.SetActive(true);
        s3.SetActive(true);
    }

    public void ChangeStar4()
    {
        s1.SetActive(true);
        s2.SetActive(true);
        s3.SetActive(true);
        s4.SetActive(true);
    }

    public void ChangeStar5()
    {
        s1.SetActive(true);
        s2.SetActive(true);
        s3.SetActive(true);
        s4.SetActive(true);
        s5.SetActive(true);
    }
}
