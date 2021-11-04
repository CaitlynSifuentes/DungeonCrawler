using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Collidable
{
    // Variables
    public int levelNumber;
    private GameObject interactable;

    protected override void Start()
    {
        base.Start();

        interactable = GameManager.instance.interactible;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            // UI element
            interactable.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                // saves scene and updates health
                GameManager.instance.SaveState();

                GameManager.instance.DefeatedLevel(levelNumber);

                // loads next dungeon in queue
                StartCoroutine(LoadYourAsyncScene());


                // UI element
                interactable.SetActive(false);
            } // end if

        } // end if
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameManager.instance.nextLevel, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // spawns the player in the correct placement
        GameManager.instance.player.transform.position = GameObject.FindGameObjectWithTag("SpawnPosition").transform.position;

        // unloads last level
        GameManager.instance.UnloadScene(GameManager.instance.previousLevel);

        // restores player health
        GameManager.instance.hudScript.UpdateHearts();

    }

    // Hiding interactive UI
    private void OnCollisionExit2D(Collision2D collision)
    {
        interactable.SetActive(false);
    }
}
