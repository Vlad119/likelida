using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FridgeIngrPrefScript : MonoBehaviour
{
    public TMP_Text counter;
    public RawImage img;
    public TMP_Text name;
    public Nid prod;
    public GameObject pref;
    public GameObject plus;
    public GameObject minus;
    bool b = false;


    public void Plus()
    {
        if (prod.count < 11)
        {
            prod.count++;
            counter.text = Convert.ToString(prod.count);
        }
    }

    public void Minus()
    {
        if (prod.count > 1)
        {
            prod.count--;
            counter.text = Convert.ToString(prod.count);
        }
        else
        {
           DelProd();
        }
    }

    public void DelProd()
    {
        prod.isON = false;
        prod.repeat = false;
        AppManager.Instance.products.Remove(prod);
        GetComponentInParent<FonFridgeScript>().OnEnable();
        Destroy(pref);
    }

    async public void CreatePref(Nid _prod)
    {
        prod = _prod;
        name.text = _prod.title;
        counter.text = Convert.ToString(_prod.count);
        img.texture = await LoadImg(_prod.img);
    }

    async Task<Texture> LoadImg(string endpoint)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(endpoint))
        {
            await request.SendWebRequest();
            var texture = DownloadHandlerTexture.GetContent(request);
            return texture;
        }
    }

    public void ShowHide()
    {
        if(b)
        {
            plus.SetActive(false);
            minus.SetActive(false);
            b = false;
        }
        else
        {
            plus.SetActive(true);
            minus.SetActive(true);
            b = true;
        }
    }
}
