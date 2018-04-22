using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exploration : MonoBehaviour
{
   

    
    
    

    
        

    public int Longitude;//coox
    public int Latitude;//cooy
    public int DestinationLongitude=0;
    public int DestinationLatitude=0;
    public string coo;
    
    

    public Vector2 myPosition;
    public Vector2 myDestination;

    public GameObject SubmarineMode;
    public GameObject ExplorerMode;
    public GameObject LoadingScreen;

    MapGenerator MG;

    Plateau myPlateau;

    public Canvas Map;

    public Image PlayerPosition;

    public Button MapSquare;
    public Button ExploreButton;

    public Text LongitudeText;
    public Text LatitudeText;
    public Text ProfondeurText;
    public Text InfoIleText;
    public Text ActionText;

    public List<Plateau> Plateaux = new List<Plateau>();
    public List<MapSquareInfo> Voisins = new List<MapSquareInfo>();
    public List<MapSquareInfo> AnciensVoisins = new List<MapSquareInfo>();
    public Dictionary<string, Plateau> CooToPlateau = new Dictionary<string,Plateau>();
    public Dictionary<string, Button> CootoMapSquare = new Dictionary<string, Button>();
	void Start ()
    {
        
        MG = FindObjectOfType<MapGenerator>();
        MapSquare.GetComponent<MapSquareInfo>().CanLegalyMoveTo = true;
        Moving();
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
    public void ShowPlateau()
    {
        if (CooToPlateau.ContainsKey(MG.makeCoo(Longitude, Latitude)))
        {

            myPlateau = CooToPlateau[MG.makeCoo(Longitude, Latitude)];
        }
        InfoIleText.text = "";

        if (myPlateau != null)
        {
            
            InfoIleText.text = "Info: " + "\n" + "coordones: " + myPlateau.Coo + "\n" + "Taille: " + myPlateau.Size + "\n" + "Type: " + myPlateau.TypeName + "\n" + "Montagnes: " + "\n" + "Ressources: " + myPlateau.ChestProb + "\n" + "Danger: " + myPlateau.Danger;
            
        }
        
    }
    IEnumerator loadingscreenGotoExplorer()
    {
        SubmarineMode.SetActive(false);
        LoadingScreen.SetActive(true);
        if(MG.IsDiscoverd==false)
        {
            GameObject Dogtsm = Instantiate(MG.DestroyOnGotoSubMarinePrefab);
            MG.DestroyOnGotoSubMarine = Dogtsm;
        }
              
        yield return new WaitForSeconds(1f);
        MG.OnClicStart();
        LoadingScreen.SetActive(false);
        ExplorerMode.SetActive(true);
        

    }
    public void GotoExplorer()
    {
        StartCoroutine(loadingscreenGotoExplorer());
    }
    public void GotoSubmarine()
    {
        SubmarineMode.SetActive(true);
        ExplorerMode.SetActive(false);
        MG.DestroyOnGotoSubMarine.SetActive(false);
        Destroy(MG.P);
        Camera.main.transform.position = new Vector3(0.08f, 1.39f, -2.5f);
        Camera.main.transform.rotation = Quaternion.Euler(30, 0, 0);
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
         
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        Voisins.Add(CootoMapSquare[MG.makeCoo(NewCoox, NewCooy)].GetComponent<MapSquareInfo>());    
        NewCoox = coox - 1;
        NewCooy = cooy;
        if (CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox, NewCooy)) == false)
        {
   
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        Voisins.Add(CootoMapSquare[MG.makeCoo(NewCoox, NewCooy)].GetComponent<MapSquareInfo>());
        NewCoox = coox;
        NewCooy = cooy+1;
        if (CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox, NewCooy)) == false)
        {
        
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        Voisins.Add(CootoMapSquare[MG.makeCoo(NewCoox, NewCooy)].GetComponent<MapSquareInfo>());
        NewCoox = coox;
        NewCooy = cooy-1;
        if (CootoMapSquare.ContainsKey(MG.makeCoo(NewCoox, NewCooy)) == false)
        {
           
            MakeMapSquare(NewCoox, NewCooy,0);
        }
        Voisins.Add(CootoMapSquare[MG.makeCoo(NewCoox, NewCooy)].GetComponent<MapSquareInfo>());
        
    }
    void Update()
    {
        
        
        
    }
    MapSquareInfo InfoOfMySquare;
    MapSquareInfo LastPosition;
    public void Moving()
   {
            if(InfoOfMySquare!=null)
            {
             LastPosition = InfoOfMySquare;
             LastPosition.DefineMyposition();
            }
         foreach(MapSquareInfo ov in AnciensVoisins)
        {
            ov.CanLegalyMoveTo = false;
            //AnciensVoisins.Remove(ov);
           
        }
        AnciensVoisins.Clear();
            Longitude = DestinationLongitude;
            Latitude = DestinationLatitude;
            coo = MG.makeCoo(Longitude, Latitude);
            LongitudeText.text = "Longitude: " + Longitude.ToString();
            LatitudeText.text = "Latitude: " + Latitude.ToString();
            MakeMapSquare(Longitude, Latitude, 1);
            MakeNeighborMapSquare(Longitude, Latitude);
            exploring();
        
        foreach (MapSquareInfo msi in Voisins)
        {
            
            msi.CanLegalyMoveTo = true;
            AnciensVoisins.Add(msi);
            //Voisins.Remove(msi);
            
        }
        Voisins.Clear();
        
    }
    public void exploring()
    {
        InfoOfMySquare = CootoMapSquare[MG.makeCoo(Longitude, Latitude)].GetComponent<MapSquareInfo>();
        if (InfoOfMySquare.state==0)
        {
            InfoOfMySquare.state = 1;
            InfoOfMySquare.RollForPlateau();
            InfoOfMySquare.definemycolor();
            InfoOfMySquare.DefineMyposition();
            //InfoOfMySquare.ShowPlateau();
        }
        
    }
    
    public void ShowHideMap()
    {
        Map.enabled=!Map.enabled;
        PlayerPosition.enabled = !PlayerPosition.enabled;
    }
}
