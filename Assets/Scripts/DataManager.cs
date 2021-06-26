using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DataManager>();
            }

            return _instance;
        }
    }
         


    Dictionary<int, ActionData> actionsData;
    List<HeroClassData> classesData;
    List<StuffData> stuffsData;

    public void Init(List<ActionData> p_actions, List<HeroClassData> p_classes, List<StuffData> p_stuffs)
    {
        actionsData = new Dictionary<int, ActionData>();
        foreach (var data in p_actions)
        {
            actionsData.Add(data.Id, data);
        }

        classesData = new List<HeroClassData>();
        foreach(var data in p_classes)
        {
            classesData.Add(data);
        }

        stuffsData = new List<StuffData>();
        foreach(var data in p_stuffs)
        {
            stuffsData.Add(data);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
        return actionsData[p_id];
    }

    public HeroClassData GetClassDataFromId(HeroClass p_id)
    {
        return classesData.Find(x => x.Class == p_id);
    }

    public StuffData GetStuffDataFromId(int p_id)
    {
        return stuffsData[p_id];
    }

    public List<StuffData> GetAllStuffsFromType(StuffType p_type)
    {
        return stuffsData.FindAll(x => x.Type == p_type);
    }
}
