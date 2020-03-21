using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonUserScript : MonoBehaviour
{
    public GameObject favoriteDishesPref;
    public GameObject ParentFavoriteDishesPref;
    public GameObject ParentViewedDishesPref;
    public GameObject ParentReviewPref;
    public GameObject reviewPref;
    public RawImage img;
    public GameObject favorite;
    public GameObject review;
    public TMP_Text userName;
    public void OnEnable()
    {
        var AM = AppManager.Instance;
        if (AM.userInfo.user.phone!=null)
        {
            userName.text = AM.userInfo.user.phone;
        }
        ClearAllCells();
        FavoriteDishesPref();
        ViewedDishesPref();
        ReviewPref();
        AM.BottomBar.SetActive(true);
        AM.user.SetActive(false);
        AM.userON.SetActive(true);
        AM.White();
        if (AM.noLogin==1)
        {
            favorite.SetActive(false);
            review.SetActive(false);
        }
        else
        {
            favorite.SetActive(true);
            review.SetActive(true);
        }
    }

    public void Settings()
    {
        AppManager.Instance.SwitchScreen(15);
    }

    public void ViewAllFavorite()
    {
        var AM = AppManager.Instance;
        AM.SwitchScreen(6);
        AM.user.SetActive(true);
        AM.userON.SetActive(false);
        AM.favoriteON.SetActive(true);
        AM.favorite.SetActive(false);
    }

    public void ViewAllReviews()
    {

    }

    public void ReviewPref()
    {
        var DPP = AppManager.Instance.dishesPlayerPrefs;
        foreach (var review in  DPP.userReviews)
        {
            var rev = Instantiate(reviewPref, ParentReviewPref.transform);
            rev.GetComponent<ReviewPrefScript>().CreatePref2(review);
        }
    }


    public void FavoriteDishesPref()
    {
        var DPP = AppManager.Instance.dishesPlayerPrefs;
        for (int i = 0; i < DPP.likedDishes.Count; i++)
        {
            var dish = Instantiate(favoriteDishesPref, ParentFavoriteDishesPref.transform);
            dish.GetComponent<FoodPrefScript>().ScrollCellContent(DPP.likedDishes[i]);
        }
    }

    public void ViewedDishesPref()
    {
        var DPP = AppManager.Instance.dishesPlayerPrefs;
        for (int i = 0; i < DPP.viewedDishes.Count; i++)
        {
            {
                var dish = Instantiate(favoriteDishesPref, ParentViewedDishesPref.transform);
                dish.GetComponent<FoodPrefScript>().ScrollCellContent(DPP.viewedDishes[i]);
            }
        }
    }

    public void ClearAllCells()
    {
        ParentFavoriteDishesPref.transform.ClearChildren();
        ParentViewedDishesPref.transform.ClearChildren();
        ParentReviewPref.transform.ClearChildren();
    }

    //public void ChooseImageFromGallery()
    //{
    //    PickImage(1024);
    //}

    //private void PickImage(int maxSize)
    //{
    //    Texture2D texture = new Texture2D(1, 1);
    //    NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
    //    {
    //        Debug.Log("Image path: " + path);
    //        if (path != null)
    //        {
    //            AppManager.Instance.path = path;
    //            var bt = File.ReadAllBytes(path);
    //            //AppManager.Instance.bytes = File.ReadAllBytes(path);
    //            texture.LoadImage(bt);
    //            if (texture == null)
    //            {
    //                Debug.Log("Couldn't load texture from " + path);
    //                return;
    //            }
    //        }
    //    }, "Выберите изображения для объявления", "image/png", maxSize);
    //    Debug.Log("Permission result: " + permission);
    //    img.texture = texture;
    //}
}
