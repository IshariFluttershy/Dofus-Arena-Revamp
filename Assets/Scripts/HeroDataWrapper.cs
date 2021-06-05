using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TeamDataWrapper
{
    public List<HeroData> heroes;

    public TeamDataWrapper()
    {
        heroes = new List<HeroData>();
    }
}
