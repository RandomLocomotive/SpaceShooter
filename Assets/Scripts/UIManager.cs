using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3;
    public RawImage heart4;
    public RawImage heart5;

    public TMPro.TextMeshProUGUI Points;
    private int score = 0;  // poczatkowy wynik
    private int currentHealth;

    void Start()
    {
        currentHealth = 5;  // start
        UpdateHealthUI(currentHealth);
        UpdateScoreText();
    }

    public void UpdateHealthUI(int currentHealth)
    {
        heart1.enabled = currentHealth >= 1;
        heart2.enabled = currentHealth >= 2;
        heart3.enabled = currentHealth >= 3;
        heart4.enabled = currentHealth >= 4;
        heart5.enabled = currentHealth >= 5;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (Points != null)
        {
            Points.text = "Points: " + score;
        }
        else
        {
            Debug.LogError("Points Text is not assigned in UIManager.");
        }
    }

    private void GameOver()
    {
        Debug.Log("Koniec gry!");
        // blah blah
    }
}