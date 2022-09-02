using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 moveForce;
    public float gravity;

    private CharacterController controller;

    public float jumpForce; // 점프 힘

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void MoveTo(Vector3 direction)
    {       
        // 허공에 떠있으면 중력만큼 y축 이동속도 감소
        if (!controller.isGrounded)
        {
            moveForce.y += gravity * Time.deltaTime;
        }

        // 이동 방향 = 캐릭터 회전 값 * 방향 값 ( 카메라 회전으로 전방 방향이 변하기 때문에 회전값을 곱해서 연산해야한다.)
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // 이동 힘 = 이동방향 * 속도 ( 위나 아래를 바라보고 이동할 경우 캐릭터가 공중으로 뜨거나 아래로 가라 앉으려 하기 때문에 direction을 그대로 사용하지 않고, moveForce변수에 x, z값만 넣어서 사용
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);

        controller.Move(moveForce * Time.deltaTime);
    }
}
