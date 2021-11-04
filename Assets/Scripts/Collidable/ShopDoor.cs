using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShopDoor : Collidable
{
    // Variables
    private GameObject interactable;
    private float tempPlayerHealth;

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
                // saves player health if they were damaged
                tempPlayerHealth = GameManager.instance.playerScript.hitPoints;

                // Teleport player to random dungeon location and saves scene
                GameManager.instance.SaveState();

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
        GameManager.instance.UnloadScene("ShopFloor");

        // updates player health
        GameManager.instance.playerScript.hitPoints = tempPlayerHealth;

        GameManager.instance.PlayerDamaged();
    }

    // Hiding interactive UI
    private void OnCollisionExit2D(Collision2D collision)
    {
        interactable.SetActive(false);
    }
}
