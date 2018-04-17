using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThuileInfo : MonoBehaviour {

    public float multiplicator = 1;
    public int cooX=0;
    public int cooY=0;
    public string coo;
    public float Hauteur=0f;
    public bool HasChest;
    public bool HasTrap;
    public GameObject ChestPrefab;
    public GameObject pickupPrefab;
    public bool hasPickup;
	void Start ()
    {
        
        updateme();
	}
	
	
	void Update ()
    {
		
	}
    void updateme()
    {
        coo = cooX.ToString() + "," + cooY.ToString();
        this.transform.position = new Vector3(cooX, 0, cooY);
        this.transform.localScale = new Vector3(1,Hauteur,1);
        if(HasChest)
        {
            GameObject Chest = Instantiate(ChestPrefab, new Vector3(cooX, Hauteur*multiplicator, cooY),Quaternion.identity);
            
        }
        if(hasPickup)
        {
            GameObject pickup = Instantiate(pickupPrefab, new Vector3(cooX, Hauteur*multiplicator, cooY),Quaternion.identity);
        }
    }
    public void square()
    {
        multiplicator = 1;
    }
    public void Lanscpae()
    {
        multiplicator = 0.25f;
    }
}
