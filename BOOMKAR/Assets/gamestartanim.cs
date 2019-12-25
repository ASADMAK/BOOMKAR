using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestartanim : MonoBehaviour {

    public GameObject player,dropship;
    int highestlevel, currentlevel;
    public Animator anim;
    public Camera playercam;

	// Use this for initialization
	void Start () {

        currentlevel = SceneManager.GetActiveScene().buildIndex;
        if (currentlevel < PlayerPrefs.GetInt("levelreached", 2))
        {
            PlayerPrefs.SetInt("levelreached", currentlevel + 1);          
        }

        Invoke("activee", 2.5f);
        Invoke("delay", 3);
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
        anim.SetTrigger("gamestart");
    }
    public void cameraon()
    {
        playercam.depth = 2;
        dropship.SetActive(false);
    }

}
