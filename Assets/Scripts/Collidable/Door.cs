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
                // Teleport player to random dungeon location and saves scene
                GameManager.instance.SaveState();

                GameManager.instance.DefeatedLevel(levelNumber);

                GameManager.instance.UnloadScene(GameManager.instance.previousLevel);


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
