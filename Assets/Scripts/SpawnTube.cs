using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnTube : MonoBehaviour
{
    [SerializeField]
    private Package _packagePrefab;

    [SerializeField]
    private int _nPackages=2;

    [SerializeField]
    public List<PackageColor> _packageColors;

    private List<Package> _packages = new List<Package>();
    private bool _noPackages = true;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoader());

        _audio = GetComponent<AudioSource>();
    }

    double LogGrowth(double x, double a, double k)
    {
        // return a * (1 - Math.Exp(-k * x)) / (1 + Math.Exp(-k * x));
        return 1/(1+Math.Exp(k*(x-a)));
    }

    IEnumerator SpawnLoader() {
        int startInterval = 10;
        int endInterval = 4;
        yield return new WaitForSeconds(1);
        for (int x = 0; x < _nPackages; x++)
        {
            // _waitTimes.Add(startTime + startInterval * LogGrowth(x, 10, 0.2));
            double waitTime = (startInterval-endInterval) * LogGrowth(x, 10, 0.4) + endInterval;
            yield return wait((float)waitTime);
            SpawnPackage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(_packages.Count);
    }

    void SpawnPackage()
    {
        if (!PlayerControls._alive) {
            return;
        }
        var package = Instantiate(_packagePrefab, this.transform.position, new Quaternion(0, 0, -90, 0));
        package._colorName = _packageColors[Random.Range(0, _packageColors.Count)];
        _packages.Add(package);
        _noPackages = false;
        ScoreBoard._running = true;
    }

    IEnumerator wait(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            // Debug.Log("We have waited for: " + counter + " seconds");
            if (_noPackages)
            {
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
    }

    public void checkPackages() {
        _noPackages = _packages.Where(p => p != null).Count() == 0;

        if (_noPackages && _packages.Count == _nPackages) {
            FinishGame();
        }
    }

    void FinishGame(){
        ScoreBoard._running = false;
        _audio.PlayDelayed(0.5f);
        Invoke("NextScene", 2);
    }

    void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

[Serializable]
public struct PackageColor
{
    public string Name;
    public Color Color;
}