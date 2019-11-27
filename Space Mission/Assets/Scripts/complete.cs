using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete : MonoBehaviour {

    bool levelcomplete;
    public GameObject missioncomplete,game;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (levelcomplete == true)
        {
            if (other.tag == "player")
            {
                missioncomplete.SetActive(true);
                game.SetActive(false);
                Time.timeScale = 0;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void done()
    {
        levelcomplete = true;
    }
}
