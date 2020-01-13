using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floppy : MonoBehaviour {

    public GameObject indicator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<playercon>().floppygot();
            Destroy(indicator);
            Destroy(gameObject);
        }
    }
}
