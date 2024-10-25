using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject winText;
    private void OnTriggerEnter(Collider other) {
        ball.SetActive(false);
        winText.SetActive(true);
    }
}
