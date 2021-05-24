using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeroData
{
    public int MaxHP;
    public int MaxPM;
    public int MaxPA;

    public string Name;
    public List<int> ActionsId;
}
