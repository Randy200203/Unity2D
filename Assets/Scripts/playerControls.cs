using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedMove;
    public float jumpingPower;
    public SpriteRenderer spriteRnd;
    public Animator animPlayer;
    public Transform transformPlayer;
    public float waithShootTime;

    private float horizontal;
    private bool isFacingRight = true;
    private float lastShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMovement();
    }
    private void checkMovement()
    {
        
        if (Mathf.Abs(horizontal) != 0f)
        {
            animPlayer.SetBool("isRunning", true);
        }
        else
        {
            animPlayer.SetBool("isRunning", false);
        }

        if (CkeckGround.isGrounded)
        {
            rb.velocity = new Vector2(horizontal * speedMove, rb.velocity.y);
            animPlayer.SetBool("isGrounded", true);
        }
        else
        {
            animPlayer.SetBool("isGrounded", false);
        }
        

        if (!isFacingRight && horizontal > 0f)
        {
            //Girar a la derecha 
            isFacingRight = true;
            spriteRnd.flipX = false;
        }
        else if (isFacingRight && horizontal < 0f)
        {
            //Girar a la izquierda
            isFacingRight = false;
            spriteRnd.flipX = true;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
            horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump()
    {

        if (CkeckGround.isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && !animPlayer.GetBool("isRunning"))
        {
            animPlayer.SetBool("Attack", true);
        }else if (context.canceled)
        {
            animPlayer.SetBool("Attack", false);
        }
        
    }
}
