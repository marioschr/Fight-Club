using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    private bool pause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pause = Input.GetKeyDown(KeyCode.G);
        if (pause) Pause();
    }

    void Pause()
    {
        if (pause)
        {
            GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>().TogglePause();
        }
        if (PauseMenu.paused)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.layer == 9)
                {
                    player.GetComponent<Movement>().enabled = false;
                    player.GetComponent<Fighting>().enabled = false;
                }
            }
        }
        else
        {
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
}
