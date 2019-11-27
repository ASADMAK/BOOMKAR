using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class panel : MonoBehaviour {

    public TextMeshProUGUI coin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        coin.text = PlayerPrefs.GetInt("gold", 0).ToString();
	}
}
