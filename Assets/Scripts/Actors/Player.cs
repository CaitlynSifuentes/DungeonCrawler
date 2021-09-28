using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    private void FixedUpdate()
    {
        // getting horizontal and vertical input from user for player movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // calling movement func to move player
        UpdateMotor(new Vector3(x, y, 0));
    }


    /** LEVELING **/
    public void LevelUp()
    {
        maxHitPoint += 2;
        hitPoints += 2;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            LevelUp();
        } // end for
    }
    /** END **/
}
