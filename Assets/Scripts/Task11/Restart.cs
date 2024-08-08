using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem deathParticles;
    private void OnTriggerEnter(Collider other) {
        RestartLevel();
    }

public void RestartLevel()
{
    StartCoroutine(reload());
}
    private IEnumerator reload()
    {
        player.SetActive(false);
        deathParticles.gameObject.transform.position = player.transform.position;
        deathParticles.Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
