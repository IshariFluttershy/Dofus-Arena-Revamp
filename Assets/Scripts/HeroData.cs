using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum HeroClass
{
    IskaiWarrior,
    IskaiMagician,
    CeltWarrior,
    CeltMagician
}


[Serializable]
public class HeroData
{
    public int MaxHP;
    public int MaxPM;
    public int MaxPA;

    public string Name;
    public string Class;
    public List<int> ActionsId;
}
