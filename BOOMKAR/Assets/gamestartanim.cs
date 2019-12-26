using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestartanim : MonoBehaviour {

    public GameObject PlayerCamera;
    public GameObject player,dropship,dropcam,levelcam;
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

        Invoke("cameraon", 3.0f);
        Invoke("activee", 2.0f);
        Invoke("delay", 6);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activee()
    {
        player.SetActive(true);
        dropcam.SetActive(false);
    }
    public void delay()
    {
        anim.SetTrigger("gamestart");
    }
    public void cameraon()
    {
        PlayerCamera.SetActive(true);
        dropship.SetActive(false);
        levelcam.SetActive(false);
    }

}
