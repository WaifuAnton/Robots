using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    BulletMovement bulletPrefab;

    [SerializeField]
    float fireRate = 5;

    Transform firePoint;
    float currentFireRateTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = transform.GetChild(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFireRateTime >= fireRate)
        {
            Vector2 direction = Vector2.zero;
            if (transform.rotation.y < 0)
                direction.x = -1;
            else
                direction.x = 1;
            BulletMovement currentBullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
            //currentBullet.SetDirection(direction);
            currentFireRateTime = 0;
        }
        else
            currentFireRateTime += Time.deltaTime;
    }
}
