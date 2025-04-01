using TMPro;
using UnityEngine;

public class AxeUpgradeStationCanvas : MonoBehaviour
{
    private float currentAS;
    private float upgradeAS;
    private float currentAD;
    private float upgradeAD;

    [SerializeField] TMP_Text currentASText;
    [SerializeField] TMP_Text UpgradeASText;
    [SerializeField] TMP_Text currentADText;
    [SerializeField] TMP_Text upgradeADText;

    AxeManager am = AxeManager.Instance;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // currentAS = am.axeAttackSpeed;
        // currentAD = am.axeAttackDamage;
        // upgradeAS = am.attackSpeedUpgradedAmount;
        // upgradeAD = am.attackDamageUgradedAmount;

        currentAS = AxeManager.Instance.axeAttackSpeed;
        currentAD = AxeManager.Instance.axeAttackDamage;
        upgradeAS = AxeManager.Instance.attackSpeedUpgradedAmount;
        upgradeAD = AxeManager.Instance.attackDamageUgradedAmount;


        currentASText.text = currentAS.ToString();
        UpgradeASText.text = upgradeAS.ToString();
        currentADText.text = currentAD.ToString();
        upgradeADText.text = upgradeAD.ToString();
    }
}
