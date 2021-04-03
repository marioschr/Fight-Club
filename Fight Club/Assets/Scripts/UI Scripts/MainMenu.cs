using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("MultiplayerMenu", 0) == 1)
        {
            GameObject.FindGameObjectWithTag("MainMenuUI Canvas").transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Multiplayer Canvas").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Multiplayer Canvas").transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Multiplayer Canvas").transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            PlayerPrefs.SetInt("MultiplayerMenu", 0);
        }
    }

    public void LoadPractice()
    {
        SceneManager.LoadScene(GameConstants.PRACTICE_SCENE_INDEX);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
