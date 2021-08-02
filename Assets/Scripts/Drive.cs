using UnityEngine;

public class Drive : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public float torque = 1000f;

    void Start()
    {
        //wheelCollider = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //float a = Input.GetAxis("Vertical");
        //Go(a);
    }

    private void Go(float accel)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        float thrustTorque = accel * torque;
        wheelCollider.motorTorque = thrustTorque;
    }
}
