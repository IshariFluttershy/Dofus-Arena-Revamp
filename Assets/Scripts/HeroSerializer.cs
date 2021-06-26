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
    ClassesDataWrapper classesWrapper;
    StuffsWrapper stuffsWrapper;
    public List<TeamData> TeamDatas { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Debug.Log("Before Actions");
        //Actions read
        string destination = Application.dataPath + "/actionsList.json";
        StreamReader actionsFile;

        if (File.Exists(destination))
            actionsFile = File.OpenText(destination);
        else 
            return;

        actionsWrapper = JsonUtility.FromJson<ActionsWrapper>(actionsFile.ReadToEnd());

        actionsFile.Close();

        Debug.Log("Before Teams");
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






        Debug.Log("Before classes");

        //Classes
        destination = Application.dataPath + "/classesList.json";
        StreamReader classesFile;

        if (File.Exists(destination))
            classesFile = File.OpenText(destination);
        else
            return;

        classesWrapper = JsonUtility.FromJson<ClassesDataWrapper>(classesFile.ReadToEnd());

        classesFile.Close();


        Debug.Log("Before stuffs");
        //stuffs
        destination = Application.dataPath + "/stuffsList.json";
        StreamReader stuffsFile;

        if (File.Exists(destination))
            stuffsFile = File.OpenText(destination);
        else
            return;

        stuffsWrapper = JsonUtility.FromJson<StuffsWrapper>(stuffsFile.ReadToEnd());

        stuffsFile.Close();

        Debug.Log("after stuffs");

        var actionList = new List<ActionData>();

        foreach (var action in actionsWrapper.actions)
        {
            actionList.Add(action);
        }


        DataManager.Instance.Init(actionList, classesWrapper.classes, stuffsWrapper.stuffs);

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
            heroData.Class = 0;

            team.HeroesDatas.Add(heroData);

            heroData = new HeroData();
            heroData.MaxHP = 35;
            heroData.MaxPA = 5;
            heroData.MaxPM = 5;
            heroData.Name = "Yupa";
            heroData.ActionsId = new List<int>();
            heroData.ActionsId.Add(1);
            heroData.Class = 0;

            team.HeroesDatas.Add(heroData);

            heroData = new HeroData();
            heroData.MaxHP = 10;
            heroData.MaxPA = 4;
            heroData.MaxPM = 4;
            heroData.Name = "Asbel";
            heroData.ActionsId = new List<int>();
            heroData.ActionsId.Add(0);
            heroData.ActionsId.Add(1);
            heroData.Class = 0;

            team.HeroesDatas.Add(heroData);

            team.ConsecutiveLoss = 0;
            team.ConsecutiveWins = 4;
            team.Name = "NausicaaTeam";
            team.Loss = 2;
            team.Wins = 8;
            team.Cost = 6000;

            string actionJson = JsonUtility.ToJson(team, true);

            string destination = Application.dataPath + "/Teams/NausicaaTeam.json";
            StreamWriter file;

            if (File.Exists(destination)) return;
            else file = File.CreateText(destination);

            file.Write(actionJson);

            file.Close();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ClassesDataWrapper wrapper = new ClassesDataWrapper();

            var iskaiWarrior = new HeroClassData(HeroClass.IskaiWarrior);
            iskaiWarrior.MaxHP = 50;
            iskaiWarrior.MaxPA = 6;
            iskaiWarrior.MaxPM = 5;
            iskaiWarrior.BaseRawDamages = 2;

            var celtWarrior = new HeroClassData(HeroClass.CeltWarrior);
            celtWarrior.MaxHP = 80;
            celtWarrior.MaxPA = 6;
            celtWarrior.MaxPM = 3;
            celtWarrior.BaseRawDamages = 8;

            var celtMagician = new HeroClassData(HeroClass.CeltMagician);
            celtMagician.MaxHP = 45;
            celtMagician.MaxPA = 6;
            celtMagician.MaxPM = 3;
            celtMagician.BaseRawDamages = 0;

            var iskaiMagician = new HeroClassData(HeroClass.IskaiMagician);
            iskaiMagician.MaxHP = 25;
            iskaiMagician.MaxPA = 6;
            iskaiMagician.MaxPM = 5;
            iskaiMagician.BaseRawDamages = 2;


            wrapper.classes.Add(iskaiWarrior);
            wrapper.classes.Add(iskaiMagician);
            wrapper.classes.Add(celtWarrior);
            wrapper.classes.Add(celtMagician);

            string classesJson = JsonUtility.ToJson(wrapper, true);

            string destination = Application.dataPath + "/classesList.json";
            StreamWriter file;

            if (File.Exists(destination)) return; //file = File.OpenText(destination);
            else file = File.CreateText(destination);

            file.Write(classesJson);

            file.Close();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StuffsWrapper wrapper = new StuffsWrapper();

            var stuff1 = new StuffData(StuffType.Head);
            stuff1.MaxHP = 20;
            stuff1.MaxPA = 1;
            

            var stuff2 = new StuffData(StuffType.Head);
            stuff2.MaxHP = 4;
            stuff2.MaxPM = 3;

            var stuff3 = new StuffData(StuffType.Bonus);
            stuff3.MaxHP = 10;
            stuff3.MaxPM = 1;

            wrapper.stuffs.Add(stuff1);
            wrapper.stuffs.Add(stuff2);
            wrapper.stuffs.Add(stuff3);

            string stuffJson = JsonUtility.ToJson(wrapper, true);

            string destination = Application.dataPath + "/stuffsList.json";
            StreamWriter file;

            if (File.Exists(destination)) return; //file = File.OpenText(destination);
            else file = File.CreateText(destination);

            file.Write(stuffJson);

            file.Close();
        }
    }

    public void SaveTeam(TeamData p_team)
    {
        string actionJson = JsonUtility.ToJson(p_team, true);

        string destination = Application.dataPath + "/Teams/" + p_team.Name + ".json";
        StreamWriter file;

        if (File.Exists(destination))
        {
            File.Delete(destination);
            file = File.CreateText(destination);
        } 
        else 
            file = File.CreateText(destination);

        file.Write(actionJson);

        file.Close();
    }
}
