using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Movement
{
    // variables
    public List<int> hitPointsInLevel;
    public float currentHealed = 0f;

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
    public bool Heal(float healingAmount, float maxHealingAmount)
    {
        if (hitPoints == maxHitPoint)
            return false; 

        if (currentHealed <= maxHealingAmount)
        {
            hitPoints += healingAmount;
            currentHealed++;

            GameManager.instance.hudScript.UpdateHearts();

            GameManager.instance.ShowText("+ " + healingAmount.ToString(), 25, new Color(99 / 255f, 180 / 255f, 184 / 255f), new Vector3(transform.position.x, transform.position.y + 1, 0), Vector3.up * 30, 2f);

            if (hitPoints > maxHitPoint)
            {
                hitPoints = maxHitPoint;
            } // end if

            return false;
        }
        else
        {
            return true;
        } // end if
    }
    /** END **/



    /** DEATH **/
    protected override void Death()
    {
        // loads the shop scene 
        StartCoroutine(LoadYourAsyncScene());

        movementSpeed = 260;
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
        GameManager.instance.UnloadScene(GameManager.instance.nextLevel);

        GameManager.instance.PlayerDamaged();

    }
    /** END **/
}
