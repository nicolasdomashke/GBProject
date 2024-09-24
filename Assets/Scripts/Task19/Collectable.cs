using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Frog")
        {
            GameData.score++;
            Destroy(this.gameObject);
        }
    }
}
