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

    public List<Chest> Chests = new List<Chest>();
    public List<Mountain> Mountains = new List<Mountain>();
    public List<ThuileInfo> ThuilesInfos = new List<ThuileInfo>();

	public Plateau(int cooX,int cooY,int Taille,int ProbabiliteDeChest,int Type,bool PossedeMontagne,int NombreDeMontagnes,int AverageMountainsRadius,int AverageMountainsRange)
    {
        //cree le plateau
    }
    public void MakeMountains()
    {

    }
    //Ce plateau doit contenir toutes les informations necessaires pour construire la meme map plusieurs fois.
    //liste de monttagnes et emplacements,radius,height; classes???
    //liste des coffres et emplacements; classes???
}
