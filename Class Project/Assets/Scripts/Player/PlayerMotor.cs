using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded = true;
    private bool lerpCrouch;
    private bool crouching;
    private bool sprinting;
    private float crouchTimer;
    private float speed;

    public float sprintSpeed = 18;
    public float walkSpeed = 14;
    public float gravity = -20f;
    public float jumphight = 10f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if(lerpCrouch)
        {
            speed = walkSpeed;
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height,6,p);
            else 
                controller.height = Mathf.Lerp(controller.height,10,p);
            
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    //receive input from our InputManager and apply tham to our charactor controller
    public void ProcessMove(Vector2 inupt) 
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = inupt.x;
        moveDirection.z = inupt.y;
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime; 
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumphight * -3f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0f;
        lerpCrouch = true;
    }

    public void Sprint()
    {
       
        sprinting = !sprinting;
        if(sprinting)
            speed = sprintSpeed;
        else
            speed = walkSpeed;
        
    }
}
