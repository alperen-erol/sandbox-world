using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject prisonCell;
    public bool MovePrisonCell;
    Vector3 targetPos;

    void Start()
    {
        prisonCell = transform.parent.Find("prison break")?.gameObject;
        targetPos = new Vector3(-4.5f, -4.64f, 0.11f);
    }


    void Update()
    {
        Debug.Log(prisonCell);
        Debug.Log(MovePrisonCell);
        HandleMovePrisonCell();
    }


    private void HandleMovePrisonCell()
    {
        if (MovePrisonCell)
        {
            prisonCell.transform.localPosition = Vector3.Lerp(
            prisonCell.transform.localPosition,
            targetPos,
            Time.deltaTime * 3

        );
        }
    }
}
