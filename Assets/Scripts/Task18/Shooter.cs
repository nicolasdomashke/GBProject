using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunPos;
    [SerializeField] private float fireSpeed;

    public void Shoot(float direction)
    {
        GameObject currentBullet = Instantiate(bullet, gunPos.position, Quaternion.identity);
        Rigidbody2D currentBulletRB = currentBullet.GetComponent<Rigidbody2D>();

        currentBulletRB.velocity = new Vector2(direction * fireSpeed, 0f);
    }
}
