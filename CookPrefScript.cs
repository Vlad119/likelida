using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CookPrefScript : MonoBehaviour
{
    public RawImage img;
    public TMP_Text alt;
    public TMP_Text number;


    public async void CreatePref(Cooking cooking, int index)
    { 
        img.texture = await LoadImg(cooking.url);
        alt.text = cooking.alt;
        number.text = Convert.ToString(index+1);
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


