using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static int multiplayerLobbyScene = 1, practiceScene = 3;

    public void LoadMultiplayer()
    {
        SceneManager.LoadScene(multiplayerLobbyScene);
    }

    public void LoadPractice()
    {
        SceneManager.LoadScene(practiceScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
