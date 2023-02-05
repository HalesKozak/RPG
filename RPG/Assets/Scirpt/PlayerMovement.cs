using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform player;
    Animator animator;

    public float speed = 3;
    public float gravity = -10;
    public float jumpHeight = 0.7f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool doubleJump;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        {
            if (Input.GetKey(KeyCode.W))
            {
                if(speed<3) animator.SetFloat("Side",1);
                else if(speed<6) animator.SetFloat("Side",2);
                else animator.SetFloat("Side",3);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                if (speed<3)animator.SetFloat("Side", 8);
                else animator.SetFloat("Side", 9);
            }
            if(Input.GetKey(KeyCode.A))
            {
                if (speed <3) animator.SetFloat("Side", 4);
                else animator.SetFloat("Side", 7);
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (speed <3) animator.SetFloat("Side", 6);
                else animator.SetFloat("Side", 5);
            }
        }
        else 
        { 
            animator.SetFloat("Side",10);
        }
        //Shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed -= 1f;       
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed += 1f;
        }
        //Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speed += 1f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed -= 1f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || doubleJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                doubleJump = !doubleJump;
            }
            else if (doubleJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                doubleJump = false;
            }
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bonus>(out var bonus)==true)
        {
            if(bonus.Type== BonusType.Slow)
            {
                bonus.PickUp();
                StartCoroutine(SlowSpeed());
            }
            else if(bonus.Type == BonusType.Speed)
            {
                bonus.PickUp();
                StartCoroutine(BoostSpeed());
            }
            else if(bonus.Type == BonusType.JumpBaff)
            {
                bonus.PickUp();
                StartCoroutine(JumpBaff());
            }
            else if(bonus.Type == BonusType.HealthKit)
            {
                bonus.PickUp();
                TryGetComponent<ProgressBar>(out var HP);
            }
            else if (bonus.Type == BonusType.JumpDebaff)
            {
                bonus.PickUp();
                StartCoroutine(JumpDebaff());
            }

        }
    }
    IEnumerator SlowSpeed()
    {
        speed -= 1f;
        yield return new WaitForSeconds(5.0f);
           speed += 1.0f;
    }
    IEnumerator BoostSpeed()
    {
        speed += 3.0f;
        yield return new WaitForSeconds(5.0f);
        speed -= 3.0f;
    }
    IEnumerator JumpBaff()
    {
        jumpHeight += 0.4f;
        yield return new WaitForSeconds(5.0f);
        jumpHeight -= 0.4f;
    }
    IEnumerator JumpDebaff()
    {
        jumpHeight -= 0.4f;
        yield return new WaitForSeconds(5.0f);
        jumpHeight += 0.4f;
    }
}
