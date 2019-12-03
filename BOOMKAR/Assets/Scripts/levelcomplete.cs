using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelcomplete : MonoBehaviour {

    bool level;


	// Use this for initialization
	void Start () {
        level = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (level == true)
        {
            if (other.tag == "player")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void complete()
    {
        level = true;
    }
}
