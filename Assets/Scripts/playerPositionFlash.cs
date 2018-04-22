using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPositionFlash : MonoBehaviour
{
    public Color startColor;
    public Color endCoor;
    public Material mymat;
    public float speed;
    bool startToend = true;
    bool endtostart = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(startToend)
        {
            speed -= 0.5f * Time.deltaTime;
        }
        if (endtostart)
        {
            speed += 0.5f * Time.deltaTime;
        }
        if(speed>=1)
        {
            endtostart = false;
            startToend = true;
        }
        if (speed <= 0)
        {
            endtostart = true;
            startToend = false;
        }
        mymat.color=Color.Lerp(startColor,endCoor,speed);
        
	}
}
