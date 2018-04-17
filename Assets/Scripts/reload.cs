using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reload : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void reloading()
    {
        Application.LoadLevel("FelixLVL1");
        
    }
    public void win()
    {
        Application.LoadLevel("WinScene");
    }
}
