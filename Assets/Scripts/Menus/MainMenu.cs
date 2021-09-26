using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // variables
    public Image characterSelectionSprite;


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


    /** CHARACTER SELECTION **/
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            GameManager.instance.currentCharacterSelection ++;

            if (GameManager.instance.currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                GameManager.instance.currentCharacterSelection = 0;
            } // end if

            OnSelectionChange();
        }
        else
        {
            GameManager.instance.currentCharacterSelection --;

            if (GameManager.instance.currentCharacterSelection < 0)
            {
                GameManager.instance.currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            } // end if

            OnSelectionChange();
        }
    }

    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.currentCharacterSelection];

        GameManager.instance.UpdateCharacterAnimation();
    }

    /** END **/
}
