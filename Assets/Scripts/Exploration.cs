using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exploration : MonoBehaviour
{
    //est-ce que explore est sur generator ou sur sous marin?
    //je vais avoir un sous marin controler?

    
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
        //Comment je cree le visuel de la map??
        //canvas worldposition?

    public int Longitude=0;//coox
    public int Latitude=0;//cooy
    public int DestinationLongitude=0;
    public int DestinationLatitude=0;

    public Vector2 myPosition;
    public Vector2 myDestination;

    public float T = 0;
    public float CountToNextIsland;

    MapGenerator MG;

    public Canvas Map;
    public Button MapSquare;

    public Text LongitudeText;
    public Text LatitudeText;
    public Text ProfondeurText;

    public List<Plateau> Plateaux = new List<Plateau>();
    public Dictionary<string, Plateau> CooToPlateau = new Dictionary<string, Plateau>();
    public Dictionary<string, Button> CootoMapSquare = new Dictionary<string, Button>();
	void Start ()
    {
        
        MG = FindObjectOfType<MapGenerator>();
        Moving();
	}
	
	void Update ()
    {
		
	}
    
    void MakeMapSquare(int coox,int cooy,int state)
    {
        
        if(CootoMapSquare.ContainsKey(MG.makeCoo(coox,cooy))==false)
        {
            Button Ms = Instantiate(MapSquare, new Vector3(coox, cooy, 0), Quaternion.identity, Map.transform);
            MapSquareInfo MSI = Ms.GetComponent<MapSquareInfo>();
            MSI.cooX = coox;
            MSI.cooY = cooy;
            MSI.coo = MG.makeCoo(coox, cooy);
            MSI.state = state;
            CootoMapSquare.Add(MG.makeCoo(coox, cooy), Ms);
        }
        
        
    }
    void MakeNeighborMapSquare(int coox,int cooy)
    {
        //cree de 1-4 voisins
        //check si un mapsquare existe deja aux coordonnes coox+1,cooy la list+dictionaire
        //si non: MakeMapsquare(newcoox,newcooy)
        int NewCoox;
        int NewCooy;

        NewCoox = coox + 1;
        NewCooy = cooy;
        if(CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox,NewCooy))==false)
        {
            print("t1");
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        NewCoox = coox - 1;
        NewCooy = cooy;
        if (CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox, NewCooy)) == false)
        {
            print("t1");
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        NewCoox = coox;
        NewCooy = cooy+1;
        if (CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox, NewCooy)) == false)
        {
            print("t1");
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        NewCoox = coox;
        NewCooy = cooy-1;
        if (CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox, NewCooy)) == false)
        {
            print("t1");
            MakeMapSquare(NewCoox, NewCooy,0);
        }
    }
   public void Moving()
   {

        
        Longitude = DestinationLongitude;
        Latitude = DestinationLatitude;
        LongitudeText.text = "Longitude: "+Longitude.ToString();
        LatitudeText.text = "Latitude: "+Latitude.ToString();
        MakeMapSquare(Longitude, Latitude,1);
        MakeNeighborMapSquare(Longitude, Latitude);
        exploring();
    }
    public void exploring()
    {
        MapSquareInfo InfoOfMySquare = CootoMapSquare[MG.makeCoo(Longitude, Latitude)].GetComponent<MapSquareInfo>();
        if (InfoOfMySquare.state==0)
        {
            InfoOfMySquare.state = 1;
            InfoOfMySquare.RollForPlateau();
            InfoOfMySquare.definemycolor();
        }
    }
    public void ShowHideMap()
    {
        Map.enabled=!Map.enabled;
    }
}
