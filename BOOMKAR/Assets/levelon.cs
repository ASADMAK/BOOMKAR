using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelon : MonoBehaviour {

    int highestlevel;
    public GameObject[] levels;
	// Use this for initialization
	void Start () {
     highestlevel=   PlayerPrefs.GetInt("hightest", 1);
        levels[highestlevel - 1].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
