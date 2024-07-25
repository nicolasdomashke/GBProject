using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    [SerializeField] private float delay = 3f;
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionPower = 5f;
    private Rigidbody rb;
    [SerializeField] private Material mat;
    private Rigidbody [] blocks;
    private bool isExplosionInitiated = false;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        blocks = FindObjectsOfType<Rigidbody>();

    }

    void Update()
    {
        mat.color = Color.red * (Time.time - delay) / explosionDelay;
        if (Time.time >= delay && !isExplosionInitiated)
        {
            rb.isKinematic = false;
            rb.AddForce(-200, 200, 0, ForceMode.Impulse);
            Debug.Log("fire!");
            isExplosionInitiated = true;
        }
        else if (Time.time >= delay + explosionDelay)
        {
            foreach (Rigidbody blockRb in blocks)
            {
                Vector3 direction = blockRb.transform.position - transform.position;
                if (direction.magnitude <= explosionRadius)
                {
                    blockRb.AddForce(direction.normalized * (explosionPower - direction.magnitude), ForceMode.Impulse);
                }
            }
            Debug.Log("explosion!");
            mat.color = Color.black;
            this.gameObject.SetActive(false);
        }
    }
}
