using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuelspawner : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<carcontroller>().refill_Fuel();
            FindObjectOfType<fuel>().startspawning();
            Destroy(gameObject);
        }
    }
}
