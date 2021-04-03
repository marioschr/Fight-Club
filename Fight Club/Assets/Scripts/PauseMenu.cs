using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    private bool disconnecting = false;

    public void TogglePause()
    {
        if (disconnecting) return;
        paused = !paused;
        
        transform.GetChild(0).gameObject.SetActive(paused);
        Cursor.lockState = (paused) ? CursorLockMode.None : CursorLockMode.Confined;
        Cursor.visible = paused;
    }

    public void Quit()
    {
        disconnecting = true;
        PhotonNetwork.Disconnect();
        //PlayerPrefs.SetInt("MultiplayerMenu", 1);
        SceneManager.LoadScene(GameConstants.MAIN_MENU_INDEX);
    }

    public void Resume()
    {
        GetComponent<PauseMenu>().TogglePause();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.layer == 9)
            {
                player.GetComponent<Movement>().enabled = true;
                player.GetComponent<Fighting>().enabled = true;
            }
        }
    }
}
