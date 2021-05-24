using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ActionData
{
    public string Name;
    public int MinRange;
    public int MaxRange;
    public int PACost;
    public int Damages;
    public bool IsBlocking;

    public int Id;
}
