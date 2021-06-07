using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBuilderUI : MonoBehaviour
{
    TeamData teamData;

    [SerializeField]
    List<SelectedTeamHeroUI> heroUIs;

    [SerializeField]
    CreateHeroUI createHeroUI;

    int modifiedHeroIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateHero()
    {
        createHeroUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ModifyHero()
    {
        createHeroUI.gameObject.SetActive(true);
        //modifiedHeroIndex = ;
        createHeroUI.ModifyHero(teamData.HeroesDatas[modifiedHeroIndex]);
        gameObject.SetActive(false);
    }

    public void SaveHero(HeroData p_hero)
    {
        teamData.HeroesDatas.Add(p_hero);
    }


    public void UpdateTeamUI()
    {
        int i = 0;

        foreach (var ui in heroUIs)
        {
            ui.gameObject.SetActive(false);
        }

        foreach (var hero in teamData.HeroesDatas)
        {
            heroUIs[i].gameObject.SetActive(true);
            heroUIs[i].UpdateUIDisplay(hero);
            i++;
        }
    }
}
