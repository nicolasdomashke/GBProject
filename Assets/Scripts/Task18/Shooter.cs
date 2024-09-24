using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector2 gunPos = new Vector2(.475f, .406f);
    [SerializeField] private float fireSpeed;

    public void Shoot(bool direction)
    {
        GameObject currentBullet = Instantiate(bullet, new Vector2( transform.position.x + (direction ? gunPos.x : -gunPos.x), transform.position.y + gunPos.y), Quaternion.identity);
        Rigidbody2D currentBulletRB = currentBullet.GetComponent<Rigidbody2D>();

        currentBulletRB.velocity = new Vector2(direction ? fireSpeed : -fireSpeed, 0f);
    }
}
