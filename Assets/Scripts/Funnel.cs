using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    [SerializeField]
    public PackageColor _color;

    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = _color.Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
