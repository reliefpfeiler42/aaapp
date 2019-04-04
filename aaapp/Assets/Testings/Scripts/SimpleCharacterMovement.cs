using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour
{
    public float speed = 6.0f;
	public float rotSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    
    private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // let the gameObject fall down
        gameObject.transform.position = new Vector3(0, 5, 0);
    }

    void Update()
    {

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

		rotation = new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f);
		rotation = rotation * rotSpeed;
		transform.Rotate(rotation);

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
