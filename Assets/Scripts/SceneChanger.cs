using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    static SceneChanger _instance;
    public static SceneChanger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SceneChanger>();
                
            }

            return _instance;
        }
    }

    public void Start()
    {
        DontDestroyOnLoad(this);
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