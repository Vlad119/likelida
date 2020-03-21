using System;
using TMPro;
using UnityEngine;

public class MapPrefScript : MonoBehaviour
{
    public GameObject mapPref;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;
    public GameObject s5;
    public TMP_Text name;
    public TMP_Text distance;
    public TMP_Text address;
    public int stars;

    public void OnEnable()
    {
        mapPref.SetActive(true);
        CreatePref();
    }

    public void Close()
    {
        AppManager.Instance.mapPrefOnline = false;
        Destroy(mapPref);
    }
    
    public void CreatePref()
    {
        var AM = AppManager.Instance;
        if (AppManager.Instance.res.company.Count>0)
        {
            name.text = AM.name;
            stars = AM.rating;
            address.text = AM.address;
            if (AM.distance < 1)
            {
                AM.distance *= 1000;
                distance.text = Convert.ToString(AM.distance) + " m";
            }
            else
            {
                distance.text = Convert.ToString(AM.distance) + " km";
            }
            ChangeStars();
        }
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
