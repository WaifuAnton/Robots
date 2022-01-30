using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float maxLifeTime = 500;
    float currentLifeTime = 0;
    Vector2 movement = Vector2.right;

    // Update is called once per frame
    void Update()
    {
        if (currentLifeTime >= maxLifeTime)
            Destroy(gameObject);
        transform.Translate(movement * speed * Time.deltaTime);
        currentLifeTime += Time.deltaTime;
    }
}
