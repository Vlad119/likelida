using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FoodPrefScript : MonoBehaviour
{
    public RawImage img;
    public TMP_Text name;
    public GameObject close;
    public int index;
    public Dish dish;

    public void OpenReceipt()
    {
        var DPP = AppManager.Instance.dishesPlayerPrefs;
        var AM = AppManager.Instance;
        //Debug.Log("open");
        AM.currentDish = dish;
        DPP.viewedDishes.Add(dish);
        if (DPP.viewedDishes.Count>6)
        {
            DPP.viewedDishes.Remove(DPP.viewedDishes[0]);
        }
        AM.SwitchScreen(4);
        string s = JsonUtility.ToJson(DPP);
        PlayerPrefs.SetString("dish", s);
    }


    public async void ScrollCellContent(Dish _dish)
    {
        dish = _dish;
        name.text = _dish.name;
        _dish.showed = true;
        try
        {
            img.texture = await LoadImg(_dish.img[0].url);
        }
        catch
        { }
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
}
