using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroDataWrapper
{
    public List<HeroData> heroes;

    public HeroDataWrapper()
    {
        heroes = new List<HeroData>();
    }
}
