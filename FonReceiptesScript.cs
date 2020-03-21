using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FonReceiptesScript : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public TMP_Text mainDishName;
    public TMP_Text mainDishKcal;
    public TMP_Text mainDishTime;
    public TMP_Text mainDishServing;
    public RawImage mainImage;
    public GameObject dishesPref;
    public GameObject ParentDishesPref;
    public GameObject space;
    public int count = 0;
    public bool newest = true;
    public int r;
    public RawImage img;


    public void OnEnable()
    {
        var AM = AppManager.Instance;
        ClearAllCells();
        GetData();
        AM.White();
        AM.recipes.SetActive(false);
        AM.recipesON.SetActive(true);
        AM.BottomBar.SetActive(true);
        AM.Recipes();
        AM.fonLoad.SetActive(true);
    }


    private void Start()
    {
        newest = true;
        var count = AppManager.Instance.res.dish.Count;
        System.Random rnd = new System.Random();
        r = rnd.Next(0, count);

    }

    public async void LoadMainDish(Dish dish)
    {
        try
        {
            mainDishName.text = dish.name;
            mainDishKcal.text = dish.kcal;
            mainDishTime.text = dish.time;
            mainDishServing.text = dish.serving;
            mainImage.texture = await LoadMainDishImg(dish.img[0].url);
        }
        catch { }
    }

    async Task<Texture> LoadMainDishImg(string endpoint)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(endpoint))
        {
            await request.SendWebRequest();
            var texture = DownloadHandlerTexture.GetContent(request);
            return texture;
        }
    }


    public void InstanceDishesPref()
    {
        Debug.Log("InstanceDishesPref");
        var AM = AppManager.Instance;
        var cats = AM.res.cats;
        foreach (var cat in cats)
        {
            if (newest && cat.newCount > 0)
            {
                var dish = Instantiate(dishesPref, ParentDishesPref.transform);
                dish.GetComponent<DishesScript>().CreatePref2(cat, newest);
            }
            if (!newest && cat.popCount > 0)
            {
                var dish = Instantiate(dishesPref, ParentDishesPref.transform);
                dish.GetComponent<DishesScript>().CreatePref2(cat, newest);
            }
        }
        Instantiate(space, ParentDishesPref.transform);
    }


    public void SelectedDishesPref()
    {
        Debug.Log("SelectedDishesPref");
        var cats = AppManager.Instance.res.cats;
        foreach (var cat in cats)
        {
            if (newest && cat.newCount > 0 && cat.selected)
            {
                var dish = Instantiate(dishesPref, ParentDishesPref.transform);
                dish.GetComponent<DishesScript>().CreatePref(cat, newest);
            }
            if (!newest && cat.popCount > 0 && cat.selected)
            {
                var dish = Instantiate(dishesPref, ParentDishesPref.transform);
                dish.GetComponent<DishesScript>().CreatePref(cat, newest);
            }
        }
        Instantiate(space, ParentDishesPref.transform);
    }

    public void FilteredDishesPref()
    {
        //Debug.Log("FilteredDishesPref");
        p2.SetActive(false);
        p1.SetActive(false);
        var cats = AppManager.Instance.res.cats;
        foreach (var cat in cats)
        {
            if (newest && cat.newCount > 0)
            {
                var dish = Instantiate(dishesPref, ParentDishesPref.transform);
                dish.GetComponent<DishesScript>().CreatePref(cat, newest);
            }
            if (!newest && cat.popCount > 0)
            {
                var dish = Instantiate(dishesPref, ParentDishesPref.transform);
                dish.GetComponent<DishesScript>().CreatePref(cat, newest);
            }
        }
        Instantiate(space, ParentDishesPref.transform);
    }


    public void Filter()
    {
        AppManager.Instance.screens[8].SetActive(true);
    }


    public void Day()
    {
        var DPP = AppManager.Instance.dishesPlayerPrefs;
        var AM = AppManager.Instance;
        var count = AM.res.dish.Count;
        if (count > 0)
        {
            AM.currentDish = AM.res.dish[r];
            DPP.viewedDishes.Add(AM.currentDish);
            if (DPP.viewedDishes.Count > 6)
            {
                DPP.viewedDishes.Remove(DPP.viewedDishes[0]);
            }
            AM.SwitchScreen(4);
        }
    }


    public void NewDish()
    {
        p1.SetActive(true);
        p2.SetActive(false);
        newest = true;
        Default();
    }


    public void Pop()
    {
        p2.SetActive(true);
        p1.SetActive(false);
        newest = false;
        Default();
    }

    public void Default()
    {
        var AM = AppManager.Instance;
        AM.changedCalories = false;
        AM.changedDifficult = false;
        AM.changedMinutes = false;
        AM.filter = false;
        ClearAllCells();
        InstanceDishesPref();
    }

    public void ClearAllCells()
    {
        ParentDishesPref.transform.ClearChildren();
    }


    async public void GetData()
    {
        var AM = AppManager.Instance;
        var dish = AM.res.dish;//блюда
        var liked = AM.dishesPlayerPrefs.likedDishes;//любимые блюда
        await WebHandler.Instance.GetDataWrapper((repl) =>
        {
            JsonUtility.FromJsonOverwrite(repl, AM);
        }
        );
        LoadMainDish(dish[0]); //загрузка блюда дня
        await new WaitForSeconds(1);
        if (AM.select)
        {
            SelectedDishesPref();
        }
        else
        {
            NewDish();// по умолчанию
        }
        //подстановка лайков у блюд
        foreach (var d in dish)
        {
            foreach (var l in liked)
            {
                if (d.id == l.id)
                {
                    d.like = l.like;
                }
            }
        }
    }

    public void OpenCategories()
    {
        AppManager.Instance.SwitchScreen(10);
    }
}
