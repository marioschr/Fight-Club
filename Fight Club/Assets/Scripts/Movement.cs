using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviourPunCallbacks // script για την κίνηση του χαρακτήρα
{
    public Transform opponent = null;
    private Transform cam;
    private Rigidbody rig;
    private Animator animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Z = Animator.StringToHash("Z");
    private bool foundOpponent = false;
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        if (!photonView.IsMine)
        {
            gameObject.layer = 10;
        }
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        FindOpponent();
    }

    private void Update()
    { 
        if (!foundOpponent) FindOpponent();
        if (!photonView.IsMine) return;
        transform.LookAt(opponent);
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");

        animator.SetFloat(Z, t_hmove, 0.05f, Time.deltaTime);
        animator.SetFloat(X, -t_vmove, 0.05f, Time.deltaTime);
        Vector3 direction = new Vector3(t_hmove, 0f, t_vmove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            rig.MovePosition(transform.position + moveDir.normalized * Time.deltaTime);
        }
    }

    private void FindOpponent() // βρίσκουμε τον αντίπαλο για να κοιτάμε προς την κατεύθυνση του
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 2)
        {
            foreach (GameObject player in players)
            {
                if (!player.GetPhotonView().IsMine)
                {
                    opponent = player.GetPhotonView().transform;
                    foundOpponent = true;
                }
            }
        }
    }
    
    
}
