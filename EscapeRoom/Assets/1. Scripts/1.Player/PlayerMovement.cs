using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float  _MoveSpeed;
    [SerializeField] private float  _Gravity;
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
            moveForce.y += _Gravity * Time.deltaTime;
        }

        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
        moveForce = new Vector3(direction.x * _MoveSpeed, moveForce.y, direction.z * _MoveSpeed);
        controller.Move(moveForce * Time.deltaTime);
    }
}
