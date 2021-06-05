using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    [Serializable]
    public class TeamData
{
    public int Cost;
    public int Wins;
    public int Loss;
    public int ConsecutiveWins;
    public int ConsecutiveLoss;

    public string Name;
    public string Image;
    public List<HeroData> HeroesDatas;

    public TeamData()
    {
        HeroesDatas = new List<HeroData>();
    }
}
