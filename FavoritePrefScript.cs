using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FavoritePrefScript : MonoBehaviour
{
    public RawImage img;
    public TMP_Text name;
    public GameObject close;
    public int index;
    public Dish dish;
    public GameObject pref;

    public void OpenReceipt()
    {
        var AM = AppManager.Instance;
        AM.currentDish = dish;
        AM.SwitchScreen(4);
    }


    public async void ScrollCellContent(Dish _dish)
    {
        dish = _dish;
        name.text = _dish.name;
        img.texture = await LoadImg(dish.img[0].url);
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

    public async void Dislike()
    {
        var AM = AppManager.Instance;
        var DPP = AM.dishesPlayerPrefs;
        dish.like = false;
        for (int i = 0; i < DPP.likedDishes.Count; i++)
        {
            if (DPP.likedDishes[i].id == dish.id)
            {
                DPP.likedDishes.Remove(DPP.likedDishes[i]);
            }
        }
        await new WaitForSeconds(.5f);
        if (!dish.like)
        {
            string s = JsonUtility.ToJson(DPP);
            PlayerPrefs.SetString("dish", s);
            PlayerPrefs.Save();
        }
        Destroy(pref);
        AM.screens[6].GetComponent<FonFavoriteScript>().OnEnable();
    }
}
