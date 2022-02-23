using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public UnityAction<int> m_clip; 
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private Text healthText;
    [SerializeField] private Text coinText;

    public static bool _isPause = false;
    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        pause();
    }
    // Start is called before the first frame update
    public void active()
    {
        deathUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void inactive()
    {
        Time.timeScale = 1;
    }

    public void restart()
    {
        inactive();
        m_clip?.Invoke(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void mainMenu()
    {
        inactive();
        m_clip?.Invoke(0);
        SceneManager.LoadScene("SceneIntro");
    }
    private void pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isPause)
            {
                Time.timeScale = 1;
                _isPause = false;
                pauseUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                _isPause = true;
                pauseUI.SetActive(true);
            }
        }
    }

    public void channge_scene()
    {
        inactive();
        m_clip?.Invoke(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void winActive()
    {
        Time.timeScale = 0;
        m_clip?.Invoke(3);
        winUI.SetActive(true);
        healthText.text = "= " + playerHealth.instance._currentHealth / 5;
        coinText.text = "= " + coin.instance.Coin.ToString();
    }

    public void exit()
    {
        Application.Quit();
    }
}
