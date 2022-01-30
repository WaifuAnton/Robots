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
    Vector2 startPosition;
    bool facingRight = true;
    float horizontal = 0;
    float timeToFinishGame = 5;
    float currentTimeToFinishGame = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        animator.SetBool("Grounded", IsGrounded());
        if (rb2d.velocity.x > 0 && !facingRight || rb2d.velocity.x < 0 && facingRight)
            Flip();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2)
            animator.SetBool("Jumped", false);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        animator.SetBool("Jumped", true);
    }

    public void AddForce()
    {
        rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    bool IsGrounded()
    {
        BoxCollider2D bodyCollider = transform.GetChild(2).GetComponent<BoxCollider2D>();
        float distance = 0.7f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bodyCollider.bounds.center, bodyCollider.bounds.size, 0, Vector2.down, distance);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            transform.position = startPosition;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Finish")
            return;
        currentTimeToFinishGame += Time.deltaTime;
        if (currentTimeToFinishGame >= timeToFinishGame)
            Application.Quit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
            currentTimeToFinishGame = 0;
    }
}
