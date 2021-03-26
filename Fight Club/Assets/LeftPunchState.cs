﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunchState : StateMachineBehaviour
{
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left1 = Animator.StringToHash("Left1");
    private static readonly int Right1 = Animator.StringToHash("Right1");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Left1,true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool(Right1))
        {
            Debug.Log("Right from LeftState");
            animator.SetTrigger(Right);
            animator.SetBool(Left1,true);
            animator.SetBool(Right1,true);
        }
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Left1,false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
