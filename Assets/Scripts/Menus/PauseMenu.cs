using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // variables
    public Animator _animator;

    public AudioSource openingSound;
    public AudioClip open;

    public Text levelText, coinsText, xpText;
    public List<Image> healthHearts;

    public Image characterSelectionSprite;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private float playerHealth;
    private float playerMaxHealth;


    // Update is called once per frame
    void Update()
    {
        // when to pause / resume game
        if (GameManager.instance.isGameStarted && !GameManager.instance.isShopOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.instance.isGamePaused)
            {
                // plays open sound effect
                openingSound.PlayOneShot(open);

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
        levelText.text = "Level " + GameManager.instance.GetCurrentLevel().ToString();

        int currentLevel = GameManager.instance.GetCurrentLevel();
        int previousLevelXp = GameManager.instance.XpLeftInLevel(currentLevel - 1);
        int currentLevelXp = GameManager.instance.XpLeftInLevel(currentLevel);
        int difference = currentLevelXp - previousLevelXp;
        int currentXpIntoLevel = GameManager.instance.experience - previousLevelXp;
        xpText.text = currentXpIntoLevel + " / " + difference;

        coinsText.text = GameManager.instance.coins.ToString();

        characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.currentCharacterSelection];

        UpdateHearts();

    }

    public void UpdateHearts()
    {
        playerHealth = GameManager.instance.playerScript.hitPoints;
        playerMaxHealth = GameManager.instance.playerScript.maxHitPoint;

        for (int i = 0; i < healthHearts.Count; i++)
        {
            if (i < playerHealth)
            {
                if (i + 0.5 == playerHealth)
                {
                    healthHearts[i].sprite = halfHeart;
                }
                else
                {
                    healthHearts[i].sprite = fullHeart;
                } // end if
            }
            else
            {
                healthHearts[i].sprite = emptyHeart;
            } // end if


            if (playerMaxHealth > i)
            {
                healthHearts[i].enabled = true;
            }
            else
            {
                healthHearts[i].enabled = false;
            } // end if
        }// end for
    }

    /** END **/

    public void QuitButton()
    {
        Application.Quit();
    }
}
