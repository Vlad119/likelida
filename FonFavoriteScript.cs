using UnityEngine;

public class FonFavoriteScript : MonoBehaviour
{
    public GameObject favoriteDishesPref;
    public GameObject ParentFavoriteDishesPref;
    public GameObject space;
    public GameObject needReg;
    public GameObject noFavorite;

    public void OnEnable()
    {
        var AM = AppManager.Instance;
        var DPP = AM.dishesPlayerPrefs;
        ClearAllCells();
        noFavorite.SetActive(false);
        needReg.SetActive(false);
        if (AM.noLogin == 0)
        {
            AM.White();
            AM.favorite.SetActive(false);
            AM.favoriteON.SetActive(true);
            if (DPP.likedDishes.Count > 0)
            {
                InstanceDishesPref();
            }
            else
            {
                noFavorite.SetActive(true);
            }
        }
        else
        {
            needReg.SetActive(true);
        }
    }


    public void InstanceDishesPref()
    {
        var AM = AppManager.Instance;
        var cats = AM.res.cats;
        foreach (var cat in cats)
            foreach (var liked in AppManager.Instance.dishesPlayerPrefs.likedDishes)
            {
                if (cat.tid == liked.cat && cat.likedCount == 0)
                {
                    var dish = Instantiate(favoriteDishesPref, ParentFavoriteDishesPref.transform);
                    dish.GetComponent<FavoriteDishesScript>().CreatePref(cat);
                    cat.likedCount++;
                }
            }
        Instantiate(space, ParentFavoriteDishesPref.transform);
        foreach (var cat in cats)
        {
            cat.likedCount = 0;
        }
    }


    public void ClearAllCells()
    {
        ParentFavoriteDishesPref.transform.ClearChildren();
    }
}
