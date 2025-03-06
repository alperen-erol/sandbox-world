using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject stonePrefab;
    public GameObject stonePropPrefab;
    public GameObject playerKatana;
    public GameObject playerKatanaProp;
    public GameObject CameraHolder;
    private GameObject heldItem;
    private FixedJoint itemJoint;
    public TMP_Text pickupText;
    public LayerMask layerMask;
    private Vector3 rayStartPos;
    private Vector3 rayDirection;

    public float springSpring;
    public float springDamper;
    public float springMinDistance;
    public float springMaxDistance;

    public float stoneCount = 0;
    public bool playerHasBalls = false;
    public bool itemDetected = false;
    public int currentActiveSlot = 0;
    public bool destroyBallInHand = false;

    void Start()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        playerKatana.SetActive(false);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Activateslot1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Activateslot2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Activateslot3();
        }

        if (Input.GetKeyUp(KeyCode.E) && heldItem != null)
        {
            DropHeldItem();
        }

        ItemCheckRay();
        HandleItemDrop();
    }

    private void Activateslot1()
    {
        Slot1.SetActive(true);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        currentActiveSlot = 1;
    }
    private void Activateslot2()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(true);
        Slot3.SetActive(false);
        InstantiateBall();
        currentActiveSlot = 2;
    }

    private void HandleItemDrop()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (currentActiveSlot == 1)
            {
                playerKatana.SetActive(false);
                InstantiateKatanaProp();
            }
            else if (currentActiveSlot == 2)
            {
                if (playerHasBalls == true)
                {
                    InstantiateBallProp();
                    stoneCount--;
                    if (stoneCount == 0)
                    {
                        playerHasBalls = false;
                        destroyBallInHand = true;
                    }
                }
            }
        }
    }

    private void Activateslot3()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(true);
        currentActiveSlot = 3;
    }

    public void InstantiateBall()
    {
        if (stoneCount > 0 && playerHasBalls == false)
        {
            destroyBallInHand = false;
            Instantiate(stonePrefab, Slot2.transform.position, Slot2.transform.rotation, Slot2.transform);
            playerHasBalls = true;
        }
    }

    public void InstantiateBallProp()
    {
        if (stoneCount > 0 && playerHasBalls == true)
        {
            GameObject newStone = Instantiate(stonePropPrefab, Slot2.transform.position, Slot2.transform.rotation);
            StartCoroutine(DelayedForce(newStone, Slot2.transform.forward * 4));
            playerHasBalls = true;
        }
    }

    private void InstantiateKatanaProp()
    {
        GameObject newKatana = Instantiate(playerKatanaProp, Slot1.transform.position + Slot1.transform.forward * 3, quaternion.Euler(0, 0, -90));
        StartCoroutine(DelayedForce(newKatana, Slot1.transform.forward * 4));
    }

    RaycastHit[] hits;
    float distance;

    private void ItemCheckRay()
    {
        rayStartPos = Camera.main.transform.position;
        rayDirection = Camera.main.transform.forward;
        distance = 3f;

        hits = Physics.RaycastAll(rayStartPos, rayDirection, distance, layerMask);
        Debug.DrawRay(rayStartPos, rayDirection * distance, Color.red);

        itemDetected = false;

        foreach (RaycastHit hit in hits)
        {
            pickupText.text = hit.collider.name;
            pickupText.gameObject.SetActive(true);
            itemDetected = true;


            if (hit.collider.tag == "Stone")
            {
                pickupText.text = "Press E to pick up Stone";
                pickupText.gameObject.SetActive(true);
                itemDetected = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    stoneCount++;
                    if (stoneCount == 1)
                        InstantiateBall();
                    Destroy(hit.collider.gameObject);
                }

            }


            else if (hit.collider.tag == "Katana Drop")
            {
                pickupText.text = "Press E to pick up Katana";
                pickupText.gameObject.SetActive(true);
                itemDetected = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerKatana.SetActive(true);
                    Destroy(hit.collider.gameObject);
                }
            }


            else if (hit.collider.tag == "CellButton")
            {
                pickupText.text = "Press E to close The Cell ";
                pickupText.gameObject.SetActive(true);
                itemDetected = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject button = hit.collider.gameObject;
                    button.GetComponent<Interactable>().MovePrisonCell = true;
                }
            }


            if (hit.collider.CompareTag("PickupItem"))
            {
                pickupText.text = "Hold E to pick up " + hit.collider.name;
                itemDetected = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    heldItem = hit.collider.gameObject;
                    Rigidbody itemRb = heldItem.GetComponent<Rigidbody>();

                    if (itemRb != null)
                    {
                        itemJoint = CameraHolder.AddComponent<FixedJoint>();
                        itemJoint.connectedBody = itemRb;
                        // itemJoint.spring = springSpring;
                        // itemJoint.damper = springDamper;
                        // itemJoint.minDistance = springMinDistance;
                        // itemJoint.maxDistance = springMaxDistance;
                        itemJoint.breakForce = 5000;
                        itemJoint.breakTorque = 5000;
                    }
                }
            }
        }
        if (itemDetected == false)
        {
            pickupText.gameObject.SetActive(false);
        }
    }

    private void DropHeldItem()
    {
        if (itemJoint != null)
        {
            Destroy(itemJoint);
            itemJoint = null;
        }
        heldItem = null;
    }



    private IEnumerator DelayedForce(GameObject obj, Vector3 force)
    {
        yield return new WaitForSecondsRealtime(0);
        yield return null;

        if (obj != null && obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
