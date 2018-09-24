using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D m_RigidBody2D;

    [Header("Movement Parameters")]
    [SerializeField]
    private float maxMoveSpd = 200f;
    [SerializeField]
    private float movementSpdIncreaseAmt = 0.1f;
    [SerializeField]
    private float movementSpdDecay = 0.5f;
    [SerializeField]
    private float minTapInterval = 0.3f;
    [SerializeField]
    private float jumpTapMaxDelay = 0.1f;

    [SerializeField]
    private float currMovementSpd;
    private float lastTapDuration = 0f;
    public enum MovementDirection { NEUTRAL, LEFT, RIGHT };
    private MovementDirection currMovementDir = MovementDirection.NEUTRAL;

    [Header("Jumping Parameters")]
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private Transform groundChkOrigin;
    [SerializeField]
    private float groundChkRayLen = 0.1f;

    private float currMoveSpdInAir = 0f;
    

    private void Awake()
    {
        if(instance)
        {
            Debug.Log("Player Controller already exists, removing duplicate");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        m_RigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void OnMovementBtnClick(MovementDirection dir)
    {
        //New From Zhen Xiong
        //Increase speed no matter what, if trip will stop, if jump will still add force.
        currMovementSpd += movementSpdIncreaseAmt;
        if (currMovementSpd > 1)
            currMovementSpd = 1;

        if (lastTapDuration < jumpTapMaxDelay)
        {
            if (IsOnGround())
            {
                Jump();
                return;
            }
        }

        if (dir == currMovementDir && IsOnGround())
        {
            print("Trip");
            currMovementSpd = 0;
        }

        //Zhi Heng ---- Your Code
        //if (dir != currMovementDir)
        //{
        //    if (lastTapDuration < jumpTapMaxDelay && IsOnGround())
        //    {
        //        Jump();
        //        return;
        //    }

        //    currMovementSpd += movementSpdIncreaseAmt;
        //    if (currMovementSpd > 1)
        //        currMovementSpd = 1;
        //}
        //else
        //{
        //    currMovementSpd = 0;
        //    //Trip
        //}
        lastTapDuration = 0;
        currMovementDir = dir;
    }

    private void Update()
    {
        lastTapDuration += Time.deltaTime;

        if(IsOnGround())
        currMovementSpd -= movementSpdDecay * Time.deltaTime;
        
        if (currMovementSpd < 0)
            currMovementSpd = 0;

        Move();
    }

    private void Move()
    {
        float moveSpd = 0;

        if (IsOnGround())
            moveSpd = currMovementSpd;
        else
            moveSpd = currMoveSpdInAir;

        m_RigidBody2D.velocity = new Vector2(moveSpd * Time.deltaTime * maxMoveSpd, m_RigidBody2D.velocity.y);
    }
    
    private void Jump()
    {
        m_RigidBody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        currMoveSpdInAir = currMovementSpd;

        currMovementDir = MovementDirection.NEUTRAL;
    }

    private bool IsOnGround()
    {
        Debug.DrawLine(groundChkOrigin.position, groundChkOrigin.position - transform.up * groundChkRayLen, Color.red);

        RaycastHit2D hitInfo;
        int layersToHit = LayerMask.GetMask("Ground");
        if (Physics2D.Raycast(groundChkOrigin.position, -transform.up, groundChkRayLen, layersToHit))
        {
            Debug.Log("OnGround");
            return true;
        }
        Debug.Log("Not OnGround");
        return false;
    }

    public float GetCurrentSpeed()
    {
        return currMovementSpd;
    }
    public float GetMaxSpeed()
    {
        return maxMoveSpd;
    }
}
