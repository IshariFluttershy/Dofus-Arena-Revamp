using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
{
    static ActionsManager _instance;
    public static ActionsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ActionsManager>();
            }

            return _instance;
        }
    }
         


    Dictionary<int, ActionData> datas;

    public void Init(List<ActionData> p_datas)
    {
        datas = new Dictionary<int, ActionData>();
        foreach (var data in p_datas)
        {
            datas.Add(data.Id, data);
        }
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickActionButton(int p_index)
    {
        Hero hero = TurnBasedSystem.Instance.GetCurrentHero();
        hero.SelectAction(p_index);
    }

    public ActionData GetActionDataFromId(int p_id)
    {
        return datas[p_id];
    }
}
