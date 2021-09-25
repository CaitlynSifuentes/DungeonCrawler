using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // variables
    public Animator _animator;

    public Text levelText, coinsText, upgradeCostText, xpText;
    public List<Image> healthHearts;

    public Image weaponSprite;

    private bool isGamePaused = false;

    // Update is called once per frame
    void Update()
    {
        // when to pause / resume game
        if (GameManager.instance.isGameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }



    /** PAUSING GAME **/
    private void Resume()
    {
        _animator.SetBool("isHidden", true);
        GameManager.instance.hud.enabled = true;
        isGamePaused = false;
    }

    private void Pause()
    {
        _animator.SetBool("isHidden", false);
        GameManager.instance.hud.enabled = false;
        isGamePaused = true;
    }
    /** END **/
}
