using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game obj;

    public int maxlives = 99;

    public bool gamePaused = false;

    public int score = 0;

    private void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
        UIManager.obj.startGame();
    }

    public void addScore(int scoreGive)
    {
        score += scoreGive;
    }

    public void lostLife()
    {
        AudioManager.obj.playGameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        // Restart current scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        AudioManager.obj.playGameOver();
        Invoke("gameOverScene", 0.8f);
    }

    private void gameOverScene()
    {
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
