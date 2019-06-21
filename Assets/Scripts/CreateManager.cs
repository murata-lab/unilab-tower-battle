using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateManager : MonoBehaviour
{
    int file_length;
    // Start is called before the first frame update
    void Start()
    {
        string[] files = System.IO.Directory.GetFiles(
            @"Assets", "*.png", System.IO.SearchOption.AllDirectories);
        file_length = files.Length;
        for (int i = 0; i < files.Length; i++)
        {
            Create(files[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string[] files = System.IO.Directory.GetFiles(
            @"Assets", "*.png", System.IO.SearchOption.AllDirectories);
        if (files.Length > file_length)
        {
            Create(files[files.Length - 1]);
            file_length += 1;
        }
    }

    void Create(string file)
    {
        string tar = file.Remove(0, 17);
        tar = tar.Replace(".png", "");
        //string tar = files[i].Substring(17, len);
        // ResourcesフォルダからCubeプレハブのオブジェクトを取得
        Sprite img = Resources.Load<Sprite>(tar);
        // プレハブを元にオブジェクトを生成する
        // Instantiate (obj, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
        GameObject obj = new GameObject();
        GameObject instance = (GameObject)Instantiate(obj,
                                                  new Vector3(0.0f, 6.0f, 0.0f),
                                                  Quaternion.identity);
        instance.AddComponent<SpriteRenderer>();
        instance.AddComponent<Rigidbody2D>();
        instance.AddComponent<PolygonCollider2D>();
        instance.AddComponent<Animal>();
        instance.GetComponent<SpriteRenderer>().sprite = img;
    }
}