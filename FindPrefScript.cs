using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindPrefScript : MonoBehaviour
{
    public GameObject findPref;
    public TMP_Text value;
    public  Cats catss;



    public void Select()
    {
        if (!catss.selected)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 139, 00, 255);
            value.color = Color.white;
            catss.selected = true;
            AppManager.Instance.select = true;
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(239, 245, 249, 255);
            value.color = Color.black;
            catss.selected = false;
            AppManager.Instance.select = false;
        }
    }

    public void Check()
    {
        if (catss.selected)
        {
            gameObject.GetComponent<Image>().color = new Color32(255, 139, 00, 255);
            value.color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color32(239, 245, 249, 255);
            value.color = Color.black;
        }
    }

    public void CreatePref(string name, Cats cats)
    {
        value.text = name;
        catss= cats;
        Check();
    }
}
