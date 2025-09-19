using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<WheelCollider> throttleWheels = new();
    public List<WheelCollider> steeringWheels = new();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    float giro;
    float acel = 1f;

    
    private void FixedUpdate()
    {
        foreach (var wheel in throttleWheels)
        {
            wheel.motorTorque = throttleCoefficient * T.GetFDT() * acel;
        }

        foreach (var wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * giro;
        }

        giro = 0f;
    }

    public void SetGiro(float giro) => this.giro = giro;
    public void SetAcel(float val) => acel = val;
}