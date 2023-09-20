using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerNPC : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 moveDirection;
    Vector3 posFixer;
    [SerializeField]
    bool moveToLeft;
    [SerializeField]
    ResetPosition resetPosition;
    // Start is called before the first frame update
    void Start()
    {
        //resetPosition=GetComponent<ResetPosition>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -3)
        {
            posFixer += moveDirection * speed * Time.deltaTime;
            posFixer.y = resetPosition.initialPos.y;
            //posFixer.z = resetPosition.initialPos.z;
            transform.position = posFixer;

        }      
    }
    private void OnEnable()
    {
        resetPosition.ResetPos();
        moveDirection = new Vector3(-3, transform.position.y, 0).normalized;
        posFixer=transform.position;
    }
}
