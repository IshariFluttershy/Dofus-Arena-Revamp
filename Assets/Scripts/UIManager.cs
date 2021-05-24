using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text hpText;
    [SerializeField]
    Text paText;
    [SerializeField]
    Text pmText;

    [SerializeField]
    List<GameObject> actionButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var hero = TurnBasedSystem.Instance.GetCurrentHero();

        if (hero != null)
            hpText.text = "Hp : " + hero.Hp + "/" + hero.MaxHp;
        if (hero != null)
            paText.text = "PA : " + hero.Pa + "/" + hero.MaxPA;
        if (hero != null)
            pmText.text = "PM : " + hero.Pm + "/" + hero.MaxPM;

        for (int i = 9; i >= 0; i--)
        {
            if (i > hero.ActionsCount() - 1)
                actionButtons[i].SetActive(false);
            else
                actionButtons[i].SetActive(true);
        }
    }

    
}
