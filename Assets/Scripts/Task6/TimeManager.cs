using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private  RectTransform cursor;
    [SerializeField] private  RectTransform redZone;
    [SerializeField] private  RectTransform greenZone;
    [SerializeField] private  Button actionButton;
    [SerializeField] private  Text actionButtonText;
    [SerializeField] private  GameObject currentScreen;
    [SerializeField] private  GameObject victoryScreen;
    [SerializeField] private  Text timeGame;
    [SerializeField] private  Text bestTimeGame;
    [SerializeField] private  Text timeVictory;
    [SerializeField] private  Text bestTimeVictory;
    [SerializeField] private  GameObject errorCursor;
    private RectTransform errorCursorRectTransform;
    private Image errorCursorImage;
    [SerializeField] private const float speed = 1000f;
    private bool isGameOn = false;
    private int direction = 1;
    private int level = 1;
    private float xPosition = -750f;
    [SerializeField] private const float buttonCD = 1f;
    private float startCD = -buttonCD;
    private float roundStartTime = 0f;

    void Start()
    {
        errorCursorRectTransform = errorCursor.GetComponent<RectTransform>();
        errorCursorImage = errorCursor.GetComponent<Image>();
    }
    void Update()
    {
        if (Time.time - startCD >= buttonCD && actionButton != null)
        {
            actionButton.interactable = true;
            if (actionButtonText != null)
            {
                actionButtonText.text = "Тык!";
            }
            errorCursor.SetActive(false);
        }
        else if (actionButtonText != null)
        {
            string newText = (buttonCD + startCD - Time.time).ToString();
            if (newText.Length > 2)
            {
                actionButtonText.text = newText.Substring(0, 3);
            }
            errorCursorImage.color = new Color(1, 0, 0.4f, 1 + (startCD - Time.time) / buttonCD);
        }
        if (isGameOn && cursor != null && redZone != null)
        {
            xPosition += direction * speed * (redZone.rect.width / 1920f) * level * Time.deltaTime;
            if (Mathf.Abs(xPosition) * 2 >=  redZone.rect.width) {
                direction *= -1;
            }
            xPosition = Mathf.Clamp(xPosition, -redZone.rect.width / 2, redZone.rect.width / 2);

            cursor.anchoredPosition = new Vector2(xPosition, cursor.anchoredPosition.y);
            if (timeGame != null)
            {
                timeGame.text = Mathf.Round(Time.time - roundStartTime).ToString();
            }
        }
    }
    public void OnActionButtonClick()
    {
        if (cursor != null && redZone != null && greenZone != null && actionButton != null && actionButton.interactable) 
        {
            if (Mathf.Abs(xPosition) * 2 <= greenZone.rect.width)
            {
                xPosition = -redZone.rect.width / 2;
                direction = 1;
                cursor.anchoredPosition = new Vector2(xPosition, cursor.anchoredPosition.y);
                if (level++ == 4)
                {
                    SetGameState(false);
                }
            }
            else if (actionButton != null)
            {
                actionButton.interactable = false;
                startCD = Time.time;
                errorCursor.SetActive(true);
                errorCursorRectTransform.anchoredPosition = cursor.anchoredPosition;
            }
        }
    }
    public void SetGameState(bool state)
    {
        isGameOn = state;
        if (state)
        {
            xPosition = -redZone.rect.width / 2;
            direction = 1;
            cursor.anchoredPosition = new Vector2(xPosition, cursor.anchoredPosition.y);
            roundStartTime = Time.time;
        }
        else
        {
            level = 1;
            timeVictory.text = timeGame.text;
            if (bestTimeVictory.text == "--:--:--" || Convert.ToInt32(bestTimeVictory.text) > Convert.ToInt32(timeGame.text))
            {
                bestTimeVictory.text = timeGame.text;
                bestTimeGame.text = timeGame.text;
            }
            victoryScreen.SetActive(true);
            currentScreen.SetActive(false);
        }
    }
}
