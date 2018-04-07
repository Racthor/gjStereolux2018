using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
//using UnityEditor;

public class test : MonoBehaviour
{
    //    public Texture2D texture
    // Use this for initialization
    Texture2D myTexture;
    string url = "http://192.168.43.71:8080/?image=ombre.png";

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

            //        myTexture = Resources.Load<Texture2D> ("a")as Texture2D;;//omit extension

            File.WriteAllBytes(Application.persistentDataPath + "/ab.png", myTexture.EncodeToPNG());
            myTexture = LoadPNG(Application.persistentDataPath + "/ab.png");
            //create texture from it
            make_sprite_atruntime();
            yield return new WaitForSeconds(2);
        }
    }

    void make_sprite_atruntime()
    {
        // now  myTexture  can be used any way you like ,you can compress it re-size it anything 
        Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f), 100);
        GetComponent<SpriteRenderer>().sprite = sprite;
        var poly = GetComponent<PolygonCollider2D>();
        if (poly)
        {
            Destroy(poly);
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }


}