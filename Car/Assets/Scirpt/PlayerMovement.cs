using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform player;

    public float speed = 4f;
    public float gravity = -10;
    public float jumpHeight = 0.7f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool doubleJump;
    void Update()
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
        //Shift
        if (Input.GetKeyDown(KeyCode.LeftShift)) speed -= 2f;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) speed += 2f;
        //Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl)) speed += 2f;
        else if (Input.GetKeyUp(KeyCode.LeftControl)) speed -= 2f;

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
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);  
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
            if (bonus.Type == BonusType.Speed)
            {
                bonus.PickUp();
                StartCoroutine(BoostSpeed());
            }
            if (bonus.Type == BonusType.JumpBaff)
            {
                bonus.PickUp();
                StartCoroutine(JumpBaff());
            }
            if (bonus.Type == BonusType.HealthKit)
            {
                StartCoroutine(BoostSpeed());
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
    IEnumerator HealthKit()
    {
        for(int i=0;i<=10;i++)
        {
            yield return new WaitForSeconds(1.0f);
           
        }
    }
}
