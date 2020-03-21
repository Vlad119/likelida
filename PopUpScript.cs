using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public float timeRemaining = 5f;

    private void OnEnable()
    {
        var AM = AppManager.Instance;
        timeRemaining = 5f;
        AM.BottomBar.SetActive(false);
    }

    void Update()
    {
        var AM = AppManager.Instance;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0) 
            {
               AM.popup.SetActive(false);
            }
        }
    }
}
