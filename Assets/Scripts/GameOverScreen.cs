using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene 1");
    }


    public void nextScene()
    {
        SceneManager.LoadScene("SampleScene 2");
    }
}
