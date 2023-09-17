using System.Collections;

public class GoogleDriveTest : UnityEngine.MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        var data = new UnityGoogleDrive.Data.File
        { Name = "Test.txt", Content = System.Text.Encoding.UTF8.GetBytes("Hello Google Drive ") };
        var req = UnityGoogleDrive.GoogleDriveFiles.Create(data);
        print("Start Test Upload");
        yield return req.Send();
        print("Finish Test Upload");
    }
}
