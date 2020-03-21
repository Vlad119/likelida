using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class WebHandler : MonoBehaviour
{
    public static WebHandler Instance;
    public delegate UnityWebRequest RequestCall();

    public string servAddress;
    public string loginEndpoint;

    public string registerEndpoint;
    public string dataEndpoint;

    public string reviewEndpoint;
    public string updateUser;

    public DownloadHandler postResponse;
    public DownloadHandler getResponse;

    private void Awake()
    {
        SingletonImplementation();
    }

    public async Task<UnityWebRequest> IRequestSend(RequestCall data, bool showLoading = true)
    {
        UnityWebRequest request;
        do
        {
            request = data();
            if (showLoading)

            await request.SendWebRequest();

            if (request.error != null)
            {
                print(request.error);
                print(request.downloadHandler.text);
                if (request.error != null)
                {
                    if (request.isHttpError)
                    {
                        if (request.downloadHandler.text.Contains("wrong phone or code"))
                        {
                            //TODO BAD IMPLEMENTATION FOR SPECIAL CASE
                            break;
                        }
                        else if (request.responseCode == 403)
                        {
                            AppManager.Instance.userInfo = new UserInfo();
                            AppManager.Instance.userInfo.user = new User();
                            PlayerPrefs.DeleteAll();
                            AppManager.Instance.SwitchScreen(0);
                            break;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        
                    }
                }
            }
        }
        while (request.error != null);
        //Debug.Log(request.downloadHandler.text);
        return request;
    }

    private async Task PostJsonAsync(string url, string dataString = null, UnityAction<string> DoIfSuccess = null, bool addTokenHeader = false)
    {
        var endUrl = servAddress + url;
        var req = await IRequestSend(() =>
        {
            var request = new UnityWebRequest(endUrl, "POST");
            if (dataString != null)
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(dataString);
                var uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.uploadHandler = uploadHandler;
            }
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            if (!addTokenHeader)
                request.SetRequestHeader("content-type", "application/json");
            else
            {
                request.SetRequestHeader("content-type", "application/json");
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token != null ? AppManager.Instance.userInfo.access_token : "0");
            }
            //print(request.GetRequestHeader("content-type") + "   " + (addTokenHeader ? request.GetRequestHeader("token") : "") + "    " + request.url + "    " + (request.uploadHandler != null?Encoding.UTF8.GetString(request.uploadHandler.data):""));
            return request;
        });

        //Debug.Log("All OK");
        //Debug.Log("Status Code: " + req.responseCode);
        DoIfSuccess?.Invoke(req.downloadHandler.text);

    }

    #region Requests
    private IEnumerator PostJson(string url, string dataString, UnityAction<string> DoIfSuccess = null, bool addTokenHeader = false)
    {
        var endUrl = servAddress + url;
        var request = new UnityWebRequest(endUrl, "POST");
        byte[] bodyRaw = Encoding.ASCII.GetBytes(dataString);
        var uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.uploadHandler = uploadHandler;
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        if (!addTokenHeader)
            request.SetRequestHeader("content-type", "application/json");
        else
        {
            request.SetRequestHeader("content-type", "application/json");
            request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
        }
        yield return request.SendWebRequest();
        print(request.GetRequestHeader("content-type") + "   " + (addTokenHeader ? request.GetRequestHeader("token") : "") + "    " + request.url + "    " + Encoding.ASCII.GetString(request.uploadHandler.data));

        if (request.error != null)
        {
            Debug.Log("Erro: " + request.error + "\n" + request.downloadHandler.text);

        }
        else
        {
            Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);
            DoIfSuccess?.Invoke(request.downloadHandler.text);
            postResponse = request.downloadHandler;
        }
        bodyRaw = null;
    }

    private IEnumerator GetRequest(string url, UnityAction<string> DoIfSuccess = null, bool addToken = false, string getParameters = null)
    {
        var endUrl = servAddress + url + (getParameters == null? "": getParameters);
        var request = new UnityWebRequest(endUrl, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        if (addToken)
            request.SetRequestHeader("token", AppManager.Instance.userInfo.access_token);

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogWarning("Error " +request.responseCode+ " "  + request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Request sent");
            Debug.Log("Status code: " + request.responseCode);
            DoIfSuccess?.Invoke(request.downloadHandler.text);
        }
        print(endUrl + "    " + AppManager.Instance.userInfo.access_token + "   " + request.downloadHandler.text);
    }

    private IEnumerator LoadImage(string url, UnityAction<Texture2D> DoIfSuccess)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Loading image");
                var tex = DownloadHandlerTexture.GetContent(request);
                tex.Apply();
                DoIfSuccess(tex);
            }
        }
    }
    #endregion

   
   
    public void LoginWrapper(UnityAction<string> afterFinish, string data)
    {
        StartCoroutine(PostJson(loginEndpoint, data, afterFinish));
    }


    public void LoadImageWrapper(string imageUrl, UnityAction<Texture2D> afterfinish)
    {
        StartCoroutine(LoadImage(imageUrl, afterfinish));
    }

    public async Task GetDataWrapper(UnityAction<string> afterfinish)
    {
        await GetRequest(dataEndpoint, afterfinish);
    }

    public async Task GetShopsWrapper(UnityAction<string> afterfinish, string coords)
    {
        await GetRequest(dataEndpoint, afterfinish,true, coords);
    }

    public async Task SendReviewWrapper(UnityAction<string> afterfinish, string review)
    {
        await GetRequest(reviewEndpoint, afterfinish,true,review);
    }

    private void SingletonImplementation()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        else if (Instance != this)
            Destroy(this);
    }

    public void RegisterWraper(UnityAction<string> afterFinish)
    {
        var userJSON = JsonUtility.ToJson(AppManager.Instance.userInfo.user);
        PostJson(registerEndpoint, userJSON, afterFinish);
    }

    public void LogWraper(UnityAction<string> afterFinish)
    {
        var userJSON = JsonUtility.ToJson(AppManager.Instance.userInfo.user);
        PostJson(loginEndpoint, userJSON, afterFinish);
    }

    public void UpdateUserWrapper(UnityAction<string> afterFinish, string data)
    {
         PostJson(updateUser, data, afterFinish, true);
    }

}
