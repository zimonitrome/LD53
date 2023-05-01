using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Package : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _sprites;

    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    private Rigidbody2D _body;
    public PackageColor _colorName;

    // Start is called before the first frame update
    void Start()
    {
        var size = new Vector2(Random.Range(1.3f, 4.0f), Random.Range(1.0f, 2.0f));
        var sprite = _sprites[Random.Range(0, _sprites.Count)];

        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _body = GetComponent<Rigidbody2D>();

        _renderer.size = size;
        _collider.size = size;
        _body.mass = 0.05f * size.x * size.y;

        _renderer.color = _colorName.Color;
        _renderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
