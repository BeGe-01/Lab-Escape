using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
      public void QuitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }
}