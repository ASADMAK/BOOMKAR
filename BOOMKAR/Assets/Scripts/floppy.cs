using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floppy : MonoBehaviour {

    public GameObject indicator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Destroy(gameObject);
            Destroy(indicator);

        }
    }
}
