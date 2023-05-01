using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    private Funnel _funnel;
    [SerializeField]
    private SpawnTube _spawnTube;

    [SerializeField]
    private ScoreBoard _scoreBoard;

    [SerializeField]
    private AudioSource _correct;
    [SerializeField]
    private AudioSource _incorrect;
    public bool _deadly = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Package") {
            var package = other.gameObject.GetComponent<Package>();

            if(package._colorName.Name == _funnel._color.Name) {
                _scoreBoard.addCorrect();
                _correct.Play();
            }
            else {
                _scoreBoard.addIncorrect();
                _incorrect.Play();
            }
            Destroy(other.gameObject);
            _spawnTube.Invoke("checkPackages", 0.1f);
        }
        else if (other.gameObject.tag == "Player") {
            if (_deadly) {
                _scoreBoard.BadEnding();
            }
            else {
                other.gameObject.transform.position = _spawnTube.transform.position;
            }
        }
    }
}
