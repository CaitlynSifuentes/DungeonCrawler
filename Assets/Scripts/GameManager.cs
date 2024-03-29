using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager instance;
    private bool isSceneLoaded = false;

    public List<RuntimeAnimatorController> playerControllers;
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public int currentCharacterSelection = 0;

    public bool isGameStarted = false;
    public bool isGamePaused = false;
    public bool isShopOpen = false;

    public GameObject player;
    public Weapon weapon;
    public Canvas hud;
    public Hud hudScript;
    public GameObject interactible;
    public Player playerScript;
    public FloatingTextManager floatingTextManager;

    public List<string> dungeonScenes;
    public string previousLevel;
    public string nextLevel;

    public int dungeonLevel;
    public int coins;
    public int experience;


    /** CREATION / SCENE MOVEMENT **/
    void Awake()
    {
        if (!isSceneLoaded)
        {
            instance = this;
            SceneManager.sceneLoaded += LoadState;

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            isSceneLoaded = true;
        }

        player.SetActive(false);
        hud.enabled = false;

        PlayerPrefs.DeleteAll();
    }

    public void UnloadScene(string scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);

        if (interactible.activeSelf == true)
        {
            interactible.SetActive(false);
        } // end if
    }
    /** END **/


    /** FLOATING TEXT **/
    public void ShowText(string message, int fontSize, Color color, Vector3 position,
        Vector3 motion, float duration)
    {
        floatingTextManager.SetandShow(message, fontSize, color, position, motion, duration);
    }
    /** END **/


    /** FLOATING TEXT **/
    public void PlayerDamaged()
    {
        hudScript.UpdateHearts();
    }
    /** END **/


    /** SAVE / LOAD STATES **/

    public void SaveState() // saving
    {

        string s = "";

        s += "Skin" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString() + "|";
        s += dungeonLevel.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);
    }


    
    public void LoadState(Scene s, LoadSceneMode mode) // loading
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        // will load data saved in SaveState
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        playerScript.SetLevel(GetCurrentLevel());
        weapon.LoadWeapon(int.Parse(data[3]));
        nextLevel = "Dungeon" + int.Parse(data[4]);
    }

    /** END **/


    /** CHARACTER SELECTION **/
    public void UpdateCharacterAnimation()
    {
        player.GetComponent<Animator>().runtimeAnimatorController = playerControllers[currentCharacterSelection] as RuntimeAnimatorController;
    }
    /** END **/



    /** WEAPON UPGRADE **/
    public bool TryUpgradeWeapon()
    {
        // is weapon max level
        if (weapon.weaponLevel >= 6)
        {
            return false;
        } // end if

        // does player have enough money
        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();

            return true;
        } // end if

        return false;
    }
    /** END **/



    /** EXPERIENCE SYSTEM **/
    public int GetCurrentLevel()
    {
        int returnValue = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[returnValue];

            returnValue++;

            // if player is max level
            if (returnValue == xpTable.Count)
            {
                return returnValue;
            }// end if

        } // end while

        return returnValue;
    }

    public int XpLeftInLevel(int level)
    {
        int returnValue = 0;
        int xp = 0;

        while (returnValue < level)
        {
            xp += xpTable[returnValue];
            returnValue++;
        } // end while

        return xp;
    }

    public void GiveExperience(int xpGiven)
    {
        int currentLevel = GetCurrentLevel();

        experience += xpGiven;
        
        if (currentLevel < GetCurrentLevel())
        {
            OnLevelUp();
        } // end if

    }

    public void OnLevelUp()
    {
        playerScript.LevelUp();
        hudScript.UpdateHearts();
    }
    /** END **/



    /** BEATING LEVEL **/
    public void DefeatedLevel(int levelNum)
    {

        previousLevel = dungeonScenes[levelNum];

        if (levelNum + 1 <= 5)
        {
            nextLevel = dungeonScenes[levelNum + 1];
            dungeonLevel = levelNum + 1;
        }
        else
        {
            nextLevel = "Credits";

            player.SetActive(false);
            hud.enabled = false;
            isGameStarted = false;
        }

    }
    /** END **/
}
