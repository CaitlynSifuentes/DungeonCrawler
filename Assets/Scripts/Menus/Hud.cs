using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hud : MonoBehaviour
{
    // Variables
    public Text coinsText;
    public List<Image> healthHearts;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private float playerHealth;
    private float playerMaxHealth;


    private void Start()
    {
        UpdateHearts();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = GameManager.instance.coins.ToString();
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

}
