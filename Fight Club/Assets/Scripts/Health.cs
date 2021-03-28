using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPunCallbacks
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxStamina = 100;
    public int currentStamina;
    private static readonly int Attacked = Animator.StringToHash("Attacked");

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

    [PunRPC]
    public void TakeDamage(int p_damage)
    {
        //if (photonView.IsMine)
       // {
            currentHealth -= p_damage;
            GetComponent<Animator>().SetTrigger(Attacked);
            Debug.Log(currentHealth);
       // }
    }
}
