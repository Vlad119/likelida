using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FonResultScript : MonoBehaviour
{
    public GameObject findPref;
    public GameObject foodPref;
    public GameObject ParentFindPref;
    public GameObject ParentFoodPref;
    public GameObject background;
    public GameObject NotFound;
    public int counter = 0;

    public void InstanceFindPref()
    {
        var res = AppManager.Instance.res;
        List<Cats> cat = res.cats.Where(s => s.selected).ToList();
        //Debug.Log("cat.Count " + cat.Count);
        if (cat.Count > 0)
        {
            counter++;
            for (int i = 0; i < cat.Count; i++)
            {
                var dish = Instantiate(findPref, ParentFindPref.transform);
                dish.GetComponent<FindPrefScript>().CreatePref(cat[i].name, cat[i]);
            }
        }
        if (counter == 0)
        {
            NotFound.SetActive(true);
        }
    }



    public void InstanceFoodPref()
    {
        counter = 0;
        var res = AppManager.Instance.res;
        List<Cats> cat = res.cats.Where(s => s.selected).ToList();
        //Debug.Log("cat.Count " + cat.Count);
        for (int i = 0; i < cat.Count; i++)
        {
            List<Dish> found = res.dish.Where(d => d.cat.Contains(cat[i].tid)).ToList();
            //Debug.Log("found.Count " + found.Count);
            if (found.Count > 0)
            {
                counter++;
                for (int j = 0; j < found.Count; j++)
                {
                    var dish = Instantiate(foodPref, ParentFoodPref.transform);
                    dish.GetComponent<FoodPrefScript>().ScrollCellContent(found[j]);
                }
            }
        }
        if (counter == 0)
        {
            NotFound.SetActive(true);
        }
    }


    public void ClearAllCells()
    {
        ParentFindPref.transform.ClearChildren();
        ParentFoodPref.transform.ClearChildren();
    }

    public void InstanceProductsWithIngredients()
    {
        int count = 0;
        var AM = AppManager.Instance;
        for (int i = 0; i < AM.res.dish.Count; i++)
            for (int j = 0; j < AM.res.dish[i].ing.Count; j++)
                for (int k = 0; k < AM.products.Count; k++)
                    if (AM.res.dish[i].ing[j].target_id == AM.products[k].nid)
                    {
                        count++;
                        if (AM.res.dish[i].ing.Count == count)
                        {
                            var dish = Instantiate(foodPref, ParentFoodPref.transform);
                            dish.GetComponent<FoodPrefScript>().ScrollCellContent(AM.res.dish[i]);
                            count = 0;
                        }
                    }
    }

    public void OnScreen()
    {
        var AM = AppManager.Instance;
        switch (AM.way)
        {
            case 0:
                {
                    NotFound.SetActive(true); break;
                }
            case 1:
                {
                    Search(); break;
                }
            case 2:
                {
                    InstanceFindPref();
                    InstanceFoodPref();
                    break;
                }
            case 3:
                {
                    InstanceFindPref();
                    Search();
                    break;
                }
            case 4:
                {
                    InstanceProductsWithIngredients();
                    break;
                }
        }
    }


    public void Search()
    {
        counter = 0;
        background.SetActive(false);
        var res = AppManager.Instance.res;
        List<Dish> found = res.dish.Where(s => s.name.ToUpper().Contains(AppManager.Instance.searching.ToUpper())).ToList();
        if (found.Count > 0)
        {
            counter++;
            for (int i = 0; i < found.Count; i++)
            {
                var dish = Instantiate(foodPref, ParentFoodPref.transform);
                dish.GetComponent<FoodPrefScript>().ScrollCellContent(found[i]);
            }
        }
        if (counter == 0)
        {
            NotFound.SetActive(true);
        }
    }


    public void Check()
    {
        var AM = AppManager.Instance;
        AM.way = 0;
        if (AM.searching != "")
        { AM.way = 1; }
        if (AM.select)
        { AM.way = 2; }
        if (AM.searching != "" && AM.select)
        { AM.way = 3; }
    }


    private void OnEnable()
    {
        NotFound.SetActive(false);
        background.SetActive(true);
        var AM = AppManager.Instance;
        ClearAllCells();
        Check();
        OnScreen();
        AM.fonLoad.SetActive(true);
        AM.BottomBar.SetActive(false);
    }
}
