using UnityEngine;

public class AxeUpgrade : MonoBehaviour
{
    public enum UpgradeType { AttackSpeed, AttackDamage }
    public UpgradeType chosenUpgradeType;
    [SerializeField] int AttackSpeedUpgradeCost;
    [SerializeField] int AttackDamageUpgradeCost;

    public void HandleUpgrade()
    {
        switch (chosenUpgradeType)
        {
            case UpgradeType.AttackDamage:
                UpgradeAttackDamage();
                break;
            case UpgradeType.AttackSpeed:
                UpgradeAttackSpeed();
                break;

        }
    }


    private void UpgradeAttackSpeed()
    {
        if (PlayerInventory.Instance.money >= AttackSpeedUpgradeCost)
        {
            AxeManager.Instance.axeAttackSpeed += AxeManager.Instance.attackSpeedUpgradeAmount;
            AxeManager.Instance.attackSpeedUpgradedAmount += AxeManager.Instance.attackSpeedUpgradeAmount;
            Debug.Log("speed upgraded to " + AxeManager.Instance.axeAttackSpeed);
            PlayerInventory.Instance.money -= AttackSpeedUpgradeCost;
        }
    }
    private void UpgradeAttackDamage()
    {
        if (PlayerInventory.Instance.money >= AttackDamageUpgradeCost)
        {
            AxeManager.Instance.axeAttackDamage += AxeManager.Instance.attackDamageUgradeAmount;
            AxeManager.Instance.attackDamageUgradedAmount += AxeManager.Instance.attackDamageUgradeAmount;
            Debug.Log("damage upgraded to " + AxeManager.Instance.axeAttackDamage);
            PlayerInventory.Instance.money -= AttackDamageUpgradeCost;
        }
    }
}
