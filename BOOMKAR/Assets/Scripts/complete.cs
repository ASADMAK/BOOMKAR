using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete : MonoBehaviour {

    bool levelcomplete;
    public GameObject missioncomplete,game;
    public AudioSource levelcompleted;
    public Animator ship;
    public GameObject cameraa,player;

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (levelcomplete == true)
        {
            if (other.tag == "player")
            {
   
                cameraa.SetActive(true);
                ship.SetBool("iscomplete", true);
                Invoke("completeanim", 5);
                Invoke("playeroff", 2.5f);
                FindObjectOfType<carcontroller>().playerstationary();
            }
        }
    }

    public void completeanim()
    {
        missioncomplete.SetActive(true);
        game.SetActive(false);
        levelcompleted.Play();
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void done()
    {
        levelcomplete = true;
    }
    public void playeroff()
    {
        player.SetActive(false);
    }
}
