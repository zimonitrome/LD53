using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame() {
        ScoreBoard._time = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
