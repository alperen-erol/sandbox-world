using TMPro;
using UnityEngine;

public class ShotgunManager : MonoBehaviour
{
    public static ShotgunManager Instance;

    [SerializeField] TMP_Text ammoText;
    public float ammoCount;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        ammoText.text = "Ammo: " + ammoCount.ToString();
    }
}
