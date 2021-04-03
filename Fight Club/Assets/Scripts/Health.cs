using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviourPunCallbacks
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxStamina = 100;
    public int currentStamina;
    private static readonly int Attacked = Animator.StringToHash("Attacked");
    private static readonly int AlreadyAttacked = Animator.StringToHash("AlreadyAttacked");
    private static readonly int KO = Animator.StringToHash("KO");
    private Image clientHealthUI;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static readonly int Block = Animator.StringToHash("Block");
    private static readonly int Won = Animator.StringToHash("Won");

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (GetComponent<Animator>().GetBool(Block))
        {
            currentHealth -= Mathf.RoundToInt(damage / 1.5f);
        }
        else
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            clientHealthUI.fillAmount = 0f;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                GetComponent<Movement>().enabled = false;
                GetComponent<Fighting>().enabled = false;
                if (player.GetComponent<Health>().currentHealth <= 0)
                {
                    player.GetComponent<Animator>().SetTrigger(KO);
                    player.GetComponent<Rigidbody>().freezeRotation = true;
                    
                }
                else
                {
                    player.GetComponent<Animator>().SetTrigger(Won);
                }
            }
            // TODO: Game ends
        }
        else
        {
            clientHealthUI.fillAmount = currentHealth / 100f;
            if (!GetComponent<Animator>().GetBool(AlreadyAttacked))
            {
                GetComponent<Animator>().SetTrigger(Attacked);
            }
        }
    }

    [PunRPC]
    public void SetHealthUI()
    {
        if (photonView.IsMine)
        {
            clientHealthUI = GameObject.Find("GUI/Canvas/Right/Health/Bar").GetComponent<Image>();
        }
        else
        {
            clientHealthUI = GameObject.Find("GUI/Canvas/Left/Health/Bar").GetComponent<Image>();
        }
    }
}
