using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Minimap
//TODO: Base building
//TODO: recolte de recources et systeme magasin

//TODO: Refaire le script de controle Camera
//TODO: Raycast Player
//TODO: Tunel,stalactites,gouffres,bords falaises,empirer le landscape



//TODO: reconstruction de map
//TODO: ajouter montagnes
    
    

public class MapGenerator : MonoBehaviour {
    public GameObject EnemyPrefab;
    public GameObject squareThuile;
    public GameObject mytimer;
    public GameObject thuilePrefab;
    public GameObject ChestPrefab;
    public GameObject Player;
    public GameObject PickupPrefab;
    public GameObject DestroyOnStartClic;
    public GameObject DestroyOnGotoSubMarine;
    public GameObject DestroyOnGotoSubMarinePrefab;

    public Text timertext;
    public Text counttext;
    public Text pickuptxt;

    public Light lighte;
    public Light lightsupport;

    public Slider sliderx;
    public Slider slidery;
    public Slider sliderDiff;
    public Slider MountainRadiusSlider;
    public Slider MountainHeightSlider;
    public Slider PlayerTailleSlider;

    public InputField MountainCooInputField;

    int MapId = 0;
    public int XSize = 10;
    public int YSize = 10;
    public int ProbOfChestatstart = 10;
    public int MountainRadius = 5;
    public int mountainHeight = 5;
    public float Dangermin;
    public float Dangermax;

    public string MountainCoo = "10,10";

    bool SoftLandscapebool = false;
    bool HardLandscapebool = true;
    bool realsquarebool = false;
    bool Stalagmitesbool = false;
    public bool IsDiscoverd = false;
    

    GameObject Thuile;
    public GameObject P;

    public Exploration exploration;

    Dictionary<string, ThuileInfo> CooToThuileInfo = new Dictionary<string, ThuileInfo>();

    List<ThuileInfo> ListeDeThuileInfo = new List<ThuileInfo>();
    

