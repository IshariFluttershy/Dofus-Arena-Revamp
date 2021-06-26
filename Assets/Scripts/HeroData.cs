using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags] public enum HeroClass
{
    IskaiWarrior = 0,
    IskaiMagician = 1,
    CeltWarrior = 2,
    CeltMagician = 4
}

public enum HeroSex
{
    Male = 0,
    Female = 1
}


[Serializable]
public class HeroData
{
    public HeroData()
    {
        MaxHP = 0;
        MaxPM = 0;
        MaxPA = 0;

        Name = "";
        Class = HeroClass.IskaiWarrior;
        ActionsId = new List<int>();
    }

    public HeroData(HeroData p_data)
    {
        MaxHP = p_data.MaxHP;
        MaxPM = p_data.MaxPM;
        MaxPA = p_data.MaxPA;

        Name = p_data.Name;
        Class = p_data.Class;
        ActionsId = new List<int>();

        foreach (var id in p_data.ActionsId)
        {
            ActionsId.Add(id);
        }
    }

    public int MaxHP;
    public int MaxPM;
    public int MaxPA;

    public HeroSex Sex;
    public string Name;
    public HeroClass Class;
    public List<int> ActionsId;

    public int PercentDamages;
    public int RawDamages;
    public int PercentResistances;
    public int RawResistances;

    public int BonusRange;
    public int BonusHeal;
    public int ReturnDamages;

    public HeroClassData ClassStats;

    public StuffData Head;
    public StuffData Torso;
    public StuffData Back;
    public StuffData Weapon;
    public StuffData Bonus;
}
