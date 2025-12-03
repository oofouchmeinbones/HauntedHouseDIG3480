using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinorMod_PlayerMovement : MonoBehaviour
{
    Animator m_Animator;

    public InputAction MoveAction;
    //public InputAction SpeedAction;

    public float walkSpeed = 1.0f;
    public float turnSpeed = 20f;
    private float animatorStartSpeed;
    private float animatorShiftSpeed;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
        animatorStartSpeed = m_Animator.speed;
        animatorShiftSpeed = m_Animator.speed * 1.75f;
        //SpeedAction.Enable();
    }

    void Update()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        //var shiftDown = SpeedAction.ReadValue
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log("Shift was pressed");
            walkSpeed = 1.5f;
            turnSpeed = 25f;
            m_Animator.speed = animatorShiftSpeed;
        } else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            walkSpeed = 1.0f;
            turnSpeed = 20f;
            m_Animator.speed = animatorStartSpeed;
        }

        
        float horizontal = pos.x;
        float vertical = pos.y;
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
       

       

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        m_Rigidbody.MoveRotation(m_Rotation);
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * walkSpeed * Time.deltaTime);
    }
}