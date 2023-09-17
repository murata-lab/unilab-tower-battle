using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class GetWebImage : MonoBehaviour
{
    DateTime TodayNow;

    private void Start()
    {
        StartCoroutine(GetRequest("https://4.bp.blogspot.com/-4xxTe_qeV1E/Vd7FkNUlwjI/AAAAAAAAxFc/8u9MNKtg7gg/s800/syachiku.png"));
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                var fileName = "picture_" +  ".png";
                var path = Path.Combine(Application.persistentDataPath, fileName);
                byte[] bytes = webRequest.downloadHandler.data;
                File.WriteAllBytes(path, bytes);
            }
        }
    }
}