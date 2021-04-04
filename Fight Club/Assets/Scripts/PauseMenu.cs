using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviourPunCallbacks
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
        PhotonNetwork.LeaveRoom();
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
                return;
            }
        }
    }
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(GameConstants.MAIN_MENU_INDEX);
        base.OnLeftRoom();
    }
}
