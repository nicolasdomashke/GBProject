using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    private float localTimer = 0f;
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Тап!");
                localTimer = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended && Time.time - localTimer > 2f)
            {
                Debug.Log("Холд!");
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (touch.deltaPosition.x > 25f)
                {
                    Debug.Log("Свайп вправо!");
                }
                else if (touch.deltaPosition.x < -25)
                {
                    Debug.Log("Свайп влево!");
                }
            }
        }
        else if (Input.touchCount > 1)
        {
            Debug.Log($"Целых {Input.touchCount} касаний!");
        }
    }
}
