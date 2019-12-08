using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiosource : MonoBehaviour {

    public AudioSource[] audiosources;
    float volume;
	// Use this for initialization
	void Start () {
        volume = PlayerPrefs.GetFloat("sound", 1);
        for (int i = 0; i < audiosources.Length; i++)
        {
            audiosources[i].volume = volume;
        }  
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
