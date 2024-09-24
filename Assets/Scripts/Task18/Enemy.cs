using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Shooter shooter;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        shooter = GetComponent<Shooter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(EnemyStatic());
    }

    private IEnumerator EnemyStatic()
    {
        yield return new WaitForSeconds(5);
        for (;;)
        {
            yield return new WaitForSeconds(3);
            shooter.Shoot(!spriteRenderer.flipX);
        }
    }
}
