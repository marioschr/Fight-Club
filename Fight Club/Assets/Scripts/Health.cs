using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int max_health = 100;
    public int current_health;
    
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void TakeDamage(int p_damage)
    {
        current_health -= p_damage;
        Debug.Log(current_health);
    }
}
