using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hud : MonoBehaviour
{
    // Variables
    public Text coinsText;
    public List<Image> healthHearts;


    // Update is called once per frame
    void Update()
    {
        coinsText.text = GameManager.instance.coins.ToString();
    }
}
