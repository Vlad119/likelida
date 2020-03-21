using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace WebRequests
{
    public class Request : MonoBehaviour
    {
        private const string host = "https://likelida-byteteam.ru";
        private const string api = "/api/v1/";
        public static Request Instance;

        private void Start()
        {
            Instance = this;
        }

        private void Awake()
        {
            SingletonImplementation();
        }


        public static async Task RequestPost(string endpoint, string formData, UnityAction<int, string> _callback, bool token)
        {
            UnityWebRequest request = UnityWebRequest.Post(host + api + endpoint, formData);
            Debug.Log(formData);
            Debug.Log(request.url);
            UnityWebRequest.ClearCookieCache();
            if (token)
            {
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            }
            request.SetRequestHeader("Content-Type", "application/json");
            if (formData.Length > 0)
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(formData);
                var uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.uploadHandler = uploadHandler;
            }
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            await request.SendWebRequest();
            Debug.Log("HTTP CODE: " + request.responseCode);
            Debug.Log("SERVER RESPONDED: " + request.downloadHandler.text);
            if (request.responseCode == 200)
                _callback?.Invoke((int)request.responseCode, request.downloadHandler.text);
            else if (request.isNetworkError || request.isHttpError)
            {
                //ApplicationManager.instance.ErrorScreen.GetComponent<ErrorScreen>().error.text = "Отсутствует соединение с интернетом, проверьте подключение";
                //ApplicationManager.instance.ErrorScreen.SetActive(true);
            }
        }

        /// <summary>
        /// Sends get request
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="_callback"></param>
        /// <param name="token"></param>
        /// <param name="addetive"></param>
        /// <returns></returns>

        public static async Task RequestGet(string endpoint, UnityAction<int, string> _callback = null, bool token = true, string addetive = null)
        {
            string requestString = host + api + endpoint + addetive;
            Debug.Log(requestString);
            UnityWebRequest request = UnityWebRequest.Get(host + api + endpoint + addetive);
            UnityWebRequest.ClearCookieCache();
            if (token)
            {
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            }
            request.SetRequestHeader("Content-Type", "application/json");
            await request.SendWebRequest();
            Debug.Log("HTTP CODE: " + request.responseCode);
            Debug.Log("SERVER RESPONDED: " + request.downloadHandler.text);
            if (request.responseCode == 200)
                _callback?.Invoke((int)request.responseCode, request.downloadHandler.text);
            else if (request.isNetworkError || request.isHttpError)
            {
                //ApplicationManager.instance.ErrorScreen.GetComponent<ErrorScreen>().error.text = "Отсутствует соединение с интернетом, проверьте подключение";
                //ApplicationManager.instance.ErrorScreen.SetActive(true);
            }

        }

        public static async Task GetImage(string path, UnityAction<Texture2D> _callback)
        {
            UnityWebRequest getTexture = UnityWebRequestTexture.GetTexture(path);
            await getTexture.SendWebRequest();
            Texture2D response = DownloadHandlerTexture.GetContent(getTexture);
            if (getTexture.responseCode == 200)
            {
                _callback?.Invoke(response);
            }
        }

        public static async Task PostImage(string endpoint, byte[] data, UnityAction<int, string> _callback, bool token = true, string description = null)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormFileSection("file", data, "photo.jpg", "image/jpg"));
            if (description != null)
                form.Add(new MultipartFormDataSection("text", description));
            UnityWebRequest request = UnityWebRequest.Post(host + api + endpoint, form);
            if (token)
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            await request.SendWebRequest();
            //while (!request.isDone)
            //{
            //    Debug.Log(request.uploadProgress);
            //}
            Debug.Log(request.downloadHandler.text);
            Debug.Log(request.responseCode);
            _callback?.Invoke((int)request.responseCode, request.downloadHandler.text);
        }

        public static async Task PostImageFromPath(string endpoint, string filePath, UnityAction<int, string> _callback, bool token = true, string description = null)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormFileSection("file", System.IO.File.ReadAllBytes(filePath), "photo.jpg", "image/jpg"));
            Debug.Log(description.Length);
            //if (description != null || description.Length != 0)
            //    form.Add(new MultipartFormDataSection("text", description));
            UnityWebRequest request = UnityWebRequest.Post(host + api + endpoint, form);
            if (token)
                request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            request.SendWebRequest();
            while (!request.isDone)
            {
                Debug.Log(request.uploadProgress);
            }
            Debug.Log(request.downloadHandler.text);
            Debug.Log(request.responseCode);
            _callback?.Invoke((int)request.responseCode, request.downloadHandler.text);
        }

        public async static Task PostVideo(string endpoint, string videoPath, UnityAction<int, string> _callback, string description = null)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormFileSection("file", System.IO.File.ReadAllBytes(videoPath), "video.mp4", "video/mp4"));
            if (description != null)
                form.Add(new MultipartFormDataSection("text", description));
            UnityWebRequest request = UnityWebRequest.Post(host + api + endpoint, form);
            request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            _callback?.Invoke((int)request.responseCode, request.downloadHandler.text);
        }

        public async static Task PostAudio(string endpoint, string audioPath, UnityAction<int, string> _callback)
        {
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormFileSection("file", System.IO.File.ReadAllBytes(audioPath), "audio.wav", "audio/wav"));
            Debug.Log("File is" + System.IO.File.ReadAllBytes(audioPath).Length);
            UnityWebRequest request = UnityWebRequest.Post(host + api + endpoint, form);
            request.SetRequestHeader("Token", AppManager.Instance.userInfo.access_token);
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            _callback?.Invoke((int)request.responseCode, request.downloadHandler.text);
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
    }
}

