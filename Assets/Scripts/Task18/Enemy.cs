using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Shooter shooter;
    void Awake()
    {
        shooter = GetComponent<Shooter>();
        StartCoroutine(EnemyStatic());
    }

    private IEnumerator EnemyStatic()
    {
        for (;;)
        {
            yield return new WaitForSeconds(3);
            shooter.Shoot(-1f);
        }
    }
}
