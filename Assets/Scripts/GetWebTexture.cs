using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class GetWebTexture : MonoBehaviour
{
    DateTime TodayNow;

    private void Start()
    {
        StartCoroutine(GetRequest("https://4.bp.blogspot.com/-4xxTe_qeV1E/Vd7FkNUlwjI/AAAAAAAAxFc/8u9MNKtg7gg/s800/syachiku.png"));
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))

        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Texture myTexture = DownloadHandlerTexture.GetContent(webRequest);
                yield return myTexture;
            }
        }
    }
}