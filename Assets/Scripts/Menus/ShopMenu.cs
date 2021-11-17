using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    // variables
    public GameObject shopContainer;

    public Button upgradeButton;
    public Text upgradeCost;
    public Image weaponSprite;

    public AudioSource openingSound;
    public AudioClip open;

    // Update is called once per frame
    void Update()
    {
        // when to open / close shop
        if (GameManager.instance.isGameStarted && !GameManager.instance.isGamePaused && Input.GetKeyDown(KeyCode.I))
        {
            if (GameManager.instance.isShopOpen)
            {
                CloseShop();
            }
            else
            {
                OpenShop();

                // plays open sound effect
                openingSound.PlayOneShot(open);

            } // end if
        } // end if

        if (GameManager.instance.isShopOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();

        } // end if
    }


    /** OPEN AND CLOSE SHOP **/
    private void OpenShop()
    {
        shopContainer.SetActive(true);
        GameManager.instance.isShopOpen = true;

        // Ensures that the cost text is always up to date
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        if (GameManager.instance.weapon.weaponLevel <= 4)
        {
            upgradeCost.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        else
        {
            upgradeCost.text = "Max!";
            upgradeButton.enabled = false;
        }
    }

    private void CloseShop()
    {
        shopContainer.SetActive(false);
        GameManager.instance.isShopOpen = false;
    }
    /** END **/



    /** UPGRADE WEAPON **/
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

            if (GameManager.instance.weapon.weaponLevel <= 4)
            {
                upgradeCost.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
            }
            else
            {
                upgradeCost.text = "Max!";
                upgradeButton.enabled = false;
            }
        } // end if

    }
    /** END **/
}
