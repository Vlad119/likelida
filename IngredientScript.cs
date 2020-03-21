using TMPro;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    public TMP_Text value;
    public GameObject green;
    public GameObject up;
    public GameObject down;
    public GameObject content;
    public GameObject space;
    public GameObject line;
    public GameObject ingPref;
    Ingrediens_cat _iCat;

    private bool opened = false;

    public void OpenORClose()
    {
        if (!opened)
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
        value.color = Color.white;
        green.SetActive(true);
        content.SetActive(true);
        down.SetActive(false);
        up.SetActive(true);
        space.SetActive(true);
        line.SetActive(false);
        opened = true;
        content.transform.ClearChildren();
        ViewPref(_iCat);
    }

    public void Close()
    {
        value.color = Color.black;
        green.SetActive(false);
        content.SetActive(false);
        down.SetActive(true);
        up.SetActive(false);
        space.SetActive(false);
        line.SetActive(true);
        opened = false;
    }

    public void CreatePref(Ingrediens_cat iCat)
    {
        value.text = iCat.name;
        _iCat = iCat;
    }

    public void ViewPref(Ingrediens_cat iCat)
    {
        for (int i = 0; i < iCat.nids.Count; i++)
        {
            var ingr = Instantiate(ingPref, content.transform);
            ingr.GetComponent<IngrScript>().CreatePref(iCat.nids[i]);
        }
    }

}
