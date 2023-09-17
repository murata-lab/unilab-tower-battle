using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityGoogleDrive;

public class GetGoogleDriveImages : MonoBehaviour
{
    private const string folderId = "1kuN14vh4dfLBFFqew22rCdBZ1vRc2mou";

    void Start()
    {
        StartCoroutine(GetDriveImages());
    }

    IEnumerator GetDriveImages()
    {
        var reqfiles = GoogleDriveFiles.List();
        reqfiles.Fields = new List<string> { "files(id, name, size, mimeType, createdTime)" };
        reqfiles.Q = $"\'{folderId}\' in parents and trashed = false";

        yield return reqfiles.Send();

        reqfiles.Send().OnDone +=
            (filelist) =>
            {
                foreach (var file in filelist.Files)
                {
                    Debug.Log($"{file.Id}");
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
