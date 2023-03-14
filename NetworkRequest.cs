using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace UnityTest.Network
{
    public class NetworkRequest : MonoBehaviour
    {
        // --- FUNGSI SEND REQUEST "POST" ---
        public static async Task<(string data, UnityWebRequest.Result request)> PostRequest(string uri, WWWForm form = null, bool consoleDebug = false, string requestHeaderName = null, string requestHeaderValue = null)
        {
            Debug.Log(uri);
            UnityWebRequest www = UnityWebRequest.Post(uri, form);
            www.SetRequestHeader(requestHeaderName != null ? requestHeaderName : "Accept", requestHeaderValue != null ? requestHeaderValue : "application/json");

            var progress = www.SendWebRequest();
            while (!progress.isDone)
            {
                await Task.Yield();
            }

            switch (www.result)
            {
                case UnityWebRequest.Result.Success:
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if (consoleDebug) Debug.Log(result);

                    return (result, www.result);
                default:
                    if (consoleDebug) Debug.Log(www.error);
                    return (null, www.result);
            }
        }

        public static async Task<(string data, UnityWebRequest.Result request)> GetRequest(string uri, bool consoleDebug = false, string requestHeaderName = null, string requestHeaderValue = null)
        {
            UnityWebRequest www = UnityWebRequest.Get(uri);
            www.SetRequestHeader(requestHeaderName != null ? requestHeaderName : "Accept", requestHeaderValue != null ? requestHeaderValue : "application/json");

            var progress = www.SendWebRequest();
            while (!progress.isDone)
            {
                await Task.Yield();
            }

            switch (www.result)
            {
                case UnityWebRequest.Result.Success:
                    string result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if (consoleDebug) Debug.Log(result);

                    return (result, www.result);
                default:
                    if (consoleDebug) Debug.Log(www.error);
                    return (null, www.result);
            }
        }

        public static async Task<Texture> GetRemoteTexture(string url)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                // begin request:
                var asyncOp = www.SendWebRequest();

                // await until it's done: 
                while (asyncOp.isDone == false)
                    await Task.Delay(1000 / 30);//30 hertz

                // read results:
                if (www.result == UnityWebRequest.Result.Success)
                {
                    return DownloadHandlerTexture.GetContent(www);
                }
                else
                {
                    // log error:
#if DEBUG
                    Debug.Log($"{www.error}, URL:{www.url}");
#endif

                    // nothing to return on error:
                    return null;
                }
            }
        }
        // --- END OF FUNGSI SEND REQUEST "POST" ---
    }
}