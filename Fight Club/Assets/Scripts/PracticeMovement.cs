using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeMovement : MonoBehaviour
{
    public Transform opponent = null;
    private Transform cam;
    private Rigidbody rig;
    private Animator animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Z = Animator.StringToHash("Z");
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").transform;
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
