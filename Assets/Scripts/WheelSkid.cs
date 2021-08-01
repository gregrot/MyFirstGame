using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkid : MonoBehaviour
{
    [SerializeField] float intensityModifier = 1.5f;

    Skidmarks skidmarkController;
    PlayerCar playerCar;

    int lastSkidId = -1;

    void Start()
    {
        skidmarkController = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<PlayerCar>();
    }

    void LateUpdate()
    {
        float intensity = playerCar.SideSlipAmount;

        if (intensity < 0)
            intensity = -intensity;

        if (intensity > 0.2)
        {
            lastSkidId = skidmarkController.AddSkidMark(transform.position, Vector3.up, intensity * intensityModifier, lastSkidId);
        }
        else
        {
            lastSkidId = -1;
        }


    }
}
