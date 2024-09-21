using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    [SerializeField] Text localTimer;
    [SerializeField] Text globalTimer;
    [SerializeField] GameObject globalTimerObj;
    void Start()
    {
        globalTimerObj.SetActive(GameData.isFullRun);
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            GameData.isFullRun = false;
            SceneManager.LoadScene(5);
        }
        localTimer.text = TimerOutput(Time.timeSinceLevelLoad);
        if (GameData.isFullRun)
        {
            globalTimer.text = TimerOutput(Time.time - GameData.startTime);
        }
    }
    private string TimerOutput(float timeInput)
    {
        int centies = (int) (timeInput * 100);
        if (centies >= 360000)
        {
            centies = 359999;
        }
        int minutes = centies / 6000;
        int seconds = (centies % 6000) / 100;
        centies = centies % 100;
        return (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds) + ":" + (centies < 10 ? "0" + centies : centies);
    }
}
