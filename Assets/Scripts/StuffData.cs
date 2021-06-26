using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StuffType
{
    Head,
    Torso,
    Back,
    Weapon,
    Bonus
}

[Serializable]
public class StuffData : HeroStatsModifierData
{
    public StuffData(StuffType p_type, HeroClass p_avaliable = HeroClass.IskaiWarrior | 
        HeroClass.IskaiMagician | 
        HeroClass.CeltWarrior | 
        HeroClass.CeltMagician)
    {
        Type = p_type;
        AvaliableClasses = p_avaliable;
    }

    public StuffType Type;
    public HeroClass AvaliableClasses;
}
