﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIController.Instance.EscMenuPanel.SetActive(false);
        UIController.Instance.DeathScreenPanel.SetActive(false);
        Time.timeScale = 1f;
        UIController.Instance.isEscMenuVisible = false;
        UIController.Instance.isDeathScreenVisible = false;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
