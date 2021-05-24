using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ActionsWrapper
{
    public List<ActionData> actions;

    public ActionsWrapper()
    {
        actions = new List<ActionData>();
    }
}
