using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Fracture : MonoBehaviour
{
    GameObject enemyTrigger;
    DestructableEnemyTrigger det;
    public TriggerOptions triggerOptions;
    public FractureOptions fractureOptions;
    public RefractureOptions refractureOptions;
    public CallbackOptions callbackOptions;
    EnemyHealth eh;

    private GameObject fragmentRoot;
    public float explosionForce = 5f;
    [HideInInspector]
    public int currentRefractureCount = 0;

    void Start()
    {
        if (transform.childCount > 0)
        {
            enemyTrigger = transform.GetChild(0).gameObject;
            det = enemyTrigger.GetComponent<DestructableEnemyTrigger>();
        }
    }

    public void CauseFracture()
    {
        callbackOptions.CallOnFracture(null, gameObject, transform.position);
        ComputeFracture();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (triggerOptions.triggerType == TriggerType.Collision && collision.contactCount > 0)
        {
            var contact = collision.contacts[0];
            float collisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;
            bool tagAllowed = triggerOptions.IsTagAllowed(contact.otherCollider.gameObject.tag);
            if (collisionForce > triggerOptions.minimumCollisionForce &&
                (triggerOptions.filterCollisionsByTag && tagAllowed))
            {
                if (det)
                    det.ObjectDestroyed();
                callbackOptions.CallOnFracture(contact.otherCollider, gameObject, contact.point);
                ComputeFracture();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (triggerOptions.triggerType == TriggerType.Trigger &&
            triggerOptions.filterCollisionsByTag &&
            triggerOptions.IsTagAllowed(collider.gameObject.tag))
        {
            callbackOptions.CallOnFracture(collider, gameObject, transform.position);
            ComputeFracture();
        }
    }

    void Update()
    {
        if (triggerOptions.triggerType == TriggerType.Keyboard && Input.GetKeyDown(triggerOptions.triggerKey))
        {
            callbackOptions.CallOnFracture(null, gameObject, transform.position);
            ComputeFracture();
        }
    }

    public void ComputeFracture()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mesh != null)
        {
            if (fragmentRoot == null)
            {
                fragmentRoot = new GameObject($"{name}Fragments");
                fragmentRoot.transform.SetParent(transform.parent);
                fragmentRoot.transform.position = transform.position;
                fragmentRoot.transform.rotation = transform.rotation;
                fragmentRoot.transform.localScale = Vector3.one;
            }

            var fragmentTemplate = CreateFragmentTemplate();

            Fragmenter.Fracture(gameObject, fractureOptions, fragmentTemplate, fragmentRoot.transform);
            Destroy(fragmentTemplate);
            ApplyRandomForce();
            Destroy(this.gameObject);
        }
    }

    private void ApplyRandomForce()
    {
        foreach (Transform fragment in fragmentRoot.transform)
        {
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDir = Random.insideUnitSphere.normalized;
                rb.AddForce(randomDir * Random.Range(explosionForce * 0.5f, explosionForce), ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * explosionForce, ForceMode.Impulse);
            }
        }
    }

    private GameObject CreateFragmentTemplate()
    {
        GameObject obj = new GameObject("Fragment");
        obj.tag = tag;
        obj.AddComponent<MeshFilter>();
        var meshRenderer = obj.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterials = new Material[] { GetComponent<MeshRenderer>().sharedMaterial, fractureOptions.insideMaterial };

        var thisCollider = GetComponent<Collider>();
        var fragmentCollider = obj.AddComponent<MeshCollider>();
        fragmentCollider.convex = true;
        fragmentCollider.sharedMaterial = thisCollider.sharedMaterial;
        fragmentCollider.isTrigger = thisCollider.isTrigger;

        var fragmentRigidBody = obj.AddComponent<Rigidbody>();
        fragmentRigidBody.useGravity = GetComponent<Rigidbody>().useGravity;

        if (refractureOptions.enableRefracturing &&
            currentRefractureCount < refractureOptions.maxRefractureCount)
        {
            CopyFractureComponent(obj);
        }

        return obj;
    }

    private void CopyFractureComponent(GameObject obj)
    {
        var fractureComponent = obj.AddComponent<Fracture>();
        fractureComponent.triggerOptions = triggerOptions;
        fractureComponent.fractureOptions = fractureOptions;
        fractureComponent.refractureOptions = refractureOptions;
        fractureComponent.callbackOptions = callbackOptions;
        fractureComponent.currentRefractureCount = currentRefractureCount + 1;
        fractureComponent.fragmentRoot = fragmentRoot;
    }
}