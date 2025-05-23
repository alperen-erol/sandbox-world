using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectInteract : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image ProgressBarHolder;
    [SerializeField] Image ProgressBar;
    [SerializeField] MoveWalls mw;

    [Header("Variables")]
    [SerializeField] float Width;
    [SerializeField] bool loadStatus;
    public bool loadDone;

    private void Start()
    {
        ProgressBarHolder.gameObject.SetActive(false);
        ProgressBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (loadStatus && !mw.moveDone)
            StartLoading();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cylinder"))
        {
            loadStatus = true;
            Debug.Log("collider" + other.gameObject.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Cylinder"))
        {
            loadStatus = false;
            SetUIFalse();
            StopLoading();
            Debug.Log("collider exit" + other.gameObject.name);
        }
    }


    private void SetUITrue()
    {
        ProgressBarHolder.gameObject.SetActive(true);
        ProgressBar.gameObject.SetActive(true);
    }


    private void SetUIFalse()
    {
        ProgressBarHolder.gameObject.SetActive(false);
        ProgressBar.gameObject.SetActive(false);
    }


    private void StartLoading()
    {
        Debug.Log("sStart loading called");
        SetUITrue();
        Width += Time.deltaTime;
        ProgressBar.rectTransform.localScale = new Vector3(Width, 1, 1);
        if (Width >= 1)
        {
            Debug.Log("loading complete");
            loadDone = true;
            StopLoading();
            SetUIFalse();
        }
    }


    private void StopLoading()
    {
        Width = 0;
        loadStatus = false;
    }
}