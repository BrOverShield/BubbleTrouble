using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateau
{
    //info du plateau creer
    public int Type;
    public int CooLongitude;
    public int CooLatitude;
    public int Size;
    public int ChestProb;
    public int NumberOfMountains;
    public int MountainsRadiusRange;
    public int MountainsHeightRange;
    public int Danger;
    public int MapId;

    public bool HasMountains;

    public string Coo;
    public string TypeName;

    public List<GameObject> Thuiles = new List<GameObject>();

	public Plateau(int cooX,int cooY,int taille,int Recources,int type,int danger,int mapid)
    {
        CooLongitude = cooX;
        CooLatitude = cooY;
        Coo = makeCoo(cooX, cooY);
        Size = taille;
        ChestProb = Recources;
        Type = type;
        TypeName = DefineTypeNameFromInt(type);
        Danger = danger;
        MapId = mapid;
    }
    public string makeCoo(int x, int y)
    {
        return (x.ToString() + "," + y.ToString());
    }
    public string DefineTypeNameFromInt(int n)
    {
        if(n==1)
        {
            return "Volcanique Leger";
        }
        if (n == 2)
        {
            return "Volcanique Fort";
        }
        if (n == 3)
        {
            return "Roche Sedimentaire";
        }
        if (n == 4)
        {
            return "Stalagmites";
        }
        else return "Inconnue";
    }
    //Ce plateau doit contenir toutes les informations necessaires pour construire la meme map plusieurs fois.
    //donc, list de gameobjects thuiles.
}
