using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HeroSerializer : MonoBehaviour
{
    [SerializeField]
    Hero heroPrefab;

    [SerializeField]
    List<GridCell> playerOneStartCells;
    [SerializeField]
    List<GridCell> playerTwoStartCells;

    private void Awake()
    {
        //Actions read
        string destination = Application.dataPath + "/actionsList.json";
        StreamReader file;

        if (File.Exists(destination))
            file = File.OpenText(destination);
        else 
            return;

        ActionsWrapper wrapper = JsonUtility.FromJson<ActionsWrapper>(file.ReadToEnd());

        file.Close();




        // Heroes read
        destination = Application.dataPath + "/NausicaaTeam.json";
        StreamReader heroFile;
        
        if (File.Exists(destination))
            heroFile = File.OpenText(destination);
        else
            return;

        HeroDataWrapper heroWrapper = JsonUtility.FromJson<HeroDataWrapper>(heroFile.ReadToEnd());

        heroFile.Close();

        // Heroes read
        destination = Application.dataPath + "/KushanaTeam.json";

        if (File.Exists(destination))
            heroFile = File.OpenText(destination);
        else
            return;

        HeroDataWrapper secondHeroWrapper = JsonUtility.FromJson<HeroDataWrapper>(heroFile.ReadToEnd());

        heroFile.Close();





        var actionList = new List<ActionData>();

        foreach (var action in wrapper.actions)
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


        HeroData heroData = heroWrapper.heroes[0];

        var hero = Instantiate<Hero>(heroPrefab);
        hero.Init(heroData);
        hero.SetCurrentCell(playerOneStartCells[0]);

        HeroData secondHeroData = secondHeroWrapper.heroes[0];
        hero = Instantiate<Hero>(heroPrefab);
        hero.Init(secondHeroData);
        hero.SetCurrentCell(playerTwoStartCells[0]);
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
            HeroDataWrapper wrapper = new HeroDataWrapper();

            HeroData heroData = new HeroData();
            heroData.MaxHP = 20;
            heroData.MaxPA = 6;
            heroData.MaxPM = 3;
            heroData.Name = "Nausicaa";
            heroData.ActionsId = new List<int>();
            heroData.ActionsId.Add(0);
            heroData.ActionsId.Add(2);

            wrapper.heroes.Add(heroData);

            string actionJson = JsonUtility.ToJson(wrapper, true);

            string destination = Application.dataPath + "/NausicaaTeam.json";
            StreamWriter file;

            if (File.Exists(destination)) return;
            else file = File.CreateText(destination);

            file.Write(actionJson);

            file.Close();
        }
    }
}
