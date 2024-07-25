using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOffset : MonoBehaviour
{
    [SerializeField] private float randomRange = 2f;
    private Animator anim;
    private float  generatedOffset = 0f;
    void Awake()
    {
        anim = GetComponent<Animator>();
        generatedOffset = Random.value * randomRange;
    }

    void Update()
    {
        generatedOffset -= Time.deltaTime;
        if (generatedOffset < 0)
        {
            anim.SetTrigger("Start");
            this.enabled = false;
        }
    }
}
