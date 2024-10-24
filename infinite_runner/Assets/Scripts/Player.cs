using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float gravity;
    [SerializeField] float jumpVelocity;
    [SerializeField] float groundHeight = -5;
    [SerializeField] const float maxHoldJumpTime = 0.4f;
    [SerializeField] float jumpGroundThreshold = 3;

    Vector2 velocity;
    bool isGrounded;
    bool isHoldingJump;
    float holdJumpTimer;

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y = groundHeight);

        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0f;
            }
        }   

        if(Input.GetKeyUp(KeyCode.Space)) 
        { 
            isHoldingJump = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if(!isGrounded)
        {
            if(isHoldingJump) 
            {
                holdJumpTimer += Time.fixedDeltaTime;

                if(holdJumpTimer > maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;

            if(!isHoldingJump) 
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
        
            if(pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }
}
