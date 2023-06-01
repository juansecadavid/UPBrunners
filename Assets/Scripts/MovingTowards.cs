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
    private int desiredLine = 1;
    public float lineDistance;
    public Vector2 touchPos;
    public Vector3 touchDir;
    // Update is called once per frame
    private void Start()
    {
        //_maxSpeed = _speed;
        //rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector2 targetPosition = new Vector2(transform.position.x,transform.position.y);
        touchPos = targetPosition;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLine++;
            if (desiredLine == 3)
            {
                desiredLine = 2;
            }
            if (desiredLine == 0)
            {
                targetPosition += Vector2.left * lineDistance;
                StartCoroutine(LerpPosition(transform.position,targetPosition,0.5f));
                //transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);

            }
            else if (desiredLine == 2)
            {
                targetPosition += Vector2.right * lineDistance;
                StartCoroutine(LerpPosition(transform.position, targetPosition, 0.5f));
                //transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
            }
            else
            {
                StartCoroutine(LerpPosition(transform.position, targetPosition, 0.5f));
                //transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLine--;
            if (desiredLine == -1)
            {
                desiredLine = 0;
            }
            if (desiredLine == 0)
            {
                targetPosition += Vector2.left * lineDistance;
                //transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
                StartCoroutine(LerpPosition(transform.position, targetPosition, 0.5f));

            }
            else if (desiredLine == 2)
            {
                targetPosition += Vector2.right * lineDistance;
                //transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
                StartCoroutine(LerpPosition(transform.position, targetPosition, 0.5f));
            }
            else
            {
                //transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
                StartCoroutine(LerpPosition(transform.position, targetPosition, 0.5f));
            }
        }
    
        
        
    }
    void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.RightArrow))
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
        }*/
        transform.Translate(Vector3.forward*Time.deltaTime*_speed,Space.World);
        touchDir = transform.position;
        /*rigidbody?.AddForce(transform.forward * _speed * Time.deltaTime);

        if (rigidbody.velocity.magnitude > _maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * _maxSpeed;
        }*/
    }
    IEnumerator LerpPosition(Vector2 initial, Vector2 target, float lerpDuration)
    {
        float timeElapsed = 0f;
        Vector3 ese1 =new Vector3(initial.x,initial.y,touchDir.z);
        Vector3 ese2 = new Vector3(target.x, target.y, touchDir.z);
        while (timeElapsed < lerpDuration)
        {
            transform.position=Vector3.Lerp(ese1,ese2,timeElapsed/lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = ese2;
    }
}
