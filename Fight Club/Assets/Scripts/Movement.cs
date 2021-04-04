using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviourPunCallbacks // script για την κίνηση του χαρακτήρα
{
    private Transform cam;
    private Rigidbody rig;
    private Animator animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Z = Animator.StringToHash("Z");
    private static readonly int Block = Animator.StringToHash("Block");
    private static readonly int SkipForward = Animator.StringToHash("SkipForward");
    private static readonly int SkipBack = Animator.StringToHash("SkipBack");

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        if (!photonView.IsMine)
        {
            gameObject.layer = 10;
        }
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");

        if (animator.GetBool(Block)) return;
        animator.SetFloat(Z, t_hmove, 0.05f, Time.deltaTime);
        animator.SetFloat(X, -t_vmove, 0.05f, Time.deltaTime);
        Vector3 direction = new Vector3(t_hmove, 0f, t_vmove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            rig.MovePosition(transform.position + moveDir.normalized * (.1f * Time.deltaTime));
        }

        if (t_hmove < -0.2f && Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Skip", RpcTarget.All, SkipForward);
        }
        if (t_hmove > 0.2f && Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Skip", RpcTarget.All, SkipBack);
        }
    }

    [PunRPC]
    void Skip(int direction)
    {
        animator.SetTrigger(direction);
    }
}
