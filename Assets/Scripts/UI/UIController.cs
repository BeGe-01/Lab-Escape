using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;
    void Awake()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        if (settingsScreen.activeInHierarchy)
        {
            settingsScreen.SetActive(false);
            pauseScreen.SetActive(true);
        }
        else
        {
            settingsScreen.SetActive(true);
            pauseScreen.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
            settingsScreen.SetActive(false);
        }
    }

}
