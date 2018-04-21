using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSquareInfo : MonoBehaviour
{
    public int cooX;
    public int cooY;
    public int state;
    public int probforplateau=10;

    public string coo;
 
    public bool CurrentPosition = false;
    public bool isExplored = false;
    public bool HasPlateau = false;

    public Plateau myPlateau;

    public Canvas Map;

    
    public Material ExploredMat;
    public Material UnexploredMat;
    public Material HasPlateauMat;

    Exploration E;
    MapGenerator GM;
	void Start ()
    {
        GM = FindObjectOfType<MapGenerator>();
        E = FindObjectOfType<Exploration>();
        if(E.CooToPlateau.ContainsKey(coo))
        {
            myPlateau = E.CooToPlateau[coo];
        }
       

        this.transform.localPosition = new Vector3(cooX*51,cooY*51,0);
        this.GetComponentInChildren<Text>().text = GM.makeCoo(cooX, cooY);
        RollForPlateau();
        definemycolor();
	}
	public void movingToHere()
    {
        E.DestinationLongitude = cooX;
        E.DestinationLatitude = cooY;

    }
    
	public void definemycolor()
    {
        if(state==0)
        {
            this.GetComponent<Image>().material = UnexploredMat;
        }
        if (state==1)
        {
            this.GetComponent<Image>().material = ExploredMat;
        }
        if (state==2)
        {
            this.GetComponent<Image>().material = HasPlateauMat;
        }
    }
    public void RollForPlateau()
    {
        if(state==1)
        {
            int roll = Random.Range(0, 100);
            if(roll<=probforplateau)
            {
                state = 2;
                
            }

        }
    }
	void Update ()
    {
		
	}
}
