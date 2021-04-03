﻿using Photon.Pun;
using TMPro;
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
            GameObject gameOver = GameObject.FindGameObjectWithTag("GameUICanvas").transform.GetChild(1).gameObject;
            gameOver.SetActive(true);
            
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<Movement>().enabled = false;
                player.GetComponent<Fighting>().enabled = false;
                player.GetComponent<Rigidbody>().freezeRotation = true;
                if (player.GetComponent<Health>().currentHealth <= 0)
                {
                    player.GetComponent<Animator>().SetBool(Block, false);
                    player.GetComponent<Animator>().SetTrigger(KO);
                    gameOver.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>().text = "LOSER: " + player.GetPhotonView().Owner.NickName;

                }
                else
                {
                    player.GetComponent<Animator>().SetTrigger(Won);
                    gameOver.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = "WINNER: " + player.GetPhotonView().Owner.NickName;
                }
            }
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
