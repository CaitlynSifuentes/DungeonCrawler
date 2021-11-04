using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnShopDoor : Collidable
{
    // Variables
    private GameObject interactable;
    public string dungeonName;
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
                // saves player health as player did not complete level
                tempPlayerHealth = GameManager.instance.playerScript.hitPoints;

                // Teleport player back to spawn
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

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ShopFloor", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // spawns the player in the correct placement
        GameManager.instance.player.transform.position = GameObject.FindGameObjectWithTag("ShopFloorSpawnPoint").transform.position;
        GameManager.instance.UnloadScene(dungeonName);

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
