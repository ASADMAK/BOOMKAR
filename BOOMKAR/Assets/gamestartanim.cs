using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestartanim : MonoBehaviour {

    public GameObject player;
    public Camera dropcam;
    public GameObject gamecanvas;
    public GameObject dropcamera;

	
	// Update is called once per frame
	void Update () {
		
	}

    public void activee()
    {
        player.SetActive(true);
        dropcam.depth = 0;
    }

    public void gamecanvason()
    {
        gamecanvas.SetActive(true);
    }
    public void animationcomplete()
    {
        dropcamera.SetActive(false);
    }
    public void playeroff()
    {
        FindObjectOfType<complete>().playeroff();
    }
    public void animationcompleted()
    {
        FindObjectOfType<complete>().completeanim();
    }

}
