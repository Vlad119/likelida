using EasyMobile;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FonReceiptScript : MonoBehaviour
{
    public TMP_Text reviewLabel;
    public GameObject content;
    public TMP_Text name;
    public TMP_Text kcal;
    public TMP_Text time;
    public TMP_Text serving;
    public TMP_Text alt;
    public RawImage mainImage;
    public GameObject CookPref;
    public GameObject ParentCookPref;
    public GameObject IngrPref;
    public GameObject ParentIngrPref;
    public GameObject ReviewPref;
    public GameObject ParentReviewPref;
    public GameObject liked;
    public List<string> ingrList = new List<string>();
    public string shareIngrList;
    public GameObject like;
    public GameObject share;
    public GameObject review;
    public GameObject search;
    public GameObject sharelist;
    Dish dish = new Dish();
    public int id;
    public int i = 0;

    public void Search()
    {
        var AM = AppManager.Instance;
        AM.SwitchScreen(13);
        AM.BottomBar.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }

    public void Like()
    {
        var AM = AppManager.Instance;
        var DPP = AM.dishesPlayerPrefs;
        var cats = AM.res.cats;
        like.SetActive(true);
        if (!AM.currentDish.like)
        {
            AM.currentDish.like = true;
            liked.SetActive(true);
            foreach (var cat in cats)
            {
                if (cat.tid == AM.currentDish.cat)
                {
                    cat.likedCount++;
                }
            }
            DPP.likedDishes.Add(AM.currentDish);
            string s = JsonUtility.ToJson(DPP);
            PlayerPrefs.SetString("dish", s);
        }
        else
        {
            AM.currentDish.like = false;
            foreach (var cat in cats)
            {
                if (cat.tid == AM.currentDish.cat)
                {
                    cat.likedCount--;
                }
            }
            for (int i = 0; i < DPP.likedDishes.Count; i++)
            {
                if (DPP.likedDishes[i].id == AM.currentDish.id)
                {
                    DPP.likedDishes.Remove(DPP.likedDishes[i]);
                }
            }
            string s = JsonUtility.ToJson(DPP);
            PlayerPrefs.SetString("dish", s);
            liked.SetActive(false);
        }
        AM.screens[6].GetComponent<FonFavoriteScript>().OnEnable();
        OnEnable();
    }

    public void CheckLike()
    {
        if (AppManager.Instance.currentDish.like)
        {
            liked.SetActive(true);
        }
        else
        {
            liked.SetActive(false);
        }
    }

    public void NoLogin()
    {
        var AM = AppManager.Instance;
        if (AM.noLogin == 1)
        {
            share.SetActive(false);
            like.SetActive(false);
            review.SetActive(false);
            liked.SetActive(false);
            search.SetActive(false);
            sharelist.SetActive(false);
            if (Lean.Localization.LeanLocalization.CurrentLanguage == "English")
            {
                reviewLabel.text += " (log in to leave a review)";
            }
            else
            {
                reviewLabel.text += " (авторизуйтесь, чтобы оставить отзыв)";
            }
        }
        else
        {
            share.SetActive(true);
            like.SetActive(true);
            review.SetActive(true);
            liked.SetActive(true);
            search.SetActive(true);
            sharelist.SetActive(true);
        }
    }

    public async void OnEnable()
    {
        var AM = AppManager.Instance;
        dish = AM.currentDish;
        NoLogin();
        var trans = content.GetComponent<RectTransform>().transform;
        trans.position = new Vector3(trans.position.x, 0, trans.position.z);
        mainImage.texture = await LoadImg(dish.img[0].url);
        CheckLike();
        AM.BottomBar.SetActive(true);
        AM.Default();
        ParentCookPref.transform.ClearChildren();
        ParentReviewPref.transform.ClearChildren();
        ParentIngrPref.transform.ClearChildren();
        LoadMainDish(dish);
        try
        {
            ViewPref(dish);
        }
        catch
        { }
    }


    public void ViewPref(Dish dish)
    {
        dish = AppManager.Instance.currentDish;
        for (i = 0; i < dish.cooking.Count; i++)
        {
            var cook = Instantiate(CookPref, ParentCookPref.transform);
            cook.GetComponent<CookPrefScript>().CreatePref(dish.cooking[i], i);
        }
        for (i = 0; i < dish.reviews.Count; i++)
        {
            var rev = Instantiate(ReviewPref, ParentReviewPref.transform);
            rev.GetComponent<ReviewPrefScript>().CreatePref(dish.reviews[i]);
        }

        var ingred = AppManager.Instance.res.ingrediens;
        for (int j = 0; j < dish.ing.Count; j++)
            for (i = 0; i < ingred.Count; i++)
            {
                if (dish.ing[j].target_id == ingred[i].id)
                {
                    var rev = Instantiate(IngrPref, ParentIngrPref.transform);
                    rev.GetComponent<IngrPrefScript>().CreatePref(ingred[i]);
                    ingrList.Add(ingred[i].name);
                }
            }
        for (int i = 0; i < ingrList.Count; i++)
        {
            shareIngrList += ingrList[i];
            shareIngrList += "\n";
        }
    }


    public async void LoadMainDish(Dish dish)
    {
        dish = AppManager.Instance.currentDish;
        name.text = dish.name;
        kcal.text = dish.kcal;
        time.text = dish.time;
        serving.text = dish.serving;
        alt.text = dish.description;
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


    public void LeaveReview()
    {
        var AM = AppManager.Instance;
        if (AM.noLogin == 0)
        {
            AppManager.Instance.SwitchScreen(9);
        }
    }


    public void ExportList()
    {
        var AM = AppManager.Instance;
        if (AM.noLogin == 0)
        {
            Sharing.ShareText(shareIngrList);
        }
    }

    public void Share()
    {
        var AM = AppManager.Instance;
        if (AM.noLogin == 0)
        {
            string s = "\"";
            s += AM.currentDish.dish_url;
            s += "\"";
            Sharing.ShareURL(s);
        }
    }

    public void BackBTN()
    {
        mainImage.texture = null;
        name.text = "";
        alt.text = "";
        kcal.text = "";
        serving.text = "";
    }
}
