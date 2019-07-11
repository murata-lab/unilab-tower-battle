using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;


public class CreateManager : MonoBehaviour
{
//    private GameObject obj;
    public List<GameObject> people;//どうぶつ取得配列
    public bool isFall;
    int file_length;
    // Start is called before the first frame update
    void Init()
    {
        Animal.isMoves.Clear();//移動してる動物のリストを初期化
        string[] files = Directory.GetFiles(
            @"Assets/Resources", "*.png", SearchOption.AllDirectories
            ).ToArray();
        file_length = files.Length;
 //       obj = null;
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
//        if (obj && obj.transform.position.y < -5)
//        {
//        }
        if (CheckGameOver(people)){
          SceneManager.LoadScene ("GameOver");
        }
        if (CheckMove(Animal.isMoves))
        {
            return;//移動中なら処理はここまで
        }
        string[] files = Directory.GetFiles(
            @"Assets/Resources", "*.png", SearchOption.AllDirectories
            ).OrderBy(f => File.GetLastWriteTime(f).Date
            ).ToArray();
        if (files.Length == 0){
            return;
        }
        if (files.Length > file_length)
        {
            string tar = files[files.Length - 1].Remove(0, 17);
            tar = tar.Replace(".png", "");
            Sprite img = Resources.Load(tar, typeof(Sprite)) as Sprite;
//            Debug.Log(img);
            if (img == null){
                return;
            }
            Create(img);
            file_length += 1;
        }
    }

    void Create(Sprite img)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = img;
        // print(obj.GetComponent<SpriteRenderer>().sprite);
        obj.AddComponent<PolygonCollider2D>();
        obj.AddComponent<Rigidbody2D>();
        //obj.GetComponent<Rigidbody2D>().isKinematic = true;
        obj.AddComponent<Animal>();
        obj.transform.position = new Vector3(0.0f, 6.0f, 0.0f);
        people.Add(obj);
    }

    /// <summary>
    /// 移動中かチェック
    /// </summary>
    /// <param name="isMoves"></param>
    /// <returns></returns>
    bool CheckMove(List<Moving> isMoves)
    {
        if (isMoves == null)
        {
            return false;
        }
        foreach (Moving b in isMoves)
        {
            if (b.isMove)
            {
                //Debug.Log("移動中(*'ω'*)");
                return true;
            }
        }
        return false;
    }

    bool CheckGameOver(List<GameObject> people)
    {
        foreach (GameObject b in people)
        {
            if (b.transform.position.y < -5)
            {
                return true;
            }
        }
        return false;
    }
}
