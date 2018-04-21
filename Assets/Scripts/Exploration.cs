using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exploration : MonoBehaviour
{
    //est-ce que explore est sur generator ou sur sous marin?
    //je vais avoir un sous marin controler?

    // On compte un certain temps, ensuite, une ile est decouverte
    //Explore cree la map
    //skip cherche un autre plateau

    //Commande de deplacement du sous marin, vers ou on se deplace
    //comment on se deplace

        //ok j<ai le concept de Map
        //A chaque coordones que je vais, je cree un carre
        //un carre est une longitude et une latitude
        //Certain carre contiendront un plateau

        //ok... lets start with the map.
        //quand je me deplace, je cree un carre sur la map
        //le cree une coordones explorer sur la map
        //classe map? ou genre mapinfo
        //la map sera consitnue des coordonnes que j<ai explorer
        //la minimap montrera une portion de la map
        //quand on se deplace en mode sous-marin, on se deplace d<un point dans la map vers un autre point dans la map et on explore au fur et a mesure
        //la map est creer au depart et on l<explore? ou elle se cree au fur et a mesure?
        //systeme chaud froid scanneur pour chercher les plateaux

    public int Longitude;//coox
    public int Latitude;//cooy

    public float T = 0;
    public float CountToNextIsland;

    MapGenerator MG;

    public Text LongitudeText;
    public Text LatitudeText;
    public Text ProfondeurText;

    public List<Plateau> Plateaux = new List<Plateau>();
    public Dictionary<string, Plateau> CooToPlateau = new Dictionary<string, Plateau>();
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
