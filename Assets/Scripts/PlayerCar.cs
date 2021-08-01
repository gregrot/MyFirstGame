using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] float acceleration = 8;
    [SerializeField] float turnSpeed = 80;
    [SerializeField] float targetSensitivity = 15;
    Quaternion targetRotation;
    Rigidbody _rigidBody;

    Vector3 lastPosition;

    Vector3[] targets = new Vector3[]{new Vector3(0,0,0),new Vector3(100,0,0),new Vector3(100,0,100),new Vector3(0,0,100)};
    int currentTargetId = 0;

    float _sideSlipAmount = 0;
    public float SideSlipAmount
    {
        get
        {
            return _sideSlipAmount;
        }
    }
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //currentTarget = new Vector3(0,0,0);
    }
    void Update()
    {
        UpdateCurrentTarget();
        SetRotationPoint();
        SetSideSlip();
    }

    private void UpdateCurrentTarget()
    {
        // How far away from the checkpoint are we
        Vector3 currentTarget = targets[currentTargetId];
        float dist = Mathf.Abs((currentTarget - transform.position).magnitude);
        Debug.Log(dist);
        if(dist < targetSensitivity) {
            currentTargetId = (currentTargetId+1) % targets.Length;
        }
        Debug.Log(currentTargetId);

    }
    private void SetSideSlip()
    {
        Vector3 direction = transform.position - lastPosition;
        Vector3 movement = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;
        float speed = _rigidBody.velocity.magnitude / 1000;
        _sideSlipAmount = movement.x;
    }
    private void SetRotationPoint()
    {
        Vector3 currentTarget = targets[currentTargetId];
        Vector3 direction = currentTarget - transform.position;
        float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, rotationAngle, 0);



        //Ray ray = Camera.main.ScreenPointToRay(currentTarget);
        //Plane plane = new Plane(Vector3.up, Vector3.zero);
        //float distance;
        //if (plane.Raycast(ray, out distance))
        //{
        //    Vector3 target = ray.GetPoint(distance);
        //    Vector3 direction = currentTarget - transform.position;
        //    float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //    targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        //}
    }

    private void FixedUpdate()
    {
        float speed = _rigidBody.velocity.magnitude / 1000;
        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidBody.AddRelativeForce(Vector3.forward * accelerationInput);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.fixedDeltaTime);
        Debug.DrawLine(transform.position, transform.position + _rigidBody.velocity, Color.red);
        Debug.DrawLine(transform.position, targets[currentTargetId], Color.green);
    }
}
