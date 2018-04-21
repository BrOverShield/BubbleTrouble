using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exploration : MonoBehaviour
{
   

    
    
    

    //Lerp de deplacement
    //la minimap montrera une portion de la map
    //systeme chaud froid scanneur pour chercher les plateaux
        

    public int Longitude;//coox
    public int Latitude;//cooy
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
    public Text InfoIleText;

    public List<Plateau> Plateaux = new List<Plateau>();
    public Dictionary<string, Plateau> CooToPlateau = new Dictionary<string,Plateau>();
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
