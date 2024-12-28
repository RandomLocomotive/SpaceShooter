using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Wychodzenie z gry.");
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Laduje: "+ 1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Ładuje: "+ 0);
    }
}
