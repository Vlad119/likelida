using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    public void NotNow()
    {
        AppManager.Instance.SwitchScreen(0);
    }

    public void GotIt()
    {
        var AM = AppManager.Instance;
        AM.SwitchScreen(3);
        AM.BottomBar.SetActive(true);
    }
}
