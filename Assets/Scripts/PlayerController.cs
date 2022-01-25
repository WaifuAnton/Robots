using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            rb2d.velocity = Vector2.zero;
    }
}
