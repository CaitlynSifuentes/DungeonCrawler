using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoint : MonoBehaviour
{
    public bool continueCoroutine = true;
    private WaitForSeconds delay = new WaitForSeconds(3);
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePosition());
    }

    private IEnumerator UpdatePosition()
    {

        while (true)
        {
            while (continueCoroutine)
            {
                yield return delay;
                transform.position = startingPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * Random.Range(5f, 10f);
            }
            yield return null;
        }
    }
}
