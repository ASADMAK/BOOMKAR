﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starcollect : MonoBehaviour {

    public GameObject indicator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "player")
        {
          
            FindObjectOfType<carcontroller>().playerstar();
            Destroy(gameObject);
            Destroy(indicator);
        }
    }
}
