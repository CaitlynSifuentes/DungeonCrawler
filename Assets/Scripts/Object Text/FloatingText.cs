using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    // Variables
    public bool active;
    public float duration;
    public float lastShown;
    public GameObject go;
    public Text textComponent;
    public Vector3 motion;


    /** SHOW AND HIDE TEXT **/
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    /** END **/

    public void UpdateFloatingText()
    {
        if(!active)
        {
            return;
        } // end if


        if (Time.time - lastShown > duration)
        {
            Hide();
        } // end if

        go.transform.position += motion * Time.deltaTime;
    }
}
