using UnityEngine;
using UnityEditor;
using UnityGoogleDrive;

public static class GoogleDriveDownloader
{
    [MenuItem("Tools/DownloadFromGoogleDrive")]
    private static void DownloadFromGoogleDrive()
    {
        Debug.Log(nameof(DownloadFromGoogleDrive));
    }
}