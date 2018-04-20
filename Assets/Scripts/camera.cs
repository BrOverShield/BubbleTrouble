using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera : MonoBehaviour {

	public GameObject player;
    public Slider RotationSlider;

	public Vector3 offset;
    public Color color1;
    public Color color2;
    public float speed = 0f;
    bool onetotwo;
    bool twotoone;
    bool islock=true;
    public bool isSubmarine=true;
	// Use this for initialization
	void Start ()
	{
        GetComponent<Camera>().backgroundColor = color1;
        if (player!=null&&isSubmarine)
        {
            offset = this.transform.position - player.transform.position;
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
            speed += 0.001f;
        }
        if (twotoone)
        {
            speed -= 0.001f;
        }
    }
    // Update is called once per frame
    void LateUpdate ()
	{
        if (player != null&&islock)
        {
            transform.position = player.transform.position + offset;
        }
        if(!islock)
        {
            float Xrotation = RotationSlider.value;
            offset = this.transform.position - player.transform.position;
            this.transform.rotation = Quaternion.Euler(Xrotation, 0f, 0f);
        }
        
	}
    public void LockUnlock()
    {
        islock = !islock;
    }
    public void MoveTowardPlayer()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,player.transform.position,0.1f);
    }
}
