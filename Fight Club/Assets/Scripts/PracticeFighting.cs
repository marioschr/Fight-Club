using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeFighting : MonoBehaviour
{
    private PracticeHealth health; 
    private Animator animator;
    public Attack[] attackMoves;
    private static readonly int Block = Animator.StringToHash("Block");

    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private Coroutine regen;
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    public Image clientStaminaUI;
    void Start()
    {
        health = GetComponent<PracticeHealth>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.layer == 10) return;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Attack(0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Attack(1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, true);
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !animator.GetBool(Attacking) && animator.GetBool(Block))
        {
            animator.SetBool(Block, false);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Attack(2);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Attack(3);
            return;
        }
        
        void Attack(int attackID)
        {
            if (health.currentStamina >= attackMoves[attackID].staminaDrain)
            {
                animator.SetBool(Block, false);
                animator.SetTrigger(attackMoves[attackID].trigger);
                health.currentStamina -= attackMoves[attackID].staminaDrain;
                clientStaminaUI.fillAmount = health.currentStamina / 100f;
                if (regen != null)
                {
                    StopCoroutine(regen);
                }
                regen = StartCoroutine(RegenerateStamina());
            }
        }

        IEnumerator RegenerateStamina()
        {
            yield return new WaitForSeconds(1.5f);
            while (health.currentStamina < health.maxStamina)
            {
                health.currentStamina += health.maxStamina / 100;
                clientStaminaUI.fillAmount = health.currentStamina / 100f;
                yield return regenTick;
            }
            regen = null;
        }
    }
}
