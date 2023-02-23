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
    private bool isGrounded;

    public InventoryManager _inventoryManager;
    public QuickslotInventory _quickslotInventory;
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_quickslotInventory.activeSlot != null)
            {
                if (_quickslotInventory.activeSlot.item != null)
                {
                    if(_quickslotInventory.activeSlot.item.itemType == ItemType.Weapon)
                    {
                        if (_inventoryManager.isOpened == false)
                        {
                            //Hit 
                        }
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // hit = false
        }

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
            else if(Input.GetKey(KeyCode.A))
            {
                if (speed <3) animator.SetFloat("Side", 4);
                else animator.SetFloat("Side", 7);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (speed <3) animator.SetFloat("Side", 6);
                else animator.SetFloat("Side", 5);
            }
        }
        else animator.SetFloat("Side", 10);
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

        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                animator.SetBool("isJumping", true);
            }
        }
        else animator.SetBool("isJumping", false);
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    public void Jump()
    {
        animator.SetBool("isJumping", false);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}
