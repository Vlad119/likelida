using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FonShopsScript : MonoBehaviour
{
    public GameObject listMarker;
    public GameObject mapMarker;
    public GameObject listView;
    public GameObject whiteBack;
    public GameObject shopPref;
    public GameObject ParentShopPref;
    public GameObject location;
    public TMP_Text byList;
    public TMP_Text byMap;
    public OnlineMaps map;
    public float lat;
    public float lon;
    public GameObject MapPref;
    public GameObject parent;
    public Vector2 loc;


    public void OnEnable()
    {
        var AM = AppManager.Instance;
        AM.BottomBar.SetActive(true);
        //AM.BackInBlack();
        TakeGPScoord();
    }

    public void SelectMap()
    {
        mapMarker.SetActive(true);
        listMarker.SetActive(false);
        listView.SetActive(false);
        byMap.color = Color.black;
        byList.color = Color.white;
        whiteBack.SetActive(false);
        location.SetActive(true);
    }

    public void SelectList()
    {
        ParentShopPref.transform.ClearChildren();
        InstanceShopPref();
        mapMarker.SetActive(false);
        listMarker.SetActive(true);
        listView.SetActive(true);
        byMap.color = Color.white;
        byList.color = Color.black;
        whiteBack.SetActive(true);
        location.SetActive(false);
        MapPref.SetActive(false);
    }

    public void InstanceShopPref()
    {
        var company = AppManager.Instance.res.company;
        for (int i = 0; i < company.Count; i++)
        {
            var comp = Instantiate(shopPref, ParentShopPref.transform);
            comp.GetComponent<ShopPrefScript>().CreatePref(company[i]);
        }
    }

    public void InstanceMapPref()
    {
        var company = AppManager.Instance.res.company;
        for (int i = 0; i < company.Count; i++)
        {
            Instantiate(shopPref, ParentShopPref.transform);
        }
    }

    public void InstanceShopMarkers()
    {
        var company = AppManager.Instance.res.company;
        for (int i = 0; i < company.Count; i++)
        {
            loc.x = company[i].lon;
            loc.y = company[i].lat;
            OnlineMapsMarkerManager.CreateItem(loc, company[i].name, company[i].address, company[i].rating, company[i].distance);
        }
    }

    public void TakeGPScoord()
    {
        OnlineMapsMarkerManager.RemoveAllItems();
        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;
        if (lat != 0f && lon != 0f)
        {
            map.SetPositionAndZoom(lon, lat, 19);
            OnlineMapsMarkerManager.CreateItem(loc, "", "", 0, 0f);
        }
        else
        {
            map.SetPositionAndZoom(43.82, 55.47, 7);
            OnlineMapsMarkerManager.CreateItem(loc, "", "", 0, 0f);
        }
        GetData();
        InstanceShopMarkers();
    }

    public void Bottom()
    {
        var AM = AppManager.Instance;
        AM.BottomBar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    async public void GetData()
    {
        await WebHandler.Instance.GetShopsWrapper((repl) =>
        {
            JsonUtility.FromJsonOverwrite(repl, AppManager.Instance);
        }, "&lat=" + lat + "&lon=" + lon);
    }

    void Update()
    {
        if (!MapPref.activeSelf)
        {
            MapPref.SetActive(true);
        }
        var AM = AppManager.Instance;
        if (Input.GetMouseButtonDown(0))
        {
            if (map.GetMarkerFromScreen(Input.mousePosition) != null && !AM.mapPrefOnline)
            {
                AM.mapPrefOnline = true;
                AM.name = map.GetMarkerFromScreen(Input.mousePosition).name;
                AM.address = map.GetMarkerFromScreen(Input.mousePosition).address;
                AM.distance = Convert.ToSingle(map.GetMarkerFromScreen(Input.mousePosition).distance);
                AM.rating = map.GetMarkerFromScreen(Input.mousePosition).rating;
                Instantiate(MapPref, parent.transform);
            }
        }
    }
}

