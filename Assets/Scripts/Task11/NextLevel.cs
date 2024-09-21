using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    void OnTriggerEnter()
    {
        string requestJson;
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        Time.timeScale = .25f;
        if (level == 5 && GameData.isFullRun)
        {
            GameData.isFullRun = false;
            requestJson = "{\"login\": \"" + GameData.currentUser + "\", \"time_new\": \"" + (int) ((Time.time - GameData.startTime) * 100) + "\"}";
            StartCoroutine(PostData(requestJson, 0));
        }
        requestJson = "{\"login\": \"" + GameData.currentUser + "\", \"time_new\": \"" + (int) (Time.timeSinceLevelLoad * 100) + "\"}";
        StartCoroutine(PostData(requestJson, level));
    }
    
    private IEnumerator PostData(string json, int level)
    {
        var request = new UnityWebRequest($"http://192.168.56.10:8000/insert/{level}", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (level != 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
