using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acid : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<carcontroller>().acidin();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<carcontroller>().acidout();
        }
    }
}
