using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    private void Awake()
    {
        ButtonToArray();
        UpdateButtonInteractability();
    }

    public void OpenLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
    }

    void ButtonToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    void UpdateButtonInteractability()
    {
        SaveManager saveManager = SaveManager.instance;
        if (saveManager == null || saveManager.saveData == null)
        {
            Debug.LogError("SaveManager or SaveData is null");
            return;
        }

        // Set all buttons to non-interactable initially
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        // Unlock levels based on saveData
        buttons[0].interactable = true;
        if (saveManager.saveData.level1_completed || buttons.Length > 0)
        {
            buttons[1].interactable = true;
        }
        if (saveManager.saveData.level2_completed && buttons.Length > 1)
        {
            buttons[2].interactable = true;
        }
        if (saveManager.saveData.level3_completed && buttons.Length > 2)
        {
            // buttons[2].interactable = true;
        }
    }

    public void Quit(){
        Application.Quit();
    }
}
