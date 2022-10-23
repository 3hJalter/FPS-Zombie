using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDBLendTreeAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int velocityHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        bool forwardPress = Input.GetKey(KeyCode.W);
        bool runPress = Input.GetKey(KeyCode.LeftShift);


        if (forwardPress && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }


        if (!forwardPress && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!forwardPress && velocity < 0.0f)
        {
            velocity = 0.0f;
        }

        animator.SetFloat(velocityHash, velocity);
    }

}
