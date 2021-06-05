using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamUI : MonoBehaviour
{
    CreateTeamUIManager manager;
    public TeamData TeamData { get; private set; }

    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(CreateTeamUIManager p_manager, TeamData p_teamData)
    {
        TeamData = p_teamData;
        manager = p_manager;

        Transform Image = transform.Find("Image");
        Transform Wins = transform.Find("Wins");
        Transform Loss = transform.Find("Loss");
        Transform Consecutive = transform.Find("Consecutive");
        Transform Cost = transform.Find("Cost");
        Transform Hero1 = transform.Find("Hero1");
        Transform Hero2 = transform.Find("Hero2");
        Transform Hero3 = transform.Find("Hero3");
        Transform Hero4 = transform.Find("Hero4");
        Transform Hero5 = transform.Find("Hero5");
        Transform Hero6 = transform.Find("Hero6");
        Transform Name = transform.Find("Name");

        Name.GetComponent<Text>().text = "Name : " + p_teamData.Name;
        Wins.GetComponent<Text>().text = "Wins : " + p_teamData.Wins.ToString();
        Loss.GetComponent<Text>().text = "Loss : " + p_teamData.Loss.ToString();
        Cost.GetComponent<Text>().text = "Cost : " + p_teamData.Cost.ToString();

        if (p_teamData.ConsecutiveLoss != 0)
            Consecutive.GetComponent<Text>().text = "Consecutive Loss : " + p_teamData.ConsecutiveLoss.ToString();
        else
            Consecutive.GetComponent<Text>().text = "Consecutive Wins : " + p_teamData.ConsecutiveWins.ToString();

        //Image.GetComponent<Image>().sprite = Resources.Load(p_teamData.Image);
        //
        //Hero1.GetComponent<Image>().sprite = Resources.Load( /* path to classes images + */p_teamData.HeroesDatas[0].Class.ToString());
        //Hero2.GetComponent<Image>().sprite = Resources.Load( /* path to classes images + */p_teamData.HeroesDatas[1].Class);
        //Hero3.GetComponent<Image>().sprite = Resources.Load( /* path to classes images + */p_teamData.HeroesDatas[2].Class);
        //Hero4.GetComponent<Image>().sprite = Resources.Load( /* path to classes images + */p_teamData.HeroesDatas[3].Class);
        //Hero5.GetComponent<Image>().sprite = Resources.Load( /* path to classes images + */p_teamData.HeroesDatas[4].Class);
        //Hero6.GetComponent<Image>().sprite = Resources.Load( /* path to classes images + */p_teamData.HeroesDatas[5].Class);
    }

    public void OnButtonClick()
    {
        manager.SelectTeam(this);
        Debug.Log("click on team UI");
    }
}
