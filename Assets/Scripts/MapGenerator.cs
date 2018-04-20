using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Minimap
//TODO: Controle de sousmarin
//TODO: Base building
//TODO: recolte de recources et systeme magasin
//TODO: Make Mountains Walls
//TODO: Mountain Maker Set Enable/Disable bouton
//TODO: Corriger le noms des bool square par SoftLandscape
//TODO: Decouverte des plataux sous marins et affichage des proprietes de l<ile decouverte.
//TODO: Refaire le script de controle Camera
//TODO: Raycast Player
//TODO: Tunel,stalactites,gouffres,bords falaises,empirer le landscape

    //!!!!!!!!!! TODO: Je n<ajoute pas les informations du plateaux a la creation de la map mais a sa destruction; 
    //lorsque je range la map je range tout dans des listes de thuileInfo et chestInfo!!!!!!!!!!!!! OU MIEUX!!!!
    // DES LISTES DE GAMEOBJECT POUR CHAQUE GAMEOBJECTS INSTANTIERS ET QUAND JE RECONSTRUIT LA MAP ET FAIT SIMPLEMENT LES REINSTANTIER

public class MapGenerator : MonoBehaviour {
    public GameObject EnemyPrefab;
    public GameObject squareThuile;
    public GameObject mytimer;
    public GameObject thuilePrefab;
    public GameObject ChestPrefab;
    public GameObject Player;
    public GameObject PickupPrefab;
    public GameObject DestroyOnStartClic;

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

    public int XSize = 10;
    public int YSize = 10;
    public int ProbOfChestatstart = 10;
    public int MountainRadius = 5;
    public int mountainHeight = 5;

    public string MountainCoo = "10,10";

    bool squarebool = false;
    bool lanscapebool = true;
    bool realsquarebool = false;

    GameObject Thuile;
    GameObject P;

    Dictionary<string, ThuileInfo> CooToThuileInfo = new Dictionary<string, ThuileInfo>();

    List<ThuileInfo> ListeDeThuileInfo = new List<ThuileInfo>();

    void Start()
    {
        //  GenerateMap(XSize,YSize,ProbOfChestatstart);
    }
    public void Landscapesoft()
    {
        squarebool = true;
        lanscapebool = false;
        realsquarebool = false;
    }
    public void landscapHard()
    {
        squarebool = false;
        lanscapebool = true;
        realsquarebool = false;
    }
    public void realquare()
    {
        squarebool = false;
        lanscapebool = false;
        realsquarebool = true;
    }

    void Update()
    {
        XSize = (int)sliderx.value;
        YSize = (int)slidery.value;
        ProbOfChestatstart = (int)sliderDiff.value;
        GeneratingEnemy(1f, 5f);
        MountainRadius = (int)MountainRadiusSlider.value;
        mountainHeight = (int)MountainHeightSlider.value;
        MountainCoo = MountainCooInputField.text;

    }
    public void OnClicStart()
    {

        GenerateMap(XSize, YSize, ProbOfChestatstart);
        MakeMountainAt(CooToThuileInfo["10,10"], 5, 5);
        Instantiate(lighte);
        // Instantiate(lightsupport);
        P = Instantiate(Player, new Vector3(2, 2, 2), Quaternion.identity);
        P.GetComponent<PlayerController>().countText = counttext;
        P.GetComponent<PlayerController>().countPickUp = pickuptxt;
        P.GetComponent<PlayerController>().TaillePlayerSlider = PlayerTailleSlider;
        Camera.main.GetComponent<camera>().player = P;
        Camera.main.GetComponent<camera>().isSubmarine = false;
        Camera.main.transform.position = new Vector3(2, 7, -1);
        Camera.main.GetComponent<camera>().offset = new Vector3(2, 7, -1) - P.transform.position;
        GameObject T = Instantiate(mytimer);
        T.GetComponent<timer>().timerText = timertext;
        Destroy(DestroyOnStartClic);
        //TODO: Ajouter cette map au plateaux
    }
    public void GenerateMap(int mapLongeur, int mapLargeur, int ProbOfChest)
    {
        for (int x = 0; x < mapLargeur; x++)
        {
            for (int y = 0; y < mapLongeur; y++)
            {
                if (squarebool || lanscapebool)
                {
                    Thuile = Instantiate(thuilePrefab);
                }
                if (realsquarebool)
                {
                    Thuile = Instantiate(squareThuile);
                }

                ThuileInfo TI = Thuile.GetComponent<ThuileInfo>();
                TI.cooX = x;
                TI.cooY = y;
                if (squarebool || realsquarebool)
                {
                    TI.Hauteur = Random.Range(0f, 1f);
                    TI.multiplicator = 1f;
                }
                if (lanscapebool)
                {
                    TI.Hauteur = Random.Range(0f, 4f);
                    TI.multiplicator = 0.25f;
                }

                TI.ChestPrefab = ChestPrefab;
                TI.HasChest = generatingChest(ProbOfChest);
                TI.pickupPrefab = PickupPrefab;
                TI.hasPickup = generatingPickup(2);
                makeWall(TI);
                //makeMountainsWalls(TI);
                CooToThuileInfo.Add(makeCoo(x, y), TI);
                ListeDeThuileInfo.Add(TI);


            }
        }
    }
    string makeCoo(int x, int y)
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
            if (lanscapebool || squarebool)
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
        if (squarebool)
        {
            info.gameObject.transform.localScale = new Vector3(Random.Range(0, MountainRadius), Random.Range(0, mountainHeight), Random.Range(0, MountainRadius));
        }
        if(lanscapebool)
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
