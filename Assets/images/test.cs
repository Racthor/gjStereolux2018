using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
//using UnityEditor;

public class test : MonoBehaviour
{
    //    public Texture2D texture
    // Use this for initialization
    Texture2D myTexture;
    string url = "http://192.168.25.101:8080/?image=result.png";

    void Start()
    {
        StartCoroutine(GetScreenShoot());
    }
    
    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    IEnumerator GetScreenShoot()
    {
        Debug.Log(Application.persistentDataPath);
        while (true) {
            //load .png data from server 
            WWW www = new WWW(url);
            yield return www;
            myTexture = www.texture;
            
                    //myTexture = Resources.Load<Texture2D> ("a")as Texture2D;//omit extension

            File.WriteAllBytes(Application.persistentDataPath + "/ab.png", myTexture.EncodeToPNG());
            Destroy(myTexture);
            myTexture = LoadPNG(Application.persistentDataPath + "/ab.png");
            //create texture from it
            make_sprite_atruntime();
            www.Dispose();
            yield return new WaitForSeconds(0.035f);
        }
    }

    IEnumerator Collect()
    {
        while (true)
        {
            GC.Collect();
            yield return new WaitForSeconds(2);
        }
    }

        void make_sprite_atruntime()
        {
        // now  myTexture  can be used any way you like ,you can compress it re-size it anything 
        Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f), 100);
        var sr = GetComponent<SpriteRenderer>();
        if (sr.sprite != null)
        {
            Destroy(sr.sprite);
        }
        sr.sprite = sprite;
        var poly = GetComponent<PolygonCollider2D>();
        if (poly)
        {
            Destroy(poly);
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }


}