using UnityEngine;
using System.Collections;

public class Coroutines : MonoBehaviour
{
    public bool isWaiting;

    IEnumerator Wait2Seconds()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
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
