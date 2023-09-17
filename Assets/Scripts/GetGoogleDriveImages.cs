using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityGoogleDrive;

public class GetGoogleDriveImages : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;　 //経過時間
    private const string folderId = "1kuN14vh4dfLBFFqew22rCdBZ1vRc2mou";

    private void Start()
    {
        _repeatSpan = 5;    //実行間隔を５に設定
        _timeElapsed = 0;   //経過時間をリセット
    }

    void  Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= _repeatSpan)
        {

            StartCoroutine(GetDriveImages());
            _timeElapsed = 0;   //経過時間をリセットする
        }
        
    }

    IEnumerator GetDriveImages()
    {
        var reqfiles = GoogleDriveFiles.List();
        reqfiles.Fields = new List<string> { "files(id, name, size, mimeType, createdTime)" };
        reqfiles.Q = $"\'{folderId}\' in parents and trashed = false";

        string[] persistFiles = Directory.GetFiles(
                        Application.persistentDataPath, "*.png", SearchOption.AllDirectories
                    ).OrderBy(f => File.GetLastWriteTime(f).Date).ToArray();

        Debug.Log($"{persistFiles.Length}");

        yield return reqfiles.Send();

        Debug.Log("teat");

        reqfiles.Send().OnDone +=
            (filelist) =>
            {
                foreach (var file in filelist.Files)
                {
                    var path = Path.Combine(Application.persistentDataPath, file.Name);
                    var DLrequest = GoogleDriveFiles.Download(fileId: file.Id);
                    DLrequest.Send().OnDone += (DLFile) =>
                    {
                        byte[] bytes = DLFile.Content;
                        File.WriteAllBytes(path, bytes);
                    };
                }
            };
        
    }
}
