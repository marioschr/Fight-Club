using UnityEngine;

public class PlayerPause : MonoBehaviour // Έλεγχος αν πατήσει το ESC o χρήστης για να ανοίξει το pause menu
{
    private bool pause;

    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Z = Animator.StringToHash("Z");
    
    // Update is called once per frame
    void Update()
    {
        pause = Input.GetKeyDown(KeyCode.Escape);
        if (pause) Pause();
    }

    void Pause()
    {
        GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenu>().TogglePause();
        if (PauseMenu.paused)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.layer == 9)
                {
                    player.GetComponent<Animator>().SetFloat(X, 0);
                    player.GetComponent<Animator>().SetFloat(Z, 0);
                    player.GetComponent<Movement>().enabled = false;
                    player.GetComponent<Fighting>().enabled = false;
                    return;
                }
            }
        }
        else
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.layer == 9)
                {
                    player.GetComponent<Animator>().SetFloat(X, 0);
                    player.GetComponent<Animator>().SetFloat(Z, 0);
                    player.GetComponent<Movement>().enabled = true;
                    player.GetComponent<Fighting>().enabled = true;
                    return;
                }
            }
        }
    }
}
