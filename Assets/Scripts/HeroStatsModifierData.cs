using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroStatsModifierData 
{
    public int MaxHP;
    public int MaxPM;
    public int MaxPA;

    public string Name;

    public int BasePercentDamages;
    public int BaseRawDamages;
    public int BasePercentResistances;
    public int BaseRawResistances;

    public int BaseBonusRange;
    public int BaseBonusHeal;
    public int BaseReturnDamages;

    public string Description;

    public string IconPath;
}
