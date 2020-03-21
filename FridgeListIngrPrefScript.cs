using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FridgeListIngrPrefScript : MonoBehaviour
{
    public TMP_Text counter;
    public int count = 1;
    public TMP_Text name;
    public RawImage img;
    public List<Nid> prod;
    public int _index;
    public GameObject pref;

    private void OnEnable()
    {
        prod = AppManager.Instance.products;
        counter.text = prod[_index].count.ToString();
    }

    public void Plus()
    {
        if (count < 999)
        {
            counter.text = "";
            count++;
            counter.text = count.ToString();
            prod[_index].count = Convert.ToInt32(counter.text);
        }
    }

    public void Minus()
    {
        if (count > 1)
        {
            counter.text = "";
            count--;
            counter.text = count.ToString();
        }
        else
        {
            DelProd(prod[_index],_index);
        }
    }

    public void DelProd(Nid _prod, int index)
    {
        _index = index;
        prod[index] = _prod;
        prod[index].isON = false;
        prod[index].repeat = false;
        prod[index].count = Convert.ToInt32(counter.text);
        AppManager.Instance.products.Remove(_prod);
        Destroy(pref);
    }

    public void CreatePref(Nid _prod)
    {
        name.text = _prod.title;
    }
}
