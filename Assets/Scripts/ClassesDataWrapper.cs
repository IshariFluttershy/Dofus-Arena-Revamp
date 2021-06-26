using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClassesDataWrapper
{
    public List<HeroClassData> classes;

    public ClassesDataWrapper()
    {
        classes = new List<HeroClassData>();
    }
}
