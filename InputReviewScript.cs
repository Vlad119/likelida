using TMPro;
using UnityEngine;

public class InputReviewScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text repeat_text;
    public TMP_InputField input_text;
    public void CreateText()
    {
        if (repeat_text.text.Length < 420)
        {
            repeat_text.text = input_text.text;
        }
    }
}
