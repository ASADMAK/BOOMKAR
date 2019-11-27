using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floppy : MonoBehaviour {

    public GameObject indicator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Debug.Log("floopy collected");
            FindObjectOfType<carcontroller>().floppycollect();
            Destroy(gameObject);
            Destroy(indicator);

        }
    }
}
