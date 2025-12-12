using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; } // ✅ Singleton instance

    public Player player;
    public TMP_Text scoreText; // Use TMP_Text for TextMeshPro
    public GameObject PlayButton;
    public GameObject GameOver;
    public GameObject GetReady;



    private int score;
    public int Score { get => score; } // ✅ Make score accessible from other scripts

    private void Start()
    {
        Screen.fullScreen = true; // ✅ Force Fullscreen Mode
    }


    private void Awake()
    {
        Application.targetFrameRate = 60;
        GameOver.SetActive(false);
        GetReady.SetActive(true);
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        player.ResetPosition();
        player.ResetToIdle();


        PlayButton.SetActive(false);
        GameOver.SetActive(false);
        GetReady.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        foreach (Pipes pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void gameOver()
    {
        GameOver.SetActive(true);
        PlayButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
