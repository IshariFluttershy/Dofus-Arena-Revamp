using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTeamUIManager : MonoBehaviour
{
    public TeamUI SelectedTeam;
    List<TeamData> teamDatas;

    [SerializeField]
    GameObject teamUIPrefab;
    [SerializeField]
    SelectedTeamUI selectedTeamUI;

    List<TeamUI> teamUIs;

    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
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
            Destroy(ui);
        }

        int i = 0;

        foreach (var teamData in teamDatas)
        {
            GameObject teamUIObject = Instantiate(teamUIPrefab, canvas.transform);

            Vector3 oldPos = teamUIObject.transform.position;
            teamUIObject.transform.position = oldPos + new Vector3(0, i * -160, 0);

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
        UpdateSelectedUIDisplay();
    }
}
