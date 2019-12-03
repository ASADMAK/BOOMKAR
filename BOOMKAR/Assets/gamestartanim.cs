using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamestartanim : MonoBehaviour {

    public GameObject player,cameraa,dropship;

	// Use this for initialization
	void Start () {

        Invoke("activee", 2.5f);
        Invoke("delay", 4);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activee()
    {
        player.SetActive(true);
    }
    public void delay()
    {
        GameObject.Find("Camera").GetComponent<VehicleCameraControl>().enabled = true;
        dropship.SetActive(false);
    }

}
