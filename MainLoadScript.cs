using UnityEngine;

public class MainLoadScript : MonoBehaviour
{
    public float timeRemaining = 3f;
    void Update()
    {
        var AM = AppManager.Instance;
        var info = AppManager.Instance.userInfo;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                AM.fonMainLoad.SetActive(false);
                AM.fonLogin.SetActive(true);
            }
        }
    }
}