    void Start()
    {
        exploration = FindObjectOfType<Exploration>();
        //  GenerateMap(XSize,YSize,ProbOfChestatstart);
    }
    public void Landscapesoft()
    {
        SoftLandscapebool = true;
        HardLandscapebool = false;
        realsquarebool = false;
        Stalagmitesbool = false;
    }
    public void landscapHard()
    {
        SoftLandscapebool = false;
        HardLandscapebool = true;
        realsquarebool = false;
        Stalagmitesbool = false;
    }
    public void realquare()
    {
        SoftLandscapebool = false;
        HardLandscapebool = false;
        realsquarebool = true;
        Stalagmitesbool = false;
    }
    public void stalagmites()
    {
        SoftLandscapebool = false;
        HardLandscapebool = false;
        realsquarebool = false;
        Stalagmitesbool = true;
    }
    public int RandomMapType()
    {
        return Random.Range(1, 5);
    }
    public void DefineTypeFromInt(int type)
    {
        if(type==1)
        {
            Landscapesoft();
        }
        if(type==2)
        {
            landscapHard();
        }
        if(type==3)
        {
            realquare();
        }
        if(type==4)
        {
            stalagmites();
        }
    }
    public int RandomDanger()
    {
        return Random.Range(1, 6);
    }
    void Update()
    {
        /*//editorMode
        XSize = (int)sliderx.value;
        YSize = (int)slidery.value;
        ProbOfChestatstart = (int)sliderDiff.value;   
        MountainRadius = (int)MountainRadiusSlider.value;
        mountainHeight = (int)MountainHeightSlider.value;
        MountainCoo = MountainCooInputField.text;*/
        GeneratingEnemy(Dangermin, Dangermax);

    }
    public void OnClicStart()
    {
        //int Size = Random.Range(10, 200);
        int Size = 100;
        ProbOfChestatstart = Random.Range(1, 11);
        
        PlayerSet();
        CameraSet();
        Destroy(DestroyOnStartClic);
        //GameObject T = Instantiate(mytimer);
        //T.GetComponent<timer>().timerText = timertext;
        if(IsDiscoverd==false)
        {
            GenerateMap(Size, Size, ProbOfChestatstart, RandomMapType(), RandomDanger());
        }
        if(IsDiscoverd)
        {
            RegenerateMap(exploration.CooToPlateau[makeCoo(exploration.Longitude,exploration.Latitude)]);
        }

    }
    public void PlayerSet()
    {
        P = Instantiate(Player, new Vector3(2, 2, 2), Quaternion.identity);
        P.GetComponent<PlayerController>().countText = counttext;
        P.GetComponent<PlayerController>().countPickUp = pickuptxt;
    }
    public void CameraSet()
    {
        // Instantiate(lightsupport);
        Instantiate(lighte);
        Camera.main.GetComponent<camera>().player = P;
        Camera.main.GetComponent<camera>().isSubmarine = false;
        Camera.main.transform.position = new Vector3(2, 7, -1);
        Camera.main.GetComponent<camera>().offset = new Vector3(2, 7, -1) - P.transform.position;
    }
    public void EditorThings()
    {
        MakeMountainAt(CooToThuileInfo["10,10"], 5, 5);
        P.GetComponent<PlayerController>().TaillePlayerSlider = PlayerTailleSlider;
    }
    public void RegenerateMap(Plateau plateau)
    {

    }
    public Plateau NewPlateau;
    public void GenerateMap(int mapLongeur, int mapLargeur, int ProbOfChest,int Type,int Dangerzone)
    {
        MapId += 1;
        NewPlateau = new Plateau(exploration.Longitude,exploration.Latitude,mapLargeur,ProbOfChest,Type,Dangerzone);//cree le plateau
        exploration.Plateaux.Add(NewPlateau);//ajoute a la liste de plateaux de exploration
        exploration.CooToPlateau.Add(makeCoo(exploration.Longitude, exploration.Latitude), NewPlateau);//ajoute coordones au dictionaire coo to plateau

     
        DefineTypeFromInt(Type);
        for (int x = 0; x < mapLargeur; x++)
        {
            for (int y = 0; y < mapLongeur; y++)
            {
                if (SoftLandscapebool || HardLandscapebool||Stalagmitesbool)
                {
                    Thuile = Instantiate(thuilePrefab,DestroyOnGotoSubMarine.transform);
                }
                if (realsquarebool)
                {
                    Thuile = Instantiate(squareThuile,DestroyOnGotoSubMarine.transform);
                }

                ThuileInfo TI = Thuile.GetComponent<ThuileInfo>();
                TI.cooX = x;
                TI.cooY = y;
                if (SoftLandscapebool || realsquarebool)
                {
                    TI.Hauteur = Random.Range(0f, 1f);
                    TI.multiplicator = 1f;
                }
                if (HardLandscapebool)
                {
                    TI.Hauteur = Random.Range(0f, 4f);
                    TI.multiplicator = 0.25f;
                }
                if(Stalagmitesbool)
                {
                    TI.Hauteur = Random.Range(0f, 16f);
                    TI.multiplicator = 1/16f;
                }
                DefineDanger(Dangerzone);
                TI.ChestPrefab = ChestPrefab;
                TI.HasChest = generatingChest(ProbOfChest);
                TI.pickupPrefab = PickupPrefab;
                TI.hasPickup = generatingPickup(2);
                makeWall(TI);
                CooToThuileInfo.Add(MapId+makeCoo(x, y), TI);
                //ListeDeThuileInfo.Add(TI);
                NewPlateau.Thuiles.Add(Thuile);//ajoute les thuiles au plateau

            }
        }
    }
    public void DefineDanger(int D)
    {
        if(D==1)
        {
            
            Dangermin = 30f;
            Dangermax = 60f;
        }
        if(D==2)
        {
            Dangermin = 20f;
            Dangermax = 40f;
        }
        if(D==3)
        {
            Dangermin = 10f;
            Dangermax = 30f;
        }
        if (D == 4)
        {
            Dangermin = 5f;
            Dangermax = 15f;
        }
        if (D == 5)
        {
            Dangermin = 1f;
            Dangermax = 5f;
        }
    }
    
