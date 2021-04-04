using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Fighting : MonoBehaviourPunCallbacks // Το script που γίνεται ο χειρισμός των επιθέσεων
{
    private Health health; 
    private Animator animator;
    public Attack[] attackMoves;
    private static readonly int Block = Animator.StringToHash("Block");

    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    public Coroutine regen;
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    public Image clientStaminaUI;
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!photonView.IsMine) return; // Ελέγχουμε αν ο χαρακτήρας είναι ο δικός μας και όχι του αντιπάλου
        
        //Ανάλογα τα κουμπιά που θα πατήσει να γίνει το αντίστοιχο attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            photonView.RPC("Attack", RpcTarget.All, 0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            photonView.RPC("Attack", RpcTarget.All, 1);
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
            photonView.RPC("Attack", RpcTarget.All, 2);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            photonView.RPC("Attack", RpcTarget.All, 3);
            return;
        }
    }

    // Αποστολή της επίθεσης για να εκτελεστεί από τον server
    [PunRPC]
    void Attack(int attackID)
    {
        if (health.currentStamina >= attackMoves[attackID].staminaDrain) // Αν έχουμε το stamina Που χρειάζεται η επίθεση
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

    public IEnumerator RegenerateStamina() // Ξεκινάει το γέμισμα του stamina
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
    
    [PunRPC]
    public void SetStaminaUI() // Ορίζουμε ποια μπάρα health και stamina είναι η δική μας
    {
        if (photonView.IsMine)
        {
            clientStaminaUI = GameObject.Find("GUI/Canvas/Right/Stamina/Bar").GetComponent<Image>();
        }
        else
        {
            clientStaminaUI = GameObject.Find("GUI/Canvas/Left/Stamina/Bar").GetComponent<Image>();
        }
    }
}
