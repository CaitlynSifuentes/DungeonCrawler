using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Variables
    public Transform lookAt;
    public float boundX = 0.001f;
    public float boundY = 0.001f;


    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // see if camera is in bounds on X-axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x > lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            } // end if
        } // end if

        // see if camera is in bounds on Y-axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y > lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            } // end if
        } // end if

        transform.position += new Vector3(delta.x, delta.y, 0);

    }
}
