using TMPro;
using UnityEngine;

public class FavoriteDishesScript : MonoBehaviour
{
    public GameObject favoritePref;
    public GameObject ParentFavoritePref;
    public TMP_Text cat_name;
    public GameObject scroll;


    public void Random()
    {
        Random rnd = new Random();
    }


    public void CreatePref(Cats _cat)
    {
        var liked = AppManager.Instance.dishesPlayerPrefs.likedDishes;
        var cats = AppManager.Instance.res.cats;
        cat_name.text = _cat.name;
        foreach (var like in liked)
        {
            if (_cat.tid == like.cat)
            {
                var dish = Instantiate(favoritePref, ParentFavoritePref.transform);
                dish.GetComponent<FavoritePrefScript>().ScrollCellContent(like);
            }
        }
    }
}


