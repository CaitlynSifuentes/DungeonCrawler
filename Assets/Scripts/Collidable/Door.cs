using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Collidable
{
    // Variables
    public string[] sceneNames;
    public GameObject interactable;

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

                string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

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
