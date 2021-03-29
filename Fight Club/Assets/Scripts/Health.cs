using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
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

    private float currentHealthTakeDmg = 100f;
    private static readonly int Block = Animator.StringToHash("Block");
    private static readonly int Won = Animator.StringToHash("Won");

    [PunRPC]
    public void TakeDamage(int damage)
    {
        currentHealthTakeDmg = currentHealth;
        if (GetComponent<Animator>().GetBool(Block))
        {
            currentHealth -= Mathf.RoundToInt(damage / 1.5f);
        }
        else
        {
            currentHealth -= damage;
        }
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            //clientHealthUI.fillAmount = Mathf.Lerp(currentHealthTakeDmg / maxHealth,0, Time.deltaTime * 8f);
            clientHealthUI.fillAmount = 0f;
            GetComponent<Animator>().SetTrigger(KO);
            //GetComponent<Movement>().enabled = false;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.layer == 10)
                {
                    player.GetComponent<Animator>().SetTrigger(Won);
                }
            }
            // TODO: Game ends
        }
        else
        {
            //clientHealthUI.fillAmount = Mathf.Lerp(currentHealthTakeDmg / maxHealth,currentHealth / (float) maxHealth, Time.deltaTime * 8f);
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
