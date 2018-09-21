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

    [Header("Jumping Parameters")]
    [SerializeField]
    private float jumpForce = 5f;
    
    private float currMovementSpd;
    private float lastTapDuration = 0f;
    public enum MovementDirection { NEUTRAL, LEFT, RIGHT };
    private MovementDirection currMovementDir = MovementDirection.NEUTRAL;

    private void Start()
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
        if (dir != currMovementDir)
        {
            if(lastTapDuration < jumpTapMaxDelay)
            {
                Jump();
                return;
            }

            currMovementSpd += movementSpdIncreaseAmt;
            if (currMovementSpd > 1)
                currMovementSpd = 1;
        }
        else
        {
            currMovementSpd = 0;
            //Trip
        }
        lastTapDuration = 0;
        currMovementDir = dir;
    }

    private void Update()
    {
        lastTapDuration += Time.deltaTime;
        currMovementSpd -= movementSpdDecay * Time.deltaTime;

        if (currMovementSpd < 0 || lastTapDuration > minTapInterval)
            currMovementSpd = 0;

        Move();
    }

    private void Move()
    {
        m_RigidBody2D.velocity = new Vector2(currMovementSpd * Time.deltaTime * maxMoveSpd, m_RigidBody2D.velocity.y);
    }

    [ContextMenu("Jump")]
    private void Jump()
    {
        m_RigidBody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public float GetCurrentSpeed()
    {
        return currMovementSpd;
    }
}
