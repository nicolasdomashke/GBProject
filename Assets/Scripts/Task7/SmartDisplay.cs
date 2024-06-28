using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SmartDisplay : MonoBehaviour
{
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;
    [SerializeField] private float showTime = 3f;
    [SerializeField] private float decay = 1f;
    private Text mainText;
    private Text secondaryText;
    private int value = 0;
    private float delay = 0;

    void Awake()
    {
        Text [] texts = GetComponentsInChildren<Text>();
        mainText = texts[0];
        secondaryText = texts[1];
        mainText.text = "0";
        secondaryText.color = Color.clear;
    }

    void Update()
    {
        if (delay > 0)
        {
            secondaryText.color = new Color(secondaryText.color.r, secondaryText.color.g, secondaryText.color.b, delay / decay);
            delay -= Time.deltaTime;
        }
        else
        {
            delay = 0;
            secondaryText.color = Color.clear;
        }
    }
    public void SetValue(int value, bool showAnim=true)
    {
        mainText.text = value.ToString();
        if (showAnim)
        {
            int difference = value - this.value;
            if (difference < 0)
            {
                secondaryText.color = negativeColor;
                secondaryText.text = difference.ToString();
            }
            else
            {
                secondaryText.color = positiveColor;
                secondaryText.text = "+" + difference.ToString();
            }
            delay = showTime;
        }
        this.value = value;
    }
}
