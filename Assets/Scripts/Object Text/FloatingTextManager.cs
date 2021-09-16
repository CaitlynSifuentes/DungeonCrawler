using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    // variables
    public GameObject textContainer;
    public GameObject textPrefab;
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        } // end foreach
    }


    /** Setting and Getting Text Variables **/
    public void SetandShow(string message, int fontSize, Color color, Vector3 position, 
        Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.textComponent.text = message;
        floatingText.textComponent.fontSize = fontSize;
        floatingText.textComponent.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);
        
        // if no text is found - instantiates txt and adds it to list
        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.textComponent = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        } // end if

        return txt;
    }
    /** END **/
    

}
