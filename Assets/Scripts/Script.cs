using TMPro;
using UnityEngine;

public class Script : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Update()
    {
        text.text = "Health";
    }
}
