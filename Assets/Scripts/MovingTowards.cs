using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTowards : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _touchSpeed;
    [SerializeField] private float _maxSpeed;
    private Vector2 touchPos;
    // Update is called once per frame
    private void Start()
    {
        //_maxSpeed = _speed;
        //rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            // Get the current touch position.
            Vector2 newTouchPos = Input.touches[0].position;

            // Calculate the difference between the two touch positions.
            Vector2 deltaPos = newTouchPos - touchPos;

            // Move the player in the direction of the touch.
            transform.Translate(deltaPos * _touchSpeed * Time.deltaTime);
        }

        transform.Translate(-Vector3.right*Time.deltaTime*_speed,Space.World);
        /*rigidbody?.AddForce(transform.forward * _speed * Time.deltaTime);

        if (rigidbody.velocity.magnitude > _maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * _maxSpeed;
        }*/
    }
    void OnTouchStart(Touch touch)
    {
        touchPos = touch.position;
    }
}
