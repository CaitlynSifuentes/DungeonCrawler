using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    // variables
    public List<int> hitPointsInLevel;

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
        hitPoints = hitPointsInLevel[level - 1];
    }
    /** END **/


    /** HEALING **/
    public void Heal(int healingAmount)
    {
        if (hitPoints == maxHitPoint)
            return;

        hitPoints += healingAmount;

        if (hitPoints > maxHitPoint)
        {
            hitPoints = maxHitPoint;
        } // end if

        GameManager.instance.ShowText("+ " + healingAmount.ToString(), 25, new Color(99 / 255f, 180 / 255f, 184 / 255f), new Vector3(transform.position.x, transform.position.y + 1, 0), Vector3.up * 30, 2f);
        GameManager.instance.hudScript.UpdateHearts();
    }
    /** END **/
}
