using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HeroSerializer : MonoBehaviour
{
    static HeroSerializer _instance;
    public static HeroSerializer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<HeroSerializer>();
            }

            return _instance;
        }
    }

    [SerializeField]
    Hero heroPrefab;

    [SerializeField]
    List<GridCell> playerOneStartCells;
    [SerializeField]
    List<GridCell> playerTwoStartCells;

    ActionsWrapper actionsWrapper;
    public List<TeamData> TeamDatas { get; private set; }

    private void Awake()
    {
        //Actions read
        string destination = Application.dataPath + "/actionsList.json";
        StreamReader actionsFile;

        if (File.Exists(destination))
            actionsFile = File.OpenText(destination);
        else 
            return;

        actionsWrapper = JsonUtility.FromJson<ActionsWrapper>(actionsFile.ReadToEnd());

        actionsFile.Close();

        TeamDatas = new List<TeamData>();

        string[] files = Directory.GetFiles(Application.dataPath + "/Teams", "*.json");

        foreach (var file in files)
        {
            StreamReader heroFile;

            if (File.Exists(file))
                heroFile = File.OpenText(file);
            else
                return;

            string lol = heroFile.ReadToEnd();
            Debug.Log(lol);
            TeamData firstTeamDatas = JsonUtility.FromJson<TeamData>(lol);
            TeamDatas.Add(firstTeamDatas);
            heroFile.Close();
        }


        //// Heroes read
        //destination = Application.dataPath + "/KushanaTeam.json";
        //
        //if (File.Exists(destination))
        //    heroFile = File.OpenText(destination);
        //else
        //    return;
        //
        //TeamData secondTeamDatas = JsonUtility.FromJson<TeamData>(heroFile.ReadToEnd());
        //TeamDatas.Add(secondTeamDatas);
        //heroFile.Close();





        var actionList = new List<ActionData>();

        foreach (var action in actionsWrapper.actions)
        {
            actionList.Add(action); 
        }

        ActionsManager.Instance.Init(actionList);

        //HeroData heroData = new HeroData();
        //heroData.MaxHP = 20;
        //heroData.MaxPA = 6;
        //heroData.MaxPM = 3;
        //heroData.Name = "Bonjour";
        //heroData.ActionsId = new List<int>();


        //HeroData heroData = heroWrapper.heroes[0];
        //
        //var hero = Instantiate<Hero>(heroPrefab);
        //hero.Init(heroData);
        //hero.SetCurrentCell(playerOneStartCells[0]);
        //
        //HeroData secondHeroData = secondHeroWrapper.heroes[0];
        //hero = Instantiate<Hero>(heroPrefab);
        //hero.Init(secondHeroData);
        //hero.SetCurrentCell(playerTwoStartCells[0]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ActionsWrapper wrapper = new ActionsWrapper();

            var actionDistance = new ActionData();
            actionDistance.MaxRange = 5;
            actionDistance.MinRange = 3;
            actionDistance.Name = "Distance Atk";
            actionDistance.Damages = 5;
            actionDistance.PACost = 3;
            actionDistance.IsBlocking = false;
            actionDistance.Id = 0;

            var actionCac = new ActionData();
            actionCac.MaxRange = 1;
            actionCac.MinRange = 1;
            actionCac.Name = "CaC Atk";
            actionCac.Damages = 12;
            actionCac.PACost = 6;
            actionCac.IsBlocking = false;
            actionCac.Id = 1;

            wrapper.actions.Add(actionDistance);
            wrapper.actions.Add(actionCac);

            string actionJson = JsonUtility.ToJson(wrapper, true);

            string destination = Application.dataPath + "/actionsList.json";
            StreamWriter file;

            if (File.Exists(destination)) return; //file = File.OpenText(destination);
            else file = File.CreateText(destination);

            file.Write(actionJson);

            file.Close();
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            TeamData team = new TeamData();

            HeroData heroData = new HeroData();
            heroData.MaxHP = 20;
            heroData.MaxPA = 6;
            heroData.MaxPM = 3;
            heroData.Name = "Nausicaa";
            heroData.ActionsId = new List<int>();
            heroData.ActionsId.Add(0);
            heroData.ActionsId.Add(2);

            team.HeroesDatas.Add(heroData);

            heroData = new HeroData();
            heroData.MaxHP = 35;
            heroData.MaxPA = 5;
            heroData.MaxPM = 5;
            heroData.Name = "Yupa";
            heroData.ActionsId = new List<int>();
            heroData.ActionsId.Add(1);

            team.HeroesDatas.Add(heroData);

            heroData = new HeroData();
            heroData.MaxHP = 10;
            heroData.MaxPA = 4;
            heroData.MaxPM = 4;
            heroData.Name = "Asbel";
            heroData.ActionsId = new List<int>();
            heroData.ActionsId.Add(0);
            heroData.ActionsId.Add(1);

            team.HeroesDatas.Add(heroData);

            team.ConsecutiveLoss = 0;
            team.ConsecutiveWins = 4;
            team.Name = "NausicaaTeam";
            team.Loss = 2;
            team.Wins = 8;
            team.Cost = 6000;

            string actionJson = JsonUtility.ToJson(team, true);

            string destination = Application.dataPath + "/NausicaaTeam.json";
            StreamWriter file;

            if (File.Exists(destination)) return;
            else file = File.CreateText(destination);

            file.Write(actionJson);

            file.Close();
        }
    }
}
