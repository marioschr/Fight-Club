using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Animator animator;
    public TMP_Text textbox;
    private int lesson = 1;
    private static readonly int Block = Animator.StringToHash("Block");
    private static readonly int KickRight = Animator.StringToHash("KickRight");
    private static readonly int KickLeft = Animator.StringToHash("KickLeft");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (lesson == 1 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger(Left);
            lesson = 2;
            textbox.text = "Lesson 2: Press \"Right Mouse Button\" for a Right Punch";
            return;
        }

        if (lesson == 2 && Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger(Right);
            lesson = 3;
            textbox.text = "Lesson 3: Press \"Shift\" and \"Left Mouse Button\" for a Left Kick";
            return;
        }
        if (lesson == 3 && Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetTrigger(KickLeft);
            lesson = 4;
            textbox.text = "Lesson 4: Press \"Left Shift\" and \"Right Mouse Button\" for a Right Kick";
            return;
        }
        if (lesson == 4 && Input.GetKeyDown(KeyCode.Mouse1) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetTrigger(KickRight);
            lesson = 5;
            textbox.text = "Lesson 5: Press \"Left CTRL\" to Block incoming attacks. As long as you hold it you will keep blocking," +
                           "but you will not be able to move";
            return;
        }
        if (lesson == 5  && Input.GetKeyDown(KeyCode.LeftControl) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, true);
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !animator.GetBool(Attacking) && animator.GetBool(Block))
        {
            animator.SetBool(Block, false);
            lesson = 6;
            return;
        }
        if (lesson == 6)
        {
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
            textbox.text = "Great Job! Now you can continue practicing your moves or go online and prove your worth! Good luck out there!";
            GetComponent<PracticeFighting>().enabled = true;
            GetComponent<PracticeMovement>().enabled = true;
            animator.applyRootMotion = true;
            GetComponent<Tutorial>().enabled = false;
        }
    }
}
