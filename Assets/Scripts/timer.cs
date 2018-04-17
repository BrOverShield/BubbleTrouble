using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    bool m1 = true;
	public float time = 60;
	public Text timerText;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(m1)
        {
            time -= Time.deltaTime;
            updateTimer();
            if (time <= 0)
            {
                SceneManager.LoadScene("FelixLVL1");
            }
        }
		
	}

	void updateTimer ()
	{
		timerText.text ="Time Left : "+ time.ToString ();
	}
    void starttimer()
    {
        m1 = true;
    }
}
