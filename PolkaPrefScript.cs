using System.Collections.Generic;
using UnityEngine;

public class PolkaPrefScript : MonoBehaviour
{
    public GameObject polkaPref;
    public GameObject polkaParentPref;
    public List<Nid> _prod;
    int div;
    int mod;
    private void OnEnable()
    {
        var AM = AppManager.Instance;
        _prod = AM.products;
        polkaParentPref.transform.ClearChildren();
        CreatePref();
    }


    public void CreatePref()
    {
        div = Mathf.RoundToInt(_prod.Count / 3);
        Debug.Log(div);
        mod = Mathf.RoundToInt(_prod.Count % 3);
        Debug.Log(mod);
        if (_prod.Count<=9)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(polkaPref, polkaParentPref.transform);
            }
        }
        else
        {
            if (div != 0 || mod != 0)
            {
                for (int i = 0; i < div + 1; i++)
                {
                    Instantiate(polkaPref, polkaParentPref.transform);
                }
            }
        }
    }
}
