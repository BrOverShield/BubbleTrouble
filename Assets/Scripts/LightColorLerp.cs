using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorLerp : MonoBehaviour {

    Color myColor;
    Color color1;
    Color color2;
    bool onetotwo;
    bool twotoone=true;
    
    public Material Mat1;
    public Material Mat2;
    public float speed;
	void Start ()
    {
        myColor = GetComponent<Light>().color;
        color1 = Mat1.color;
        color2 = Mat2.color;
        myColor = Mat1.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Light>().color = Color.Lerp(color1,color2,speed);
        if(speed<=0)
        {
            onetotwo = true;
            twotoone = false;
        }
        if (speed >= 1)
        {
            onetotwo = false;
            twotoone = true;
        }
        if(onetotwo)
        {
            speed += 0.001f;
        }
        if(twotoone)
        {
            speed -= 0.001f;
        }
    }
}
