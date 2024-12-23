using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject[] hearts; // Tablica serduszek (RawImage: Health1, Health2, itd.)
    public TextMeshProUGUI scoreText; 
    private int currentHealth;
    private int score = 0;

    void Start()
    {
        currentHealth = hearts.Length; // Zdrowie = liczba serduszek
        GameManager.uiManager = this;
        UpdateScoreText();
    }

    public void ReduceHealth(int amount)
    {
        currentHealth -= amount;

        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            if (i >= currentHealth)
            {
                hearts[i].SetActive(false); // Ukryj serduszko
            }
        }

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score;
        }
        else
        {
            Debug.LogError("Score Text (TextMeshProUGUI) nie jest przypisany w UIManager!");
        }
    }

    private void GameOver()
    {
        Debug.Log("Koniec gry");
        // Tutaj możesz dodać inne akcje końca gry
    }
}
