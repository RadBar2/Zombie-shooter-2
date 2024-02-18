using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class Move : MonoBehaviour
{
    public LayerMask groundMask;
    public Transform feet;
    public float speed = 7;
    public float jumpHeight = 2;
    public float gravity = -9.8f;

    private CharacterController controller;
    private bool isGrounded;
    private float y;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(feet.position, 0.4f, groundMask);

        //cap gravity
        if(isGrounded && y < 0)
        {
            y = 0;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        //move according to input
        var direction = transform.right * x + transform.forward * z;
        var move = direction * speed * Time.deltaTime;

        //jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            y = Mathf.Sqrt(jumpHeight * -2 * gravity) * Time.deltaTime;
        }

        //add gravity
        y += gravity * Time.deltaTime * Time.deltaTime;
        move.y = y;

        controller.Move(move);
    }

    //visualize ground check
    private void OnDrawGizmos()
    {
        if(feet == null) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(feet.position, 0.4f);
    }
}
