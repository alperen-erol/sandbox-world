using UnityEngine;

public class AxeManager : MonoBehaviour
{
    public static AxeManager Instance;

    public float axeAttackDamage;
    public float axeAttackSpeed;
    public float attackSpeedUpgradeAmount;
    public float attackDamageUgradeAmount;

    //ui da kullanmak için
    public float attackSpeedUpgradedAmount;
    public float attackDamageUgradedAmount;
    void Start()
    {
        axeAttackSpeed = 1f;
        axeAttackDamage = 20f;
        attackSpeedUpgradedAmount = axeAttackSpeed + attackSpeedUpgradeAmount;
        attackDamageUgradedAmount = axeAttackDamage + attackDamageUgradeAmount;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
}
