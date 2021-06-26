using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StuffsWrapper 
{
    public List<StuffData> stuffs;

    public StuffsWrapper()
    {
        stuffs = new List<StuffData>();
    }
}
