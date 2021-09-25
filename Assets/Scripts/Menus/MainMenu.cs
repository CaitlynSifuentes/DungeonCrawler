using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        GameManager.instance.isGameStarted = true;
        GameManager.instance.player.SetActive(true);
        GameManager.instance.hud.enabled = true;

        GameManager.instance.UnloadScene(GameManager.instance.previousLevel);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
