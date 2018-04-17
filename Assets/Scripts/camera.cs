using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	public GameObject player;

	public Vector3 offset;
    public Color color1;
    public Color color2;
    public float speed = 0f;
    bool onetotwo;
    bool twotoone;
	// Use this for initialization
	void Start ()
	{
        GetComponent<Camera>().backgroundColor = color1;
        if (player!=null)
        {
           // offset = new Vector3(2, 7, 0) - player.transform.position;
        }
		
	}
    private void Update()
    {
        
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
    // Update is called once per frame
    void LateUpdate ()
	{
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
        
	}
}
