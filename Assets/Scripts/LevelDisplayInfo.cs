using UnityEngine;
using TMPro; 

public class LevelDisplay : MonoBehaviour
{
    public string levelId;
    public GameObject batteryIcon;
    public GameObject deathIcon;
    public TextMeshProUGUI totalDeathsText;
    public TextMeshProUGUI totalBatteriesText;

    void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (SaveManager.instance == null || SaveManager.instance.saveData == null)
        {
            Debug.LogWarning("SaveManager or SaveData is not initialized.");
            return;
        }

        SaveData data = SaveManager.instance.saveData;
        bool isLevelUnlocked = IsLevelUnlocked(data);

        if (isLevelUnlocked)
        {
            switch (levelId)
            {
                case "Level 1":
                    totalDeathsText.text = data.level1_deaths.ToString();
                    break;
                case "Level 2":
                    totalDeathsText.text = data.level2_deaths.ToString();
                    break;
                case "Level 3":
                    totalDeathsText.text = data.level3_deaths.ToString();
                    break;
            }
            totalBatteriesText.text = data.batteries.Count.ToString();
        }
        else
        {
            totalDeathsText.text = "0";
            totalBatteriesText.text = "0";
        }

        batteryIcon.SetActive(isLevelUnlocked && data.batteries.Count > 0);
        deathIcon.SetActive(true); 
    }

    private bool IsLevelUnlocked(SaveData data)
    {
        switch (levelId)
        {
            case "Level 1":
                return true; 
            case "Level 2":
                return data.level1_completed;
            case "Level 3":
                return data.level2_completed;
            default:
                return false;
        }
    }
}
