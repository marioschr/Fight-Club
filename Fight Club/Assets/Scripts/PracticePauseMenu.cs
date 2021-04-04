using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticePauseMenu : MonoBehaviour // Pause Menu κατά το practice
{
    public static bool paused,pause = false;
    public GameObject player;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Z = Animator.StringToHash("Z");
    private PracticeFighting fighting;
    private PracticeMovement movement;
    private Animator animator;

    private void Start()
    {
        fighting = player.GetComponent<PracticeFighting>();
        movement = player.GetComponent<PracticeMovement>();
        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        pause = Input.GetKeyDown(KeyCode.Escape);
        if (pause) Pause();
    }


    void Pause()
    {
        animator.SetFloat(X, 0);
        animator.SetFloat(Z, 0);
        movement.enabled = false;
        fighting.enabled = false;
        TogglePause();
    }
    public void TogglePause()
    {
        paused = !paused;
        
        transform.GetChild(0).gameObject.SetActive(paused);
        Cursor.lockState = (paused) ? CursorLockMode.None : CursorLockMode.Confined;
        Cursor.visible = paused;
    }
    
    public void Resume()
    {
        animator.SetFloat(X, 0);
        animator.SetFloat(Z, 0);
        movement.enabled = false;
        fighting.enabled = false;
        TogglePause();
    }

    
    public void Quit()
    {
        SceneManager.LoadScene(GameConstants.MAIN_MENU_INDEX);
    }
}
