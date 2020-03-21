using UnityEngine;
using UnityEngine.UI;

public class FonAddFoodScript : MonoBehaviour
{
    public GameObject igredientPref;
    public GameObject ParentIgredientPref;
    public Ingrediens_cat _iCat;


    public void OnEnable()
    {
        var AM = AppManager.Instance;
        AM.BottomBar.SetActive(true);
        AM.fridge.SetActive(true);
        AM.fridgeON.SetActive(false);
        //AM.BackInBlack();
        ParentIgredientPref.transform.ClearChildren();
        ViewPref();
    }

    public void Shop()
    {
        var AM = AppManager.Instance;
        AM.SwitchScreen(13);
        AM.BottomBar.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }


    public void ViewPref()
    {
        var iCat = AppManager.Instance.res.ingrediens_cat;
        for (int i = 0; i < iCat.Count; i++)
        {
            {
                var rev = Instantiate(igredientPref, ParentIgredientPref.transform);
                rev.GetComponent<IngredientScript>().CreatePref(iCat[i]);               
            }
        }
    }


    public void AddToFridge()
    {
        var cat = AppManager.Instance.res.ingrediens_cat;
        for (int i = 0; i < cat.Count; i++)
        {
            for (int j = 0; j < cat[i].nids.Count; j++)
            {
                if (cat[i].nids[j].isON && !cat[i].nids[j].repeat)
                {
                    AppManager.Instance.products.Add(cat[i].nids[j]);
                    cat[i].nids[j].repeat = true;
                }
            }
        }
        AppManager.Instance.SwitchScreen(7);
    }
}



