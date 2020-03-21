using TMPro;
using UnityEngine;

public class FonFindScript : MonoBehaviour
{
    public TMP_InputField value;
    public GameObject findPref;
    public GameObject ParentFindPref;

    public void OnEnable()
    {
        AppManager.Instance.BottomBar.SetActive(false);
        AppManager.Instance.searching = "";
        ClearAllCells();
        InstanceFindPref();
    }


    public void ReturnBottom()
    {
        AppManager.Instance.BottomBar.SetActive(true);
    }

    public void InstanceFindPref()
    {
        var res = AppManager.Instance.res;
        for (int i = 0; i < res.cats.Count; i++)
        {
            var dish = Instantiate(findPref, ParentFindPref.transform);
            dish.GetComponent<FindPrefScript>().CreatePref(res.cats[i].name, res.cats[i]);
        }
    }
    public void ClearAllCells()
    {
        ParentFindPref.transform.ClearChildren();
    }

    public void Search()
    {
        var AM = AppManager.Instance;
        if (value.text != "")
        {
            AM.searching = value.text;
        }
        AM.SwitchScreen(17);
    }
}
