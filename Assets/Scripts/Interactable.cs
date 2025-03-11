using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject prisonCell;
    public bool MovePrisonCell;
    [SerializeField] float cellCooldown = 5f;
    [SerializeField] float moveSpeed = 5f;

    Vector3 closedPosition;
    Vector3 openPosition;
    public bool isCellClosed = false;
    bool openCell = false;

    void Start()
    {
        prisonCell = transform.parent.Find("prison break")?.gameObject;
        closedPosition = new Vector3(-4.54f, -4.647f, 0.08f);
        openPosition = prisonCell.transform.localPosition;
    }


    void Update()
    {
        if (MovePrisonCell)
        {
            if (!isCellClosed)
                HandleClosePrisonCell();
            else if (isCellClosed && openCell)
                HandleOpenPrisonCell();
        }
    }


    private void HandleClosePrisonCell()
    {
        openCell = false;
        prisonCell.transform.localPosition = Vector3.MoveTowards(
            prisonCell.transform.localPosition,
            closedPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(prisonCell.transform.localPosition, closedPosition) < 0.01f)
        {
            isCellClosed = true;
            Debug.Log("Cell closed!");
            StartCoroutine(OpenCell());
        }
    }


    private void HandleOpenPrisonCell()
    {
        prisonCell.transform.localPosition = Vector3.MoveTowards(
            prisonCell.transform.localPosition,
            openPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(prisonCell.transform.localPosition, openPosition) < 0.01f)
        {
            isCellClosed = false;
            MovePrisonCell = false;
            Debug.Log("Cell opened!");
        }
    }


    IEnumerator OpenCell()
    {
        yield return new WaitForSeconds(cellCooldown);
        MovePrisonCell = true;
        openCell = true;
    }
}