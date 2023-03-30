using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        hudCanvas.gameObject.SetActive(false);
    }

    public void showHUD()
    {
        gameObject.SetActive(false);
        hudCanvas.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
