using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesSystem;
    private void OnDisable() 
    {
        particlesSystem.Play();
    }
}
