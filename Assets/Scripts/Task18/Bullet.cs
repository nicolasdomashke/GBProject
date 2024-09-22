using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Damageable"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
