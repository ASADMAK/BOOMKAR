using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoptoplayer : MonoBehaviour {

    public GameObject[] cars;
    int skin;

	// Use this for initialization
	void Start () {
        skin = PlayerPrefs.GetInt("Skin", 0);
        Debug.Log(skin);
        cars[skin].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
