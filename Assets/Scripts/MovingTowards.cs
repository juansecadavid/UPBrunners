using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTowards : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _touchSpeed;
    [SerializeField] private float _maxSpeed;
    private Vector2 touchPos;
    // Update is called once per frame
    private void Start()
    {
        //_maxSpeed = _speed;
        //rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //transform.Translate(-Vector3.right * _speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.left * 3f, Space.World);
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.World);
            if(transform.position.x<2)
            {
                transform.Translate(Vector3.right * _speedMove * Time.deltaTime, Space.World);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //transform.Translate(-Vector3.right * _speed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.left * 3f, Space.World);
        }
        transform.Translate(Vector3.forward*Time.deltaTime*_speed,Space.World);
        /*rigidbody?.AddForce(transform.forward * _speed * Time.deltaTime);

        if (rigidbody.velocity.magnitude > _maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * _maxSpeed;
        }*/
    }
}
