using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameData
{
    public static int score = 0;
}
public class Finish : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text inGameScore;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject frog;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Frog")
        {
            frog.SetActive(false);
            image.SetActive(true);
            text.text = "Твой счёт: " + GameData.score;
        }
    }

    private void Update() {
        inGameScore.text = "Счет: " + GameData.score; 
    }
    
}
