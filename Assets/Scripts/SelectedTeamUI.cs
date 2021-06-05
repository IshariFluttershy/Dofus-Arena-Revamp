using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTeamUI : MonoBehaviour
{
    [SerializeField]
    List<SelectedTeamHeroUI> heroUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIDisplay(TeamData p_teamData)
    {
        foreach (var ui in heroUI)
        {
            //ui.ResetUIDisplay();
            ui.gameObject.SetActive(false);
        }

        int i = 0;
        foreach (var hero in p_teamData.HeroesDatas)
        {
            heroUI[i].gameObject.SetActive(true);
            heroUI[i].UpdateUIDisplay(hero);

            i++;
        }
    }
}
