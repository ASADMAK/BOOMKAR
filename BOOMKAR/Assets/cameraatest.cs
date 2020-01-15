using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraatest : MonoBehaviour
{
    public GameObject[] cam;
    int n = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onit()
    {
        for (int i = 0; i < cam.Length; i++)
        {
            cam[i].SetActive(false);
        }
        cam[n].SetActive(true);
        n++;
        if(n>8)
        {
            n = 0;
        }
    }
}
