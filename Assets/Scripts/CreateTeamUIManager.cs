using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeamUIManager : MonoBehaviour
{
    public TeamUI SelectedTeam;
    List<TeamData> teamDatas;

    public TeamData SelectedTeamData { get; private set; }
    public bool CreatingTeam { get; private set; }

    [SerializeField]
    GameObject teamUIPrefab;
    [SerializeField]
    SelectedTeamUI selectedTeamUI;

    List<TeamUI> teamUIs;

    GameObject content;

    [SerializeField]
    TeamBuilderUI teamBuilderUI;
    [SerializeField]
    ModifyHeroUI modifyHeroUI;
    [SerializeField]
    GameObject vizualiseTeamUI;
    [SerializeField]
    GameObject createHeroUI;

    // Start is called before the first frame update
    void Start()
    {
        content = GameObject.Find("Content");
        teamUIs = new List<TeamUI>();
        teamDatas = HeroSerializer.Instance.TeamDatas;
        UpdateTeamDatasDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTeamDatas(List<TeamData> p_datas)
    {
        teamDatas = p_datas;
    }

    public void UpdateTeamDatasDisplay()
    {
        foreach (var ui in teamUIs)
        {
            Destroy(ui.gameObject);
        }

        teamUIs.Clear();

        int i = 0;

        foreach (var teamData in teamDatas)
        {
            GameObject teamUIObject = Instantiate(teamUIPrefab, Vector3.zero, Quaternion.identity, content.transform);

            teamUIObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(6.5f, -8.5f + i*-60.0f);
            TeamUI teamUI = teamUIObject.GetComponent<TeamUI>();
            teamUI.Init(this, teamData);
            teamUIs.Add(teamUI);

            i++;
        }
    }

    public void UpdateSelectedUIDisplay()
    {
        if (SelectedTeam != null)
        {
            selectedTeamUI.UpdateUIDisplay(SelectedTeam.TeamData);
        }
    }

    public void SelectTeam(TeamUI p_teamUI)
    {
        SelectedTeam = p_teamUI;
        SelectedTeamData = SelectedTeam.TeamData;
        UpdateSelectedUIDisplay();
    }

    public void CreateTeam()
    {
        CreatingTeam = true;
        StartTeamBuildingUI();
    }

    public void ModifyTeam()
    {
        CreatingTeam = false;
        StartTeamBuildingUI();
    }

    public void StartTeamBuildingUI()
    {
        teamBuilderUI.gameObject.SetActive(true);
        teamBuilderUI.CreateTeam();
        modifyHeroUI.gameObject.SetActive(false);
        vizualiseTeamUI.SetActive(false);
        createHeroUI.SetActive(false);
    }

    public void SaveTeam(TeamData p_team)
    {
        if (CreatingTeam == true)
            teamDatas.Add(p_team);
        else
            teamDatas[teamDatas.IndexOf(SelectedTeamData)] = p_team;

        SelectedTeamData = p_team;

        HeroSerializer.Instance.SaveTeam(p_team);

        teamBuilderUI.gameObject.SetActive(false);
        modifyHeroUI.gameObject.SetActive(false);
        vizualiseTeamUI.SetActive(true);
        createHeroUI.SetActive(false);

        UpdateCreateTeamUI();
    }

    public void CancelTeam()
    {
        teamBuilderUI.gameObject.SetActive(false);
        modifyHeroUI.gameObject.SetActive(false);
        vizualiseTeamUI.SetActive(true);
        createHeroUI.SetActive(false);

        UpdateCreateTeamUI();
    }

    public void UpdateCreateTeamUI()
    {
        UpdateSelectedUIDisplay();
        UpdateTeamDatasDisplay();
    }
}
