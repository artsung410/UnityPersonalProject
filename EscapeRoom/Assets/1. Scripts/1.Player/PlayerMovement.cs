using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float  mMoveSpeed;
    [SerializeField] private float  mGravity;
    private Vector3                 moveForce;
    private CharacterController     controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void MoveTo(Vector3 direction)
    {       
        if (!controller.isGrounded)
        {
            moveForce.y += mGravity * Time.deltaTime;
        }

        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        moveForce = new Vector3(direction.x * mMoveSpeed, moveForce.y, direction.z * mMoveSpeed);
        controller.Move(moveForce * Time.deltaTime);
    }
}
