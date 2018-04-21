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
    
    public bool HasMountains;

    public string Coo;

    public List<GameObject> Thuiles = new List<GameObject>();

	public Plateau(int cooX,int cooY,int taille,int Recources)
    {
        //cree le plateau
    }
    
    //Ce plateau doit contenir toutes les informations necessaires pour construire la meme map plusieurs fois.
    //donc, list de gameobjects thuiles.
}
