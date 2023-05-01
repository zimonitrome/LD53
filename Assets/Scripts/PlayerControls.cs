using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool _canJump = false;
    private float _dirX = 0;
    private bool _grabbing = false;
    public static bool _alive;

    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _dirX = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(_dirX * 7, _rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && _canJump)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 14);
        }

        if (Input.GetButton("Fire1"))
        {
            _grabbing = true;
        }
        else
        {
            _grabbing = false;
            var joint = transform.GetComponent<FixedJoint2D>();
            if (joint != null)
            {
                joint.connectedBody?.AddForce(new Vector2(0, 80));
                Destroy(joint);
            }
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _renderer.flipX = _dirX < 0;
        _animator.SetBool("Walking", Math.Abs(_dirX) > 0f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>(10);

        _boxCollider.GetContacts(contacts); // ?
        // other.GetContacts(contacts); // ?
        var normals = contacts.Select(c => c.normal);

        if (normals.Any(n => n.y > 0.1))
        { // 0 = very sharp angle (can't jump) 1 = very not sharp angle (can jump)
            _canJump = true;
        }

        if (
                _grabbing &&
                other.tag == "Package"
                &&
                (
                    (normals.Any(n => n.x > 0.4) && _dirX > 0) || // Right
                    (normals.Any(n => n.x < 0.4) && _dirX < 0) // Left
                )
            // other.name == "Package"
            )
        {
            // Grab
            // other.transform.parent = _rigidBody.transform;

            // creates joint
            var joint = transform.GetComponent<FixedJoint2D>();
            if (joint == null) {
                joint = gameObject.AddComponent<FixedJoint2D>();
            }
            // sets joint position to point of contact
            joint.anchor = contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = other.transform.GetComponentInParent<Rigidbody2D>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _canJump = false;
    }


}
