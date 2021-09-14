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
                // collects the coins
                collected = true;
                Debug.Log("Granted " + coinsAmount + " Coins");


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
