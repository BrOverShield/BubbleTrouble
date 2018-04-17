using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounder : MonoBehaviour {
   Color myColor;
   public Color color1;
   public Color color2;
    bool onetotwo;
    bool twotoone = true;
    float speed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Color myColor = GetComponent<Camera>().backgroundColor;
        GetComponent<Camera>().backgroundColor = Color.Lerp(color1, color2, speed);
        if (speed <= 0)
        {
            onetotwo = true;
            twotoone = false;
        }
        if (speed >= 1)
        {
            onetotwo = false;
            twotoone = true;
        }
        if (onetotwo)
        {
            speed += 0.005f;
        }
        if (twotoone)
        {
            speed -= 0.005f;
        }
    }
}
