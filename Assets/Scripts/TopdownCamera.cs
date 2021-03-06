using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownCamera : MonoBehaviour
{
    
    [SerializeField] Transform observable;
    [SerializeField] float aheadSpeed = 1;
    [SerializeField] float followDamping = 1;
    [SerializeField] float cameraHeight = 50;

    Rigidbody _observableRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _observableRigidBody = observable.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(observable == null) 
            return;
        
        Vector3 targetPosition = observable.position + Vector3.up * cameraHeight + _observableRigidBody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }
}
