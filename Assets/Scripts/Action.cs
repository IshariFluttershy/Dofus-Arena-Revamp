using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action// : MonoBehaviour
{
    ActionData data;

    public string Name { get { return data.Name; } }
    public int MinRange { get { return data.MinRange; } }
    public int MaxRange { get { return data.MaxRange; } }
    public int PACost { get { return data.PACost; } }
    public int Damages { get { return data.Damages; } }
    public bool IsBlocking { get { return data.IsBlocking; } }

    //private void Start()
    //{
    //    
    //}
    //
    //private void Update()
    //{
    //    
    //}

    public void SetDatas(ActionData p_datas)
    {
        data = p_datas;
    }

    public void DoAction(GridCell p_target)
    {

    }

    public void DoAction(Hero p_target)
    {
        TurnBasedSystem.Instance.GetCurrentHero().PAChange(PACost);
        p_target.ReceiveDamages(Damages);
    }
}
