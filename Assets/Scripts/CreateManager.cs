using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;


public class CreateManager : MonoBehaviour
{
    int file_length;
    // Start is called before the first frame update
    void Start()
    {
        string[] files = Directory.GetFiles(
            @"Assets/Resources", "*.png", SearchOption.AllDirectories
            ).OrderBy(f => File.GetLastWriteTime(f).Date)
            .ToArray();
        //Array.Sort(files, CompareLastWriteTime);
        
        file_length = files.Length;
        for (int i = 0; i < files.Length; i++)
        {
            Create(files[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        string[] files = Directory.GetFiles(
            @"Assets/Resources", "*.png", SearchOption.AllDirectories
            ).OrderBy(f => File.GetLastWriteTime(f).Date)
            .ToArray();
        if (files.Length > file_length)
        {
            Create(files[files.Length - 1]);
            file_length += 1;
        }
    }

    void Create(string file)
    {
        //画像をspriteとして保存
        string tar = file.Remove(0, 17);
        tar = tar.Replace(".png", "");
        Sprite img = Resources.Load<Sprite>(tar);
        //オブジェクト生成
        GameObject obj = new GameObject();
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = img;
        obj.AddComponent<PolygonCollider2D>();
        //obj.GetComponent<SpriteRenderer>().sprite = img;
        obj.AddComponent<Rigidbody2D>();
        //obj.GetComponent<SpriteRenderer>().sprite = img;
        obj.AddComponent<Animal>();
        //obj.GetComponent<SpriteRenderer>().sprite = img;
        obj.transform.position = new Vector3(0.0f, 6.0f, 0.0f);
    }
}