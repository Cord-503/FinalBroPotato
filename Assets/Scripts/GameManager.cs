using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float countdownTime = 10f;
    private float currentTime;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject WinUI;
    public GameObject LoseUI;

    private bool isTimerRunning = true;

    public static GameManager Instance;

    void Start()
    {
        Instance = this;
        currentTime = countdownTime;
        WinUI.SetActive(false);
        LoseUI.SetActive(false);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            RunTimer();
        }
    }

    void RunTimer()
    {
        currentTime -= Time.deltaTime;

        UpdateTimerUI();

        if (currentTime <= 0)
        {
            TimerEnd();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnd()
    {
        isTimerRunning = false;
        currentTime = 0;
        UpdateTimerUI();

        Time.timeScale = 0f;

        WinUI.SetActive(true);
    }

    public static void GameOver()
    {

        Instance.isTimerRunning = false;
        Time.timeScale = 0f;
        Instance.LoseUI.SetActive(true);
    }

}
