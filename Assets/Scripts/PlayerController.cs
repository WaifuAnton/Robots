using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    float jump = 10;

    Rigidbody2D rb2d;
    Animator animator;
    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        animator.SetBool("Grounded", rb2d.velocity.y == 0);
        if (rb2d.velocity.x > 0 && !facingRight || rb2d.velocity.x < 0 && facingRight)
            Flip();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 velocity = context.ReadValue<Vector2>();
            velocity.x *= speed;
            rb2d.AddForce(velocity, ForceMode2D.Impulse);
        }
        else
        {
            Vector2 velocityTemp = rb2d.velocity;
            velocityTemp.x = 0;
            rb2d.velocity = velocityTemp;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && rb2d.velocity.y == 0)
            rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
