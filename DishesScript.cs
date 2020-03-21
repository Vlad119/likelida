using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DishesScript : MonoBehaviour
{
    public GameObject foodPref;
    public GameObject ParentFoodPref;
    public TMP_Text cat_name;
    public List<Dish> dishToPref = new List<Dish>();

    #region Create Pref with parameters
    public void CreatePref(Cats cat, bool _newest)
    {
        var AM = AppManager.Instance;
        var dishes = AM.res.dish;
        System.Random rnd = new System.Random();
        cat_name.text = cat.name;
        foreach (var dish in dishes)
        {
            if (cat.tid == dish.cat 
                && dish.time == Convert.ToString(AM.minutes)
                && dish.difficult == AM.hard)
            //если tid категории совпадает с категорией рандомного блюда 
            //и время рандомного блюда совпадает с AM.minutes
            //и сложность рандомного блюда совпадает с AM.hard
            {
                dishToPref.Add(dish);
            }
        }

        if (dishToPref.Count > 6)
        {
            int count = dishToPref.Count - 6;
            for (int i = 1; i < count; i++)
            {
                int r = rnd.Next(0, dishToPref.Count - 1);
                dishToPref.RemoveAt(r);
            }
        }

        foreach (var dish in dishToPref)
        {
            var d = Instantiate(foodPref, ParentFoodPref.transform);
            d.GetComponent<FoodPrefScript>().ScrollCellContent(dish);
        }
    }
    #endregion

    #region Create Pref without parameters
    public void CreatePref2(Cats _cat, bool _newest)
    {
        var dishes = AppManager.Instance.res.dish;
        System.Random rnd = new System.Random();
        cat_name.text = _cat.name;
        foreach (var dish in dishes)
        {
            if (_cat.tid == dish.cat && dish.newest == _newest)
            {
                dishToPref.Add(dish);
            }
        }

        if (dishToPref.Count > 6)
        {
            int count = dishToPref.Count - 6;
            for (int i = 1; i < count; i++)
            {
                int r = rnd.Next(0, dishToPref.Count-1);
                dishToPref.RemoveAt(r);
            }
        }

        foreach (var dish in dishToPref)
        {
            var d = Instantiate(foodPref, ParentFoodPref.transform);
            d.GetComponent<FoodPrefScript>().ScrollCellContent(dish);
        }
    }
    #endregion
}
