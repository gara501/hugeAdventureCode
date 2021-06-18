using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public static UIManager obj;

    public Transform UIPanel;
    public void startGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
