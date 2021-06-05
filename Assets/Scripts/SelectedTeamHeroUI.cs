using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTeamHeroUI : MonoBehaviour
{
    public HeroData heroData { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUIDisplay(HeroData p_hero)
    {
        heroData = p_hero;

        Transform Image = transform.Find("Image");
        Transform Name = transform.Find("Name");

        Transform Spell1 = transform.Find("Spell1");
        Transform Spell2 = transform.Find("Spell2");
        Transform Spell3 = transform.Find("Spell3");
        Transform Spell4 = transform.Find("Spell4");
        Transform Spell5 = transform.Find("Spell5");
        Transform Spell6 = transform.Find("Spell6");

        Transform Weapon = transform.Find("Weapon");
        Transform Head = transform.Find("Head");
        Transform Torso = transform.Find("Torso");
        Transform Back = transform.Find("Back");
        Transform Bonus = transform.Find("Bonus");

        Name.GetComponent<Text>().text = p_hero.Name;
    }
}
