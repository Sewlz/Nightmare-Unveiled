using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
            animator.SetBool("IsWalking", true);
        }
        if (!Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
        {
            animator.SetBool("IsWalking", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) // changed from GetButtonDown to GetKeyDown
        {
            animator.SetBool("IsSprinting", true);
        }
        if (!Input.GetKey(KeyCode.LeftShift)) // changed from GetButton to GetKey
        {
            animator.SetBool("IsSprinting", false);
        }
    }
}
