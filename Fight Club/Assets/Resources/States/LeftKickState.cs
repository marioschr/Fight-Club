using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftKickState : StateMachineBehaviour
{
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left1 = Animator.StringToHash("Left1");
    private static readonly int Right1 = Animator.StringToHash("Right1");
    private static readonly int Block = Animator.StringToHash("Block");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    private static readonly int KickLeft1 = Animator.StringToHash("KickLeft1");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name,false);
            }
        }
        animator.SetBool(Attacking, true);
        animator.SetBool(KickLeft1,true);
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(Attacking, false);
        animator.SetBool(KickLeft1,false);
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
