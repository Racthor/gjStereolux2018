using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading;

public class Goal : MonoBehaviour
{
    public AudioClip clip;
    public string spawnerid;
    private AudioSource sound_player;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //joue un son

        sound_player = GetComponent<AudioSource>();

        sound_player.PlayOneShot(clip);

        Thread.Sleep(2000);

        //scène suivante
        GameObject player = GameObject.Find("personnage");

        //récupération du spawner
        GameObject spawner = GameObject.Find(spawnerid);
        player.transform.position = spawner.transform.position;

    }
}