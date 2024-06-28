using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimerBehavior raidTimer;
    [SerializeField] private TimerBehavior incomeTimer;
    [SerializeField] private TimerBehavior consumeTimer;
    [SerializeField] private TimerBehavior peasantTimer;
    [SerializeField] private TimerBehavior warriorTimer;
    [SerializeField] private Button peasantButton;
    [SerializeField] private Button warriorButton;
    [SerializeField] private SmartDisplay wheatDisplay;
    [SerializeField] private SmartDisplay peasantDisplay;
    [SerializeField] private SmartDisplay warriorDisplay;
    [SerializeField] private GameObject gameOverScreenEnemies;
    [SerializeField] private GameObject gameOverScreenWarriors;
    [SerializeField] private Text gameOverScreenEnemiesText;
    [SerializeField] private Text gameOverScreenWarriorsText;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text consumeText;
    [SerializeField] private Text waveText;
    [SerializeField] private Color neutralColor;
    [SerializeField] private Color negativeColor;
    [SerializeField] private Image pauseButton;
    [SerializeField] private Image resumeButton;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite resumeSprite;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite skipSprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sadTrombone;
    [SerializeField] private int peasantCount = 3;
    [SerializeField] private int warriorCount = 0;
    [SerializeField] private int wheatCount = 10;
    [SerializeField] private int wheatIncomeRate = 2;
    [SerializeField] private int wheatIncomeDelay = 4;
    [SerializeField] private int wheatConsumeRate = 5;
    [SerializeField] private int wheatConsumeDelay = 6;
    [SerializeField] private int peasantCost = 4;
    [SerializeField] private int warriorCost = 8;
    [SerializeField] private int peasantDelay = 5;
    [SerializeField] private int warriorDelay = 2;
    [SerializeField] private int raidDelay = 20;
    [SerializeField] private int raidIncrease = 2;
    private int raidWave = 1;
    private bool isCreatingPeasant = false;
    private bool isCreatingWarrior = false;
    private bool isGamePaused = false;
    private bool isGameSped = false;
    void Start()
    {
        raidTimer.SetDelay(raidDelay);
        incomeTimer.SetDelay(wheatIncomeDelay);
        consumeTimer.SetDelay(wheatConsumeDelay);
        wheatDisplay.SetValue(wheatCount, false);
        peasantDisplay.SetValue(peasantCount, false);
        warriorDisplay.SetValue(warriorCount, false);
    }

    void Update()
    {
        if (raidTimer.isIdle)
        {
            warriorCount -= raidWave * raidIncrease;
            if (warriorCount >= 0)
            {
                raidWave += 1;
                raidTimer.SetDelay(raidDelay);
                warriorDisplay.SetValue(warriorCount);
            }
            else
            {
                warriorCount = 0;
                Time.timeScale = 0;
                gameOverScreenEnemiesText.text += (raidWave - 1).ToString();
                gameOverScreenEnemies.SetActive(true);
                audioSource.loop = false;
                audioSource.clip = sadTrombone;
                audioSource.Play();
                this.enabled = false;
            }
        }
        if (incomeTimer.isIdle)
        {
            wheatCount += peasantCount * wheatIncomeRate;
            incomeTimer.SetDelay(wheatIncomeDelay);
            wheatDisplay.SetValue(wheatCount);
        }
        if (consumeTimer.isIdle)
        {
            wheatCount -= warriorCount * wheatConsumeRate;
            if (wheatCount >= 0)
            {
                consumeTimer.SetDelay(wheatConsumeDelay);
                wheatDisplay.SetValue(wheatCount);
            }
            else
            {
                wheatCount = 0;
                Time.timeScale = 0;
                gameOverScreenWarriorsText.text += (raidWave - 1).ToString();
                gameOverScreenWarriors.SetActive(true);
                audioSource.loop = false;
                audioSource.clip = sadTrombone;
                audioSource.Play();
                this.enabled = false;
            }
        }
        if (peasantTimer.isIdle && isCreatingPeasant)
        {
            isCreatingPeasant = false;
            peasantCount++;
            peasantDisplay.SetValue(peasantCount);
        }
        if (warriorTimer.isIdle && isCreatingWarrior)
        {
            isCreatingWarrior = false;
            warriorCount++;
            warriorDisplay.SetValue(warriorCount);
        }
        peasantButton.interactable = !(wheatCount < peasantCost || isCreatingPeasant);
        warriorButton.interactable = !(wheatCount < warriorCost || isCreatingWarrior);

        enemyText.text = (raidWave * raidIncrease).ToString();
        enemyText.color = raidWave * raidIncrease > warriorCount ? negativeColor : neutralColor;
        consumeText.text = (warriorCount * wheatConsumeRate).ToString();
        consumeText.color = warriorCount * wheatConsumeRate > wheatCount ? negativeColor : neutralColor;
        waveText.text = (raidWave).ToString();
    }
    public void OnPeasantButtonClick()
    {
        if (wheatCount >= peasantCost)
        {
            wheatCount -= peasantCost;
            peasantTimer.SetDelay(peasantDelay);
            peasantButton.interactable = false;
            isCreatingPeasant = true;
            wheatDisplay.SetValue(wheatCount);
        }
    }
    public void OnWarriorButtonClick()
    {
        if (wheatCount >= warriorCost)
        {
            wheatCount -= warriorCost;
            warriorTimer.SetDelay(warriorDelay);
            warriorButton.interactable = false;
            isCreatingWarrior = true;
            wheatDisplay.SetValue(wheatCount);
        }
    }
    public void OnPauseClick()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            pauseButton.sprite = resumeSprite;
        }
        else
        {
            pauseButton.sprite = pauseSprite;
        }
        Time.timeScale = (isGamePaused ? 0 : 1) * (isGameSped ? 2 : 1);
    }
    public void OnSpeedClick()
    {
        isGameSped = !isGameSped;
        if (isGameSped)
        {
            resumeButton.sprite = skipSprite;
        }
        else
        {
            resumeButton.sprite = playSprite;
        }
        Time.timeScale = (isGamePaused ? 0 : 1) * (isGameSped ? 2 : 1);
    }
}
