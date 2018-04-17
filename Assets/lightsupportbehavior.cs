using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsupportbehavior : MonoBehaviour {
    float speed;
    bool onetotwo;
    bool twotoone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Light>().intensity = speed;
        if (speed <= 0.001f)
        {
            onetotwo = true;
            twotoone = false;
        }
        if (speed >= 0.05f)
        {
            onetotwo = false;
            twotoone = true;
        }
        if (onetotwo)
        {
            speed += 0.001f;
        }
        if (twotoone)
        {
            speed -= 0.001f;
        }
    }
}
