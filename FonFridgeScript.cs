using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonFridgeScript : MonoBehaviour
{

    public GameObject firstTime;
    public GameObject needReg;
    public GameObject rus;
    public GameObject wantCook;
    public GameObject list;
    public GameObject fridge;
    public GameObject ListList;
    public GameObject FridgeList;
    public TMP_Text byList;
    public TMP_Text byFridge;
    public GameObject fridgeIngPref;
    public GameObject ParentFridgeIngPref;
    public GameObject polkaPref;
    public GameObject polkaParentPref;
    public GameObject listIngPref;
    public GameObject ParentListIngPref;
    public GameObject space;
    public GameObject ParentSpace;
    public Button cookBTN;
    public List<Nid> _prod;
    public int count;
    public bool b = true;
    public string fTime;

    public void OnEnable()
    {
        var AM = AppManager.Instance;
        _prod = AM.products;
        fTime = "1";
        if (PlayerPrefs.GetString("firstTime") != "")
        {
            fTime = PlayerPrefs.GetString("firstTime");
        }
        CookBTN();
        AM.BottomBar.SetActive(true);
        AM.fridgeON.SetActive(true);
        AM.fridge.SetActive(false);
        AM.White();
        ParentFridgeIngPref.transform.ClearChildren();
        ParentSpace.transform.ClearChildren();
        InstanceParentPolkaPref();
        if (b)
        {
            FridgeView();
        }
        else
        {
            ListView();
        }
        Space();
    }



    public void Space3()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(space, ParentSpace.transform);
        }
    }

    public void FridgeView()
    {
        for (int i = 0; i < _prod.Count; i++)
        {
            var prod = Instantiate(fridgeIngPref, ParentFridgeIngPref.transform);
            prod.GetComponent<FridgeIngrPrefScript>().CreatePref(_prod[i]);
        }
    }


    public void ListView()
    {
        ParentListIngPref.transform.ClearChildren();
        ParentSpace.transform.ClearChildren();
        for (int i = 0; i < _prod.Count; i++)
        {
            var prod = Instantiate(listIngPref, ParentListIngPref.transform);
            prod.GetComponent<FridgeIngrPrefScript>().CreatePref(_prod[i]);
        }
    }


    public void InstanceParentPolkaPref()
    {
        var polka = Instantiate(polkaParentPref, ParentFridgeIngPref.transform);
    }

    public void Space()
    {
        if (_prod.Count == 0 && b)
        {
            Space3();
        }

        if (_prod.Count <= 9 && b)
        {
            switch (_prod.Count)
            {
                case 1:
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Instantiate(space, ParentSpace.transform);
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Instantiate(space, ParentSpace.transform);
                        }
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            Instantiate(space, ParentSpace.transform);
                        }
                        break;
                    }
                case 4:
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Instantiate(space, ParentSpace.transform);
                        }
                        break;
                    }
                case 5:
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Instantiate(space, ParentSpace.transform);
                        }
                        break;
                    }
                case 6:
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Instantiate(space, ParentSpace.transform);
                        }
                        break;
                    }
            }
        }
        else
        {

        }
    }

    public void CloseWantCook()
    {
        wantCook.SetActive(false);
    }

    public void CloseFirstTime()
    {
        fTime = "0";
        PlayerPrefs.SetString("firstTime", fTime);
        firstTime.SetActive(false);
    }

    public void Fridge()
    {
        ParentFridgeIngPref.transform.ClearChildren();
        fridge.SetActive(true);
        list.SetActive(false);
        ListList.SetActive(false);
        FridgeList.SetActive(true);
        byFridge.color = Color.black;
        byList.color = Color.white;
        InstanceParentPolkaPref();
        b = true;
        Space();
        FridgeView();
    }

    public void List()
    {
        list.SetActive(true);
        fridge.SetActive(false);
        FridgeList.SetActive(false);
        ListList.SetActive(true);
        byList.color = Color.black;
        byFridge.color = Color.white;
        b = false;
        ParentListIngPref.transform.ClearChildren();
        ParentSpace.transform.ClearChildren();
        ListView();
    }

    public void Add()
    {
        AppManager.Instance.SwitchScreen(5);
    }

    public void Cook()
    {
        AppManager.Instance.way = 4;
        AppManager.Instance.SwitchScreen(17);
    }

    public void CookBTN()
    {
        var AM = AppManager.Instance;
        needReg.SetActive(false);
        if (AM.noLogin==0)
        {
            if (_prod.Count == 0)
            {
                cookBTN.GetComponent<Image>().raycastTarget = false;
            }
            else
            {
                cookBTN.GetComponent<Image>().raycastTarget = true;
            }

            if (fTime == "1")
            {
                firstTime.SetActive(true);
                if (Lean.Localization.LeanLocalization.CurrentLanguage == "Russian")
                {
                    rus.SetActive(true);
                }
                else
                {
                    rus.SetActive(false);
                }
            }
        }
        else
        {
            needReg.SetActive(true);
        }
    }

    public void WantCook()
    {
        if (cookBTN.GetComponent<Image>().raycastTarget == false)
        {
            wantCook.SetActive(true);
        }
    }

    public void MoveToReg()
    {
        AppManager.Instance.SwitchScreen(0);
    }
}
