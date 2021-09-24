using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChest : Collectable
{
    // variables
    public int coinsAmount = 10;
    public Animator _animator;

    protected override void OnCollect()
    {
        if (!collected)
        {
            // UI element
            interactable.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                // collects the coins and displays that to player
                collected = true;
                GameManager.instance.ShowText("+" + coinsAmount + " Coins!", 25, new Color(80 / 255f, 200 / 255f, 120 / 255f), new Vector3(transform.position.x, transform.position.y + 1, 0),
                    Vector3.up * 50, 1.5f);

                //animation controls
                _animator.SetBool("wasOpened", true);

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
