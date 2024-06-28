using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehavior : MonoBehaviour
{
    private Image img;
    private float delay = 0f;
    private float delayMax;
    public bool isIdle = true;
    void Awake()
    {
        img = GetComponent<Image>();
        img.enabled = false;
    }

    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            img.fillAmount = delay / delayMax;
        }
        else
        {
            delay = 0f;
            isIdle = true;
            img.enabled = false;
        }
    }
    public void SetDelay(float delay)
    {
        if (delay > 0)
        {
            this.delay = delay;
            delayMax = delay;
            isIdle = false;
            img.enabled = true;
        }
    }
}
