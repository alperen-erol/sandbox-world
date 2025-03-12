using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] AudioSource audioSourceOnDeath;
    [SerializeField] AudioClip deathSound;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameWinScreen;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


    public void HandlePlayerDeath()
    {
        gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSourceOnDeath.PlayOneShot(deathSound);
        Debug.Log("ded");
        Destroy(player);
        Destroy(Camera);
    }


    public void HandlePlayerWin()
    {
        gameWinScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // audioSourceOnDeath.PlayOneShot(deathSound); başarı sesi eklenecekk
        Debug.Log("win condition met");
        Destroy(player);
        Destroy(Camera);
    }
}
