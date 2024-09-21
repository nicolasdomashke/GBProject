using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullRunValidator : MonoBehaviour
{
    void Awake()
    {
        GameData.isFullRun = true;
        GameData.startTime = Time.time;
    }
}
