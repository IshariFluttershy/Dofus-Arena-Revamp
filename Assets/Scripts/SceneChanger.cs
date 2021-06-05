using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Start()
    {
        
    }
    public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene("Scenes/" + sceneName);

	}
	public void Exit()
	{
		Application.Quit();
	}
}