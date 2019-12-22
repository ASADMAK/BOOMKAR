using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestartanim : MonoBehaviour {

    public GameObject player,cameraa,dropship;
    int highestlevel, currentlevel;

	// Use this for initialization
	void Start () {

        currentlevel = SceneManager.GetActiveScene().buildIndex;
        if (currentlevel < PlayerPrefs.GetInt("levelreached", 2))
        {
            PlayerPrefs.SetInt("levelreached", currentlevel + 1);          
        }

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
        //GameObject.Find("Camera").GetComponent<VehicleCameraControl>().enabled = true;
        dropship.SetActive(false);
    }

}
