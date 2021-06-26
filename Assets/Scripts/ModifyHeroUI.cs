using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyHeroUI : MonoBehaviour
{
    StuffType selectedStuffType;
    HeroData hero;

    [SerializeField]
    TeamBuilderUI teamBuilderUI;

    [SerializeField]
    GameObject spellsTab;
    [SerializeField]
    GameObject stuffTab;

    List<GameObject> stuffUIs;
    List<GameObject> spellUIs;

    [SerializeField]
    GameObject stuffUIPrefab;
    [SerializeField]
    GameObject spellUIPrefab;

    [SerializeField]
    GameObject stuffUIContent;
    [SerializeField]
    GameObject spellUIContent;

    // Start is called before the first frame update
    void Awake()
    {
        spellsTab.SetActive(true);
        stuffTab.SetActive(false);

        stuffUIs = new List<GameObject>();
        spellUIs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyHero(HeroData p_hero)
    {
        hero = p_hero;
        UpdateHeroUI();
        Debug.Log("Modify hero called");
    }

    public void SaveHero()
    {
        if (hero.Name == "")
        {
            transform.Find("EmptyNameErrorMessage").gameObject.SetActive(true);
            return;
        }

        transform.Find("EmptyNameErrorMessage").gameObject.SetActive(false);
        teamBuilderUI.SaveHero(hero);
        teamBuilderUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CancelHero()
    {
        teamBuilderUI.gameObject.SetActive(true);
        teamBuilderUI.CancelHeroModification();
        gameObject.SetActive(false);
        transform.Find("EmptyNameErrorMessage").gameObject.SetActive(false);
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

    public void ChangeHeroName(string p_name)
    {
        hero.Name = p_name;
        Debug.Log("Change hero name : " + p_name);

        if (hero.Name != "")
            transform.Find("EmptyNameErrorMessage").gameObject.SetActive(false);
    }

    public void UpdateHeroStats()
    {
        hero.MaxHP = hero.ClassStats.MaxHP;
        hero.MaxPA = hero.ClassStats.MaxPA;
        hero.MaxPM = hero.ClassStats.MaxPM;

        if (hero.Head != null)
        {
            hero.MaxHP += hero.Head.MaxHP;
            hero.MaxPA += hero.Head.MaxPA;
            hero.MaxPM += hero.Head.MaxPM;
        }
        if (hero.Back != null)
        {
            hero.MaxHP += hero.Back.MaxHP;
            hero.MaxPA += hero.Back.MaxPA;
            hero.MaxPM += hero.Back.MaxPM;

        }
        if (hero.Torso != null)
        {
            hero.MaxHP += hero.Torso.MaxHP;
            hero.MaxPA += hero.Torso.MaxPA;
            hero.MaxPM += hero.Torso.MaxPM;

        }
        if (hero.Weapon != null)
        {
            hero.MaxHP += hero.Weapon.MaxHP;
            hero.MaxPA += hero.Weapon.MaxPA;
            hero.MaxPM += hero.Weapon.MaxPM;

        }
        if (hero.Bonus != null)
        {
            hero.MaxHP += hero.Bonus.MaxHP;
            hero.MaxPA += hero.Bonus.MaxPA;
            hero.MaxPM += hero.Bonus.MaxPM;

        }
    }

    public void UpdateHeroUI()
    {
        UpdateHeroStats();

        Transform HeroNameInput = transform.Find("HeroNameInput");
        HeroNameInput.GetComponent<InputField>().text = hero.Name;
        HeroNameInput.GetComponent<InputField>().textComponent.text = hero.Name;

        Transform PAText = transform.Find("PAText");
        Transform PMText = transform.Find("PMText");
        Transform HPText = transform.Find("HPText");

        PAText.GetComponent<Text>().text = hero.MaxPA.ToString();
        PMText.GetComponent<Text>().text = hero.MaxPM.ToString();
        HPText.GetComponent<Text>().text = hero.MaxHP.ToString();

        


    }

    public void UpdateStuffUI()
    {
        Transform StuffTab = transform.Find("StuffTab");

        Transform Head = StuffTab.Find("Head");
        Transform Torso = StuffTab.Find("Torso");
        Transform Back = StuffTab.Find("Back");
        Transform Weapon = StuffTab.Find("Weapon");
        Transform Bonus = StuffTab.Find("Bonus");

        Transform StuffWindow = StuffTab.Find("StuffWindow");

        foreach (var stuffUI in stuffUIs)
        {
            Destroy(stuffUI);
        }

        stuffUIs.Clear();

        List<StuffData> stuffDatas = DataManager.Instance.GetAllStuffsFromType(selectedStuffType);

        int i = 0;

        foreach (var stuff in stuffDatas)
        {
            int result1 = (i % 2);
            int result2 = (i / 3);

            GameObject stuffUIObject = Instantiate(stuffUIPrefab, Vector3.zero, Quaternion.identity, stuffUIContent.transform);

            stuffUIObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-43.0f + (i%2)*87.0f, 103.0f + (i/2)*-87.0f);
            Image stuffUI = stuffUIObject.GetComponent<Image>();
            stuffUI.sprite = Resources.Load<Sprite>(Application.dataPath + "/Sprites/" + stuff.IconPath);
            stuffUIs.Add(stuffUIObject);

            i++;
        }
    }
       
    public void SetSelecteStuffType(int p_id)
    {
        selectedStuffType = (StuffType)p_id;
        UpdateStuffUI();
    }
}
