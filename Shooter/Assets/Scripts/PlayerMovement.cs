using UnityEngine;
using System.Collections ;
using System;
public class PlayerMovement : MonoBehaviour
{   
    public CharacterController controller ;

    public Transform groundCheck ;
    public float groundDistance ;
    public LayerMask groundMask ;

    public float moveSpeed ;
    public float sprintSpeed ;

    public float gravity  = -9.8f ;
    public float jumpHeight = 3f;

    float horizontalMovement ;
    float verticalMovement ;
    
    Vector3 velocity ;
    bool isGrounded ;
    float playerSpeed ;

    public Gun gun ;

    public PlayerHealth health ;

    void Update()
    {
        if (!health.died)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -10f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded && !gun.reloading)
            {
                playerSpeed = sprintSpeed;
            }
            else playerSpeed = moveSpeed;

            PlyInput();
            Movement();
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void PlyInput() {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    void Movement(){
        Vector3 moveVelocity ;
        moveVelocity = transform.right*horizontalMovement +transform.forward*verticalMovement ;
        controller.Move(moveVelocity*playerSpeed*10f*Time.deltaTime);
        velocity.y += gravity * Time.deltaTime ;
        controller.Move(velocity*Time.deltaTime);
       
    }
}
