using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateHeroUI : MonoBehaviour
{
    HeroData hero;

    [SerializeField]
    ModifyHeroUI modifyHeroUI;
    [SerializeField]
    TeamBuilderUI teamBuilderUI;

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
        hero = new HeroData();

    }

    public void SaveHero()
    {
        modifyHeroUI.ModifyHero(hero);
        gameObject.SetActive(false);
        modifyHeroUI.gameObject.SetActive(true);
    }

    public void CancelHero()
    {
        hero = null;
        gameObject.SetActive(false);
        teamBuilderUI.gameObject.SetActive(true);
    }

    void UpdateCreateHeroUI()
    {
        Transform HeroNameInput = transform.Find("HeroNameInput");
        HeroNameInput.GetComponent<InputField>().text = hero.Name;
        HeroNameInput.GetComponent<InputField>().textComponent.text = hero.Name;

        Transform IskaiWarrior = transform.Find("IskaiWarrior");
        Transform CeltWarrior = transform.Find("CeltWarrior");
        Transform IskaiMagician = transform.Find("IskaiMagician");
        Transform CeltMagician = transform.Find("CeltMagician");

        Transform HeroImage = transform.Find("HeroImage");
        Transform Male = transform.Find("Male");
        Transform Female = transform.Find("Female");
        Transform ClassDescription = transform.Find("ClassDescription");

        Transform ClassName = ClassDescription.Find("ClassName");
        Transform ClassHP = ClassDescription.Find("ClassHP");
        Transform ClassPA = ClassDescription.Find("ClassPA");
        Transform ClassPM = ClassDescription.Find("ClassPM");
        Transform ClassDescriptionText = ClassDescription.Find("ClassDescriptionText");

        ClassName.GetComponent<Text>().text = hero.ClassStats.Name;
        ClassHP.GetComponent<Text>().text = hero.ClassStats.MaxHP.ToString();
        ClassPA.GetComponent<Text>().text = hero.ClassStats.MaxPA.ToString();
        ClassPM.GetComponent<Text>().text = hero.ClassStats.MaxPM.ToString();
        ClassDescriptionText.GetComponent<Text>().text = hero.ClassStats.Description;
    }

    public void ChangeClass(int p_class)
    {
        hero.ClassStats = DataManager.Instance.GetClassDataFromId((HeroClass)p_class);
        hero.Class = (HeroClass)p_class;
        UpdateCreateHeroUI();
    }

    public void ChangeName(string p_name)
    {
        hero.Name = p_name;
    }

    public void ChangeSex(int p_sex)
    {
        hero.Sex = (HeroSex)p_sex;
    }
}
