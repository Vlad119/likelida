using TMPro;
using UnityEngine;

public class IngrPrefScript : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text count;

    public async void CreatePref (Ingredients ingredients)
    {
        
        {
            name.text = ingredients.name;
        }
    }
}
