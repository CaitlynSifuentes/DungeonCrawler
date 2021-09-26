using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // variables
    public Animator _animator;

    public Text levelText, coinsText, xpText;
    public List<Image> healthHearts;

    public Image characterSelectionSprite;

    // Update is called once per frame
    void Update()
    {
        // when to pause / resume game
        if (GameManager.instance.isGameStarted && !GameManager.instance.isShopOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.instance.isGamePaused)
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
    public void Resume()
    {
        _animator.SetBool("isHidden", true);
        GameManager.instance.hud.enabled = true;
        GameManager.instance.isGamePaused = false;
    }

    private void Pause()
    {
        UpdateStats();
        _animator.SetBool("isHidden", false);
        GameManager.instance.hud.enabled = false;
        GameManager.instance.isGamePaused = true;
    }
    /** END **/


    /** UPDATING STATS **/
    private void UpdateStats()
    {
        coinsText.text = GameManager.instance.coins.ToString();
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.currentCharacterSelection];

    }

    /** END **/
}
