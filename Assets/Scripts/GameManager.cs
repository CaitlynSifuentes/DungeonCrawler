using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager instance;

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    public bool isGameStarted = true; // set to false before launch
    public Player player;
    public FloatingTextManager floatingTextManager;

    public int coins;
    public int experience;


    /** CREATION **/
    void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        } // end if

        instance = this;

        SceneManager.sceneLoaded += LoadState;

        DontDestroyOnLoad(gameObject);
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
    }

    /** END **/
}
