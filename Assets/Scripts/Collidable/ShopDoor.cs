using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShopDoor : Collidable
{
    // Variables
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
                // Teleport player to random dungeon location and saves scene
                GameManager.instance.SaveState();

                // loads next dungeon in queue
                SceneManager.LoadSceneAsync(GameManager.instance.nextLevel, LoadSceneMode.Additive);

                GameManager.instance.UnloadScene("ShopFloor");

                // UI element
                interactable.SetActive(false);
            } // end if

        } // end if
    }

    // Hiding interactive UI
    private void OnCollisionExit2D(Collision2D collision)
    {
        interactable.SetActive(false);
    }
}
