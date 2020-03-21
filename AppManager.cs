using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [HideInInspector] public static AppManager Instance;
    public int noLogin = 0;
    public GameObject[] screens;
    public int activeScreenIndex;
    public List<int> prevScreenIndex;
    public DishesPlayerPrefs dishesPlayerPrefs = new DishesPlayerPrefs();
    public string my_push_token;
    public Texture2D user_maptex;
    public GameObject BottomBar;
    public GameObject fridge;
    public GameObject recipes;
    public GameObject favorite;
    public GameObject fridgeON;
    public GameObject recipesON;
    public GameObject favoriteON;
    public GameObject user;
    public GameObject userON;
    public UserInfo userInfo = new UserInfo();
    public Reviews review = new Reviews();
    public bool popular = false;
    public bool select = false;
    public bool filter = false;
    public bool changedMinutes = false;
    public bool changedDifficult = false;
    public bool changedCalories = false;
    public bool mapPrefOnline = false;
    public int way;
    public string hard;
    public int kcall = 1;
    public int minutes;
    public Res res = new Res();
    public Dish currentDish = new Dish();
    public string searching;
    public List<Nid> products = new List<Nid>();
    public GameObject fonLoad;
    public GameObject fonMainLoad;
    public GameObject fonLogin;
    public string name;
    public string address;
    public float distance;
    public int rating;
    public string path;
    public Texture2D texture;
    public FBLogin fbLogin;
    public GameObject popup;
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton();
            }
        }
    }

    public void FBLoginWrapper()
    {
        StartCoroutine(fbLogin.Login());
    }

    private void Start()
    {
        Input.location.Start();
        Random();
        for (int i = 1; i < 12; i++)
        {
            Instance.screens[i].SetActive(false);
        }
        string s = PlayerPrefs.GetString("dish");
        JsonUtility.FromJsonOverwrite(s, dishesPlayerPrefs);
    }


    public void Random()
    {
        System.Random rnd = new System.Random();
        int r = rnd.Next(0, 12);
    }



    private void OnEnable()
    {
        try
        {
            var bt = File.ReadAllBytes(path);
            texture.LoadImage(bt);
        }
        catch { }
        Lean.Localization.LeanLocalization.UpdateTranslations();
    }

    public void ReloadScreen()
    {
        screens[activeScreenIndex].SetActive(false);
        screens[activeScreenIndex].SetActive(true);
    }

    private void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    //   app = Firebase.FirebaseApp.DefaultInstance;
                    Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                    Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
                    Firebase.Messaging.FirebaseMessaging.Subscribe("/topics/active_users");
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
    }



    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        my_push_token = token.Token;
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }


    public virtual void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        //Debug.Log("Received a new message");
        var notification = e.Message.Notification;
        if (notification != null)
        {
            Debug.Log("title: " + notification.Title);
            Debug.Log("body: " + notification.Body);
        }
        if (e.Message.From.Length > 0)
            //Debug.Log("from: " + e.Message.From);
        if (e.Message.Link != null)
        {
            //Debug.Log("link: " + e.Message.Link.ToString());
        }
        if (e.Message.Data.Count > 0)
        {
            //Debug.Log("data:");
            foreach (System.Collections.Generic.KeyValuePair<string, string> iter in
                e.Message.Data)
            {
                //Debug.Log("  " + iter.Key + ": " + iter.Value);
            }
        }
    }


    public void SwitchScreen(int nextScreenIndex)
    {
        if (activeScreenIndex < nextScreenIndex)
        {
            //play left to right animation
        }
        else if (activeScreenIndex < nextScreenIndex)
        {
            //play right to left animation
        }
        else
        {
            //do nothing
        }
        //yield return transferAnimationLength;
        if (nextScreenIndex == -1)
        {
            activeScreenIndex = prevScreenIndex[prevScreenIndex.Count - 1];
            prevScreenIndex.RemoveAt(prevScreenIndex.Count - 1);
        }
        else
        {
            prevScreenIndex.Add(activeScreenIndex);
            activeScreenIndex = nextScreenIndex;
        }
        for (int i = 0; i < screens.Length; i++)
        {
            if (i != activeScreenIndex)
                screens[i].SetActive(false);
        }
        screens[activeScreenIndex].SetActive(true);
    }


    public void SaveUserInfo(UserInfo userInfo)
    {
        var toSave = JsonUtility.ToJson(userInfo);
        PlayerPrefs.SetString("userInfo", toSave);
    }

    public void LoadUser()
    {
        var toUnravel = PlayerPrefs.GetString("userInfo");
        userInfo = JsonUtility.FromJson<UserInfo>(toUnravel);
    }

    public void BackButton()
    {
        SwitchScreen(-1);
    }

    #region bottomBar view
    public void Default()
    {
        fridge.SetActive(true);
        fridgeON.SetActive(false);
        recipes.SetActive(true);
        recipesON.SetActive(false);
        favorite.SetActive(true);
        favoriteON.SetActive(false);
        user.SetActive(true);
        userON.SetActive(false);
    }



    public void White()
    {
        fridge.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        recipes.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        favorite.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        user.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void Fridge()
    {
        Default();
        Instance.SwitchScreen(7);
        fridge.SetActive(false);
        fridgeON.SetActive(true);
        BottomBar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void Recipes()
    {
        Default();
        Instance.SwitchScreen(3);
        recipes.SetActive(false);
        recipesON.SetActive(true);
        BottomBar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void Favorite()
    {
        Default();
        SwitchScreen(6);
        favorite.SetActive(false);
        favoriteON.SetActive(true);
        BottomBar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void User()
    {
        Default();
        SwitchScreen(16);
        user.SetActive(false);
        userON.SetActive(true);
        BottomBar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
    #endregion
}

[Serializable]
public class DishesPlayerPrefs
{
    public List<Dish> likedDishes = new List<Dish>();
    public List<Dish> viewedDishes = new List<Dish>();
    public List<Reviews> userReviews = new List<Reviews>();
}
