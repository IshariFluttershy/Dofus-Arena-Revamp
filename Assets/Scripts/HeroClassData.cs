using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroClassData : HeroStatsModifierData
{
    public HeroClassData(HeroClass p_class)
    {
        Class = p_class;
    }

    public HeroClass Class;
}
