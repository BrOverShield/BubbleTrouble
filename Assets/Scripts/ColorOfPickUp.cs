using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOfPickUp : MonoBehaviour {
    public int mycolorint;
    public Color PresetRed;
    public Color PresetBlue;
    public Color PresetGreen;
    public Color mycolor;

	// Use this for initialization
	void Start ()
    {
        mycolorint = Random.Range(1, 4);
        if(mycolorint==1)
        {
            mycolor = PresetRed;
        }
        if(mycolorint==2)
        {
            mycolor = PresetBlue;
        }
        if(mycolorint==3)
        {
            mycolor = PresetGreen;
        }
        GetComponentInChildren<MeshRenderer>().material.color = mycolor;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
