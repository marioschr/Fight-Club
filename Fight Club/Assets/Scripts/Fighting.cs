using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : MonoBehaviour
{
    private Animator animator;
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left1 = Animator.StringToHash("Left1");
    private static readonly int Right1 = Animator.StringToHash("Right1");
    private static readonly int Block = Animator.StringToHash("Block");
    private float damage = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetBool(Left1) && !animator.GetBool(Right1))
        {
            damage = 10;
            Debug.Log("Left from fighting");
            animator.SetBool(Block, false);
            animator.SetTrigger(Left);
        } else if (Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool(Left1) && !animator.GetBool(Right1))
        {
            damage = 5;
            animator.SetBool(Block, false);
            Debug.Log("Right from fighting");
            animator.SetTrigger(Right);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !animator.GetBool(Left1) && !animator.GetBool(Right1) && !animator.GetBool(Block))
        {
            Debug.Log("Started Blocking from fighting");
            animator.SetBool(Block, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !animator.GetBool(Left1) && !animator.GetBool(Right1) && animator.GetBool(Block))
        {
            Debug.Log("Stopped Blocking from fighting");
            animator.SetBool(Block, false);
        }
    }
}
