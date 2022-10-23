using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolAnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkHash, isRunHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkHash = Animator.StringToHash("isWalk");
        isRunHash = Animator.StringToHash("isRun");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalk = animator.GetBool(isWalkHash);
        bool forwardPress = Input.GetKey(KeyCode.W);
        bool isRun = animator.GetBool(isRunHash);
        bool runPress = Input.GetKey(KeyCode.LeftShift);
        if (!isWalk && forwardPress)
        {
            animator.SetBool(isWalkHash, true);
        }
        if (isWalk && !forwardPress)
        {
            animator.SetBool("isWalk", false);
        }
        if (!isRun && (forwardPress && runPress))
        {
            animator.SetBool(isRunHash, true);
        }
        if (isRun && (!forwardPress || !runPress))
        {
            animator.SetBool(isRunHash, false);
        }
    }
}
