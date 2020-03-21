using UnityEngine;

public class FonLoadScript : MonoBehaviour
{
    public float timeRemaining = 2f;

    private void OnEnable()
    {
        var AM = AppManager.Instance;
        timeRemaining = 2f;
        AM.BottomBar.SetActive(false);
    }
    void Update()
    {
        var AM = AppManager.Instance;
        var info = AppManager.Instance.userInfo;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0 && info.access_token != "")
            {
                AM.fonLoad.SetActive(false);
                AM.BottomBar.SetActive(true);
            }
            if (timeRemaining < 0 && AM.noLogin == 1)
            {
                AM.fonLoad.SetActive(false);
                AM.BottomBar.SetActive(true);
            }
        }
    }
}
