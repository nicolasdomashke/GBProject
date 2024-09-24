using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }

    public void ReloadLevel()
    {
        StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(.5f);
        GameData.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
