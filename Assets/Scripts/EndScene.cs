using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeText;

    // Start is called before the first frame update
    void Start()
    {
        _timeText.text = ScoreBoard._timeTextValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) {
            SceneManager.LoadScene(0);
        }
    }
}
