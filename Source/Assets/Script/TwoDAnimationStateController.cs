using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDAnimationStateController : MonoBehaviour
{
    Animator animator;
  
    private float velocityX = 0.0f, velocityZ = 0.0f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;
    private int velocityXHash, velocityZHash;

    private void Start()
    {
        animator = GetComponent<Animator>();
       
        velocityXHash = Animator.StringToHash("Velo X");
        velocityZHash = Animator.StringToHash("Velo Z");
    }

    void changeVelocity(bool forwardPress, bool backwardPress, bool leftPress, bool rightPress, bool runPress, float currentMaxVelocity)
    {
        // increase velocity
        if (forwardPress && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (backwardPress && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        if (leftPress && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (rightPress && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease velocity
        if (!forwardPress && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (!backwardPress && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        if (!leftPress && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!rightPress && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lookOrResetVelocity(bool forwardPress, bool backwardPress, bool leftPress, bool rightPress, bool runPress, float currentMaxVelocity)
    {
        // reset velocity Z
        if (!forwardPress && !backwardPress && velocityZ != 0.0f && (velocityZ > -0.05 && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        // reset velocity X
        if (!leftPress && !rightPress && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        // lock forward
        if (forwardPress && runPress && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        // deceleration to the max walk velocity 
        else if (forwardPress && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        // round to the currentMaxVelocity if within offset
        else if (forwardPress && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05))
        {
            velocityZ = currentMaxVelocity;
        }

        // lock backward
        if (backwardPress && runPress && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        // deceleration to the max walk velocity 
        else if (backwardPress && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
        }

        // lock left
        if (leftPress && runPress && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        // deceleration to the max walk velocity 
        else if (leftPress && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        // round to the currentMaxVelocity if within offset
        else if (leftPress && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05))
        {
            velocityX = -currentMaxVelocity;
        }

        // lock right
        if (rightPress && runPress && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        // deceleration to the max walk velocity 
        else if (forwardPress && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05))
            {
                velocityX = currentMaxVelocity;
            }
        }
        // round to the currentMaxVelocity if within offset
        else if (forwardPress && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05))
        {
            velocityX = currentMaxVelocity;
        }
    }

    private void Update()
    {
        bool forwardPress = Input.GetKey(KeyCode.W);
        bool backwardPress = Input.GetKey(KeyCode.S);
        bool leftPress = Input.GetKey(KeyCode.A);
        bool rightPress = Input.GetKey(KeyCode.D);
        bool runPress = Input.GetKey(KeyCode.LeftShift);

        // set current maxVelocity
        float currentMaxVelocity = runPress ? maxRunVelocity : maxWalkVelocity;

        changeVelocity(forwardPress, backwardPress, leftPress, rightPress, runPress, currentMaxVelocity);
        lookOrResetVelocity(forwardPress, backwardPress, leftPress, rightPress, runPress, currentMaxVelocity);

        animator.SetFloat(velocityZHash, velocityZ);
        animator.SetFloat(velocityXHash, velocityX);
    }

}
