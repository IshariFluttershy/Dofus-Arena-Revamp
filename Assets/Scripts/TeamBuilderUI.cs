using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBuilderUI : MonoBehaviour
{
    TeamData teamData;

    [SerializeField]
    CreateTeamUIManager createTeamUIManager;

    [SerializeField]
    List<SelectedTeamHeroUI> heroUIs;

    [SerializeField]
    ModifyHeroUI modifyHeroUI;
    [SerializeField]
    CreateHeroUI createHeroUI;

    GameObject EmptyNameErrorMessage;
    GameObject NoHeroesErrorMessage;

    int modifiedHeroIndex;

    private void Awake()
    {
        EmptyNameErrorMessage = transform.Find("EmptyNameErrorMessage").gameObject;
        NoHeroesErrorMessage = transform.Find("NoHeroesErrorMessage").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //EmptyNameErrorMessage = transform.Find("EmptyNameErrorMessage").gameObject;
        //NoHeroesErrorMessage = transform.Find("NoHeroesErrorMessage").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTeam()
    {
        modifiedHeroIndex = -1;
        createTeamUIManager = FindObjectOfType<CreateTeamUIManager>();

        if (!createTeamUIManager.CreatingTeam)
            teamData = createTeamUIManager.SelectedTeamData;
        else teamData = new TeamData();



        UpdateTeamUI();
    }

    public void ClickedOnHero(int p_index)
    {
        if (heroUIs[p_index].heroData != null)
            ModifyHero(p_index);
        else
            CreateHero();
    }

    public void CreateHero()
    {
        createHeroUI.CreateHero();
        createHeroUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ModifyHero(int p_index)
    {
        modifyHeroUI.gameObject.SetActive(true);
        modifiedHeroIndex = p_index;
        modifyHeroUI.ModifyHero(new HeroData(teamData.HeroesDatas[modifiedHeroIndex]));
        gameObject.SetActive(false);
    }

    public void SaveHero(HeroData p_hero)
    {
        if (teamData == null)
            Debug.Log("teamdata == null");

        if (modifiedHeroIndex != -1)
            teamData.HeroesDatas[modifiedHeroIndex] = p_hero;
        else
            teamData.HeroesDatas.Add(p_hero);

        modifiedHeroIndex = -1;
        UpdateTeamUI();
    }

    public void CancelHeroModification()
    {
        modifiedHeroIndex = -1;
        UpdateTeamUI();
    }

    public void SaveTeam()
    {
        if (teamData.Name == "" || teamData.Name == null)
        {
            EmptyNameErrorMessage.SetActive(true);
            return;
        }

        if (teamData.HeroesDatas.Count == 0 || teamData.HeroesDatas == null)
        {
            NoHeroesErrorMessage.SetActive(true);
            return;
        }

        EmptyNameErrorMessage.SetActive(false);
        NoHeroesErrorMessage.SetActive(false);
        createTeamUIManager.SaveTeam(teamData);
    }

    public void CancelTeam()
    {
        EmptyNameErrorMessage.SetActive(false);
        NoHeroesErrorMessage.SetActive(false);
        createTeamUIManager.CancelTeam();
    }

    public void ChangeTeamName(string p_name)
    {
        teamData.Name = p_name;

        if (teamData.Name != "")
            transform.Find("EmptyNameErrorMessage").gameObject.SetActive(false);
    }

    public void UpdateTeamUI()
    {
        int i = 0;

        //foreach (var ui in heroUIs)
        //{
        //    ui.gameObject.SetActive(false);
        //}

        Transform TeamNameInputField = transform.Find("InputField");
        TeamNameInputField.GetComponent<InputField>().text = teamData.Name;
        TeamNameInputField.GetComponent<InputField>().textComponent.text = teamData.Name;

        foreach (var ui in heroUIs)
        {
            // heroUIs[i].gameObject.SetActive(true);
            if (i < teamData.HeroesDatas.Count)
                ui.UpdateUIDisplay(teamData.HeroesDatas[i]);
            else
                ui.UpdateUIDisplay(null);
            i++;
        }

        if (teamData.HeroesDatas.Count != 0 && teamData.HeroesDatas != null)
            NoHeroesErrorMessage.SetActive(false);
    }
}
