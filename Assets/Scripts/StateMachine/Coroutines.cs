using UnityEngine;
using System.Collections;

public class Coroutines : MonoBehaviour
{
    public bool isWaiting;

    IEnumerator Wait2Seconds()
    {
        Debug.Log("Starting Wait");
        isWaiting = true;
        Debug.Log("Waiting");
        yield return new WaitForSeconds(2);
        Debug.Log("Done Waiting");
        isWaiting = false;
    }

    public void StartCoroutineWait2Seconds()
    {
        StartCoroutine(Wait2Seconds());
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
        isWaiting = false;
    }
}
