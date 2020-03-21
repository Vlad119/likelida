using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngrScript : MonoBehaviour
{
    public TMP_Text name;
    public GameObject checkmark;
    public bool isON;
    Nid _nid;

    public string nidd;
    public string titlee;


    public void CreatePref(Nid nid)
    {
        isON = nid.isON;
        name.text = nid.title;
        _nid = nid;
        SetCheckmark();
        nidd = nid.nid;
        titlee = nid.title;

    }

    public void SetCheckmark()
    {
        if (isON == false)

        {
            checkmark.SetActive(false);
            isON = false;
            Debug.Log("OFF");
        }
        else
        {
            checkmark.SetActive(true);
            isON = true;
            Debug.Log("ON");
        }
    }

    public void Change()
    {
        if (isON == false)
        {
            checkmark.SetActive(true);
            isON = true;
            Debug.Log("ON");
            _nid.isON = true;

        }
        else
        {
            checkmark.SetActive(false);
            isON = false;
            Debug.Log("OFF");
            _nid.isON = false;
        }
    }
}
