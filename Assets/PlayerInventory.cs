using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int startMoneyAmount;
    public int money;
    [SerializeField] TMP_Text moneyText;

    void Start()
    {
        money = startMoneyAmount;
    }

    void Update()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }
}