    public string makeCoo(int x, int y)
    {
        return (x.ToString() + "," + y.ToString());
    }
    public void makeWall(ThuileInfo info)
    {
        if (info.cooX == 0 || info.cooY == 0 || info.cooX == XSize - 1 || info.cooY == YSize - 1)
        {
            if (realsquarebool)
            {
                info.Hauteur = 5;
            }
            if (HardLandscapebool || SoftLandscapebool)
            {
                info.Hauteur = 10;
            }

            info.hasPickup = false;
            info.HasChest = false;
        }
    }
    void makeMountainsWalls(ThuileInfo info)
    {
        if (info.cooX == 0 || info.cooY == 0 || info.cooX == XSize - 1 || info.cooY == YSize - 1)
        {
            MakeMountainAt(info, 20, 20);

            info.hasPickup = false;
            info.HasChest = false;
        }
    }
    public void MakeMountain()
    {
        print(CooToThuileInfo[MountainCoo].coo);
        MakeMountainAt(CooToThuileInfo[MountainCoo], mountainHeight, MountainRadius);
    }
    public void MakeRandomMountain()
    {
        int roll = Random.Range(0, ListeDeThuileInfo.Count);
        if (ListeDeThuileInfo[roll] != null)
        {
            MakeMountainAt(ListeDeThuileInfo[roll], Random.Range(0, mountainHeight), Random.Range(0, MountainRadius));
        }

    }
    public void MakeMountainAt(ThuileInfo info, float Height, int Radius)
    {
        if (realsquarebool)
        {
            for (int x = info.cooX - Radius; x < info.cooX + Radius; x++)
            {
                for (int y = info.cooX - Radius; y < info.cooX + Radius; y++)
                {
                    //pour tout ce qui est dans ce rayon rajoute de la hauteur
                    if (CooToThuileInfo[makeCoo(x, y)] != null)
                    {
                        CooToThuileInfo[makeCoo(x, y)].Hauteur += Height;
                        CooToThuileInfo[makeCoo(x, y)].updateme();
                    }

                }
            }
        }
        if (SoftLandscapebool)
        {
            info.gameObject.transform.localScale = new Vector3(Random.Range(0, MountainRadius), Random.Range(0, mountainHeight), Random.Range(0, MountainRadius));
        }
        if(HardLandscapebool)
        {
            int roll = Random.Range(0, mountainHeight);
            info.gameObject.transform.localScale = new Vector3(roll, roll, roll);
        }
        
    }
    
   public  bool generatingChest(int prob)
    {
        int roll = Random.Range(0, 100);
        if (roll <= prob)
        {
            return true;
        }
        if (roll >= prob)
        {
            return false;
        }
        
        else
        {
            print("Probleme Generating Chest roll= " + roll.ToString());
            return false;
        }
    }
    public bool generatingPickup(int prob)
    {
        int roll = Random.Range(0, 100);
        if (roll <= prob)
        {
            return true;
        }
        if (roll >= prob)
        {
            return false;
        }

        else
        {
            print("Probleme Generating Chest roll= " + roll.ToString());
            return false;
        }
    }
    float T1 = 0f;
    float SpawnTime = 1f;
    public void GeneratingEnemy(float minSpawnTime,float MaxSpawnTime)
    {
        
        if (P!=null)
        {
            
            
            T1 += Time.deltaTime;
            if (T1 >= SpawnTime)
            {
                Vector3 SpawnLocation = P.transform.position;

               while (Vector3.Distance(SpawnLocation, P.transform.position) <= 2)
                {
                    SpawnLocation = new Vector3(Random.Range(0, XSize), 0.5f, Random.Range(0, YSize));
                }
                GameObject E=Instantiate(EnemyPrefab, SpawnLocation, Quaternion.identity);
                E.GetComponent<enemyBehavior>().Target = P;
                print("EnemyGenerated");
                T1 = 0f;
                SpawnTime = Random.Range(minSpawnTime, MaxSpawnTime);
            }
        }
        
        //Je genere un spawn time random et un timer
        //quand le timer atteind spawn je genere un enemie a des coodones random mais si cest trop proche du joueur trouve un autre endroit

    }
}
