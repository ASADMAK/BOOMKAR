using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiosource : MonoBehaviour {

    public AudioSource[] audiosources;
    public AudioSource backgroundmusic;

    float volume;
    // Use this for initialization

    // Update is called once per frame
    void Update () {

            volume = PlayerPrefs.GetFloat("sound", 1);
            backgroundmusic.volume = PlayerPrefs.GetFloat("music", .4f);
            for (int i = 0; i < audiosources.Length; i++)
            {
                audiosources[i].volume = volume;
            }
 
    }

}
