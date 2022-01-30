using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    Vector2 movement = Vector2.right;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
