using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombController : MonoBehaviour
{
    [SerializeField] private Restart restart;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float initiationRange;
    [SerializeField] private float explosionRange;
    [SerializeField] private float delay;
    private Transform radiusHitbox;

    bool idle = true;
    void Awake()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        radiusHitbox = transforms[2];
        radiusHitbox.gameObject.SetActive(false);
    }

    void Update()
    {
        if (idle)
        {
            Collider[] objects = Physics.OverlapSphere(transform.position, initiationRange, playerMask);
            if (objects.Length > 0)
            {
                idle = false;
                radiusHitbox.localScale = Vector3.one * explosionRange * 2f;
                radiusHitbox.gameObject.SetActive(true);
                StartCoroutine(explotionTimer());
            }
        }
    }
    private IEnumerator explotionTimer()
    {
        yield return new WaitForSeconds(delay);
        Collider[] objects = Physics.OverlapSphere(transform.position, explosionRange, playerMask);
        if (objects.Length > 0)
        {
            restart.RestartLevel();
        }
        objects = Physics.OverlapSphere(transform.position, explosionRange, wallMask);
        foreach (Collider obj in objects)
        {
            obj.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
