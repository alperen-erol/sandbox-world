using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public Button button;
    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene 1");
    }
}
