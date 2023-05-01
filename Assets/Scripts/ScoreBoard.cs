using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _correctText;
    [SerializeField]
    private TextMeshPro _incorrectText;

    [SerializeField]
    private TextMeshPro _timeText;

    public int _correctScore = 0;
    public int _incorrectScore = 0;

    public static float _time = 0;
    public static bool _running = false;
    public static string _timeTextValue;
    public static int _level = 0;

    // Start is called before the first frame update
    void Start()
    {
        _correctText.text = _correctScore.ToString();
        if (_incorrectText != null) {
            _incorrectText.text = _incorrectScore.ToString();
        }
        _level = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (_running) {
            _time += Time.deltaTime;
        }
        // _timeText.text = TimeSpan.FromSeconds(_time).ToString("hh':'mm':'ss");
        _timeTextValue = TimeSpan.FromSeconds(_time).ToString("mm':'ss'.'fff");
        _timeText.text = _timeTextValue;

        if (_incorrectScore > 3) {
            BadEnding();
        }
    }

    public void BadEnding() {
        PlayerControls._alive = false;
        Invoke("NextScene", 0.5f);
        ScoreBoard._running = false;
    }

    void NextScene(){
        SceneManager.LoadScene(8);
    }    

    public void addCorrect() {
        _correctScore++;
        _correctText.text = _correctScore.ToString();
    }

    public void addIncorrect() {
        _incorrectScore++;
        _incorrectText.text = _incorrectScore.ToString();
    }
}
