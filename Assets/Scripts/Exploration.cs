using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exploration : MonoBehaviour
{
    // On compte un certain temps, ensuite, une ile est decouverte
    //cette ile possede des coordonnes et des stats (classe plateau?)
    //cette ile est place dans une liste et un dictionaire
    //Explore cree la map
    //skip cherche un autre plateau

    //Commande de deplacement du sous marin, vers ou on se deplace
    public float T = 0;
    public float CountToNextIsland;

    MapGenerator MG;

    public Text LongitudeText;
    public Text LatitudeText;
    public Text ProfondeurText;

    
	void Start ()
    {
        MG = FindObjectOfType<MapGenerator>();
	}
	
	

	void Update ()
    {
		
	}

    void Count()
    {
        T += Time.deltaTime;
        if(T>=CountToNextIsland)
        {
            CreatePlateau();
        }
    }
    void CreatePlateau()
    {

    }
}
