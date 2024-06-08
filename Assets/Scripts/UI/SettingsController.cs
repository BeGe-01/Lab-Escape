using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public enum SettingType
{
    Sound,
    Music,
}
public class SettingsController : MonoBehaviour
{
    private Image bar_1;
    private Image bar_2;
    private Image bar_3;
    private Image bar_4;
    private Image bar_5;
    [SerializeField] SettingType type;

    void Start()
    {
        bar_1 = transform.GetChild(0).GetComponent<Image>();
        bar_2 = transform.GetChild(1).GetComponent<Image>();
        bar_3 = transform.GetChild(2).GetComponent<Image>();
        bar_4 = transform.GetChild(3).GetComponent<Image>();
        bar_5 = transform.GetChild(4).GetComponent<Image>();
        float volume = type == SettingType.Music ? SaveManager.instance.saveData.music : SaveManager.instance.saveData.sound;
        SetBarColor(volume);
    }

    public void ChangeVolume(float value)
    {
        float currentVolume;

        if (type == SettingType.Music)
        {
            currentVolume = SaveManager.instance.saveData.music;
            if (currentVolume > 0 || currentVolume < 1)
            {
                SaveManager.instance.SetMusicVolume(Mathf.Clamp(currentVolume + value, 0f, 1f));
                SoundManager.instance.SetMusicVolume(Mathf.Clamp(currentVolume + value, 0f, 1f));
            }
        }
        else
        {
            currentVolume = SaveManager.instance.saveData.sound;
            if (currentVolume > 0 || currentVolume < 1)
            {
                SaveManager.instance.SetSoundVolume(Mathf.Clamp(currentVolume + value, 0f, 1f));
                SoundManager.instance.SetSoundVolume(Mathf.Clamp(currentVolume + value, 0f, 1f));
            }
        }
        SetBarColor(Mathf.Clamp(currentVolume + value, 0, 1));
    }

    private void SetBarColor(float volume)
    {
        Debug.Log(volume);
        bar_1.color = volume >= 0.2f ? Color.green : Color.white;
        bar_2.color = volume >= 0.4f ? Color.green : Color.white;
        bar_3.color = volume >= 0.6f ? Color.green : Color.white;
        bar_4.color = volume >= 0.8f ? Color.green : Color.white;
        bar_5.color = volume >= 1f ? Color.green : Color.white;
    }
}
