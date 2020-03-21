using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FoodPref2Script : MonoBehaviour
{
    public RawImage img;
    public TMP_Text name;
    public int index;
    public Dish dish;
    public void OpenReceipt()
    {
        Debug.Log("open");
        AppManager.Instance.currentDish = dish;
        AppManager.Instance.SwitchScreen(4);
    }


    private async void ScrollCellContent(object _dish)
    {
        dish = (Dish)_dish;
        name.text = dish.name;
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

}
