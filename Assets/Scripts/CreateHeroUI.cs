using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHeroUI : MonoBehaviour
{
    HeroData hero;

    [SerializeField]
    TeamBuilderUI teamBuilderUI;

    [SerializeField]
    GameObject spellsTab;
    [SerializeField]
    GameObject stuffTab;

    // Start is called before the first frame update
    void Start()
    {
        spellsTab.SetActive(true);
        stuffTab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyHero(HeroData p_hero)
    {
        hero = p_hero;
    }

    public void SaveHero()
    {
        teamBuilderUI.SaveHero(hero);
        teamBuilderUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CancelHero()
    {
        teamBuilderUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetSpellsTabActive()
    {
        spellsTab.SetActive(true);
        stuffTab.SetActive(false);
    }

    public void SetStuffTabActive()
    {
        spellsTab.SetActive(false);
        stuffTab.SetActive(true);
    }
}
