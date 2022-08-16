using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveForce;

    public float jumpForce; // 점프 힘
    public float gravity; // 중력 계수

    public void MoveTo(Vector3 direction)
    {
        transform.Translate(new Vector3(direction.x * moveSpeed, 0, direction.z * moveSpeed) * Time.deltaTime);
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);
    }
}
