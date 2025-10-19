using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float _worldSpeed = 2f;
    public float worldSpeed { get { return _worldSpeed; } }
    private void Awake()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    // Toggle pause panel
    public void Pause()
    {
        // Neu pause panel dang an thi hien no len va dung thoi gian
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.Instance.ExitBoost(PlayerController.Instance._frameBoost);
            AudioManager.Instance.PlaySound(AudioManager.Instance.unPause);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        //SceneManager.LoadScene("GameOver");
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
