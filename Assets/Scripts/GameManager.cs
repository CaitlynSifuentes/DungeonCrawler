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

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public int currentCharacterSelection = 0;
    public Image characterSelectionSprite;

    public bool isGameStarted = false;

    public GameObject player;
    public Canvas hud;
    public GameObject interactible;
    public Player playerScript;
    public FloatingTextManager floatingTextManager;

    public List<string> dungeonScenes;
    public string previousLevel;
    public string nextLevel;

    public int dungeonLevel;
    public int coins;
    public int experience;


    /** CREATION **/
    void Awake()
    {
        if (!isSceneLoaded)
        {
            instance = this;

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

            isSceneLoaded = true;
        }

        player.SetActive(false);
        hud.enabled = false;
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



    /** SAVE STATE **/

    public void SaveState() // saving
    {

        string s = "";

        s += "Skin" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += "weaponLevel";

        PlayerPrefs.SetString("SaveState", s);
    }


    
    public void LoadState(Scene s, LoadSceneMode mode) // loading
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        // will load data saved in SaveState
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        // Change playerSkin
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // change weapon level
        // save dungeon level
    }

    /** END **/



    /** BEATING LEVEL **/
    public void DefeatedLevel(int levelNum)
    {
        previousLevel = dungeonScenes[levelNum];
        dungeonScenes.RemoveAt(levelNum);
        nextLevel = dungeonScenes[levelNum + 1];

        SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);
    }
    /** END **/
}
