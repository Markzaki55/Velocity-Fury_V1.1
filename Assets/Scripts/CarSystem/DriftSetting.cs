using System;
using UnityEngine;
public interface IDriftSettingHandle
{
    public void ApplyDrift(bool driftinput, float steeringInput , float accelinput);
    public float GetDriftFactor();
    public bool GetDriftState();
}



class DriftSetting : IDriftSettingHandle
{
    public CarWheelColliders colliders;
    DriveType drivetype;
    bool isDrifting;
    Rigidbody carRb;
    float handBrakeFrictionMultiplier;
    float speed;
    float steerInput;
    Transform carTran;
    DriftForcesData driftForcesData;


    MazdaController controller;
    public DriftSetting(CarWheelColliders colliders, Rigidbody carRb, float handBrakeFrictionMultiplier, float speed, MazdaController controller, float Steerinput, DriveType driveType, Transform carTran , DriftForcesData driftForcesData)
    {
        this.colliders = colliders;

        this.carRb = carRb;
        this.handBrakeFrictionMultiplier = handBrakeFrictionMultiplier;
        //this.speed = speed;
        this.steerInput = Steerinput;
        this.controller = controller;
        this.drivetype = driveType;
        this.carTran = carTran;
        this.driftForcesData = driftForcesData;
    }
    public bool GetDriftState()
    {
        return isDrifting;
    }
    public void ApplyDrift(bool driftinput, float steeringInput , float accelinput)
    {
        speed = controller.Speed;
  
        // bool handBrake = Input.GetKey(KeyCode.H);

        // Adjust wheel friction when handbrake is being used
        if (driftinput)
        {
            
            isDrifting = true;
            carRb.angularDrag = 2f;
            float driftFriction = GetDriftFactor() * handBrakeFrictionMultiplier;

            for (int i = 0; i < 4; i++)
            {
                // colliders.GetWheelCollider(0).motorTorque*=1.5f;
                // colliders.GetWheelCollider(1).motorTorque*=1.5f;
                WheelCollider wheel = colliders.GetWheelCollider(i);

                // Set friction values for rear wheels
                wheel.sidewaysFriction = new WheelFrictionCurve
                {
                    extremumSlip = 0.2f,
                    extremumValue = driftFriction,
                    asymptoteValue = driftFriction,
                    asymptoteSlip = 0.5f,
                    stiffness = wheel.sidewaysFriction.stiffness
                };
                wheel.forwardFriction = new WheelFrictionCurve
                {
                    extremumSlip = 0.4f,
                    extremumValue = driftFriction + 0.7f,
                    asymptoteValue = driftFriction + 0.05f,
                    asymptoteSlip = 0.8f,
                    stiffness = wheel.forwardFriction.stiffness
                };

                //Set friction values for front wheels
                // the only work when you but the all the wheel colliders in a parwnt game object and that game objcet is in th center of the car

                if (wheel.transform.localPosition.z > 0)
                {
                    wheel.sidewaysFriction = new WheelFrictionCurve
                    {
                        extremumSlip = 0.2f,
                        extremumValue = 1.1f,
                        asymptoteValue = 1.1f,
                        asymptoteSlip = 0.5f,
                        stiffness = wheel.sidewaysFriction.stiffness
                    };
                    wheel.forwardFriction = new WheelFrictionCurve
                    {
                        extremumSlip = 0.4f,
                        extremumValue = 1.1f,
                        asymptoteValue = 1.1f,
                        asymptoteSlip = 0.8f,
                        stiffness = wheel.forwardFriction.stiffness
                    };
                }
            }

            // Apply force to vehicle in forward direction based on speed so it dont stop 
            if (speed != 0)
            {
                ApplyDriftForces(steeringInput,accelinput);
                
            }


            
        }

        else
        {
            isDrifting = false;
            carRb.angularDrag = 0.001f;

            float gripFriction = ((speed * handBrakeFrictionMultiplier) / 300) + 1;

            switch (drivetype)
            {
                case DriveType.CarWithHandbreak:
                    for (int i = 0; i < 4; i++)
                    {
                        WheelCollider wheel = colliders.GetWheelCollider(i);

                        wheel.sidewaysFriction = new WheelFrictionCurve
                        {
                            extremumSlip = 0.2f,
                            extremumValue = gripFriction ,
                            asymptoteValue = gripFriction ,
                            //  asymptoteValue = 0.75f,
                            asymptoteSlip = 0.5f,
                            stiffness = wheel.sidewaysFriction.stiffness
                        };

                        wheel.forwardFriction = new WheelFrictionCurve
                        {
                            extremumSlip = 0.4f,
                            extremumValue = gripFriction,
                            // asymptoteValue = 0.5f,
                            asymptoteValue = gripFriction,
                            asymptoteSlip = 0.8f,
                            stiffness = wheel.forwardFriction.stiffness
                        };
                    }
                    break;
                case DriveType.DriftCar:
                    for (int i = 0; i < 2; i++)
                    {
                        WheelCollider wheel = colliders.GetWheelCollider(i);

                        wheel.sidewaysFriction = new WheelFrictionCurve
                        {
                            extremumSlip = 0.2f,
                            extremumValue = gripFriction,
                            asymptoteValue = gripFriction,

                            stiffness = wheel.sidewaysFriction.stiffness
                        };

                        wheel.forwardFriction = new WheelFrictionCurve
                        {
                            extremumSlip = 0.4f,
                            extremumValue = gripFriction,
                            asymptoteValue = gripFriction,
                            stiffness = wheel.forwardFriction.stiffness
                        };
                    }
                    for (int i = 2; i < 4; i++)
                    {
                        WheelCollider wheel = colliders.GetWheelCollider(i);

                        wheel.sidewaysFriction = new WheelFrictionCurve
                        {
                            extremumSlip = 0.2f,
                            extremumValue = gripFriction,
                            asymptoteValue = gripFriction,
                            asymptoteSlip = 0.5f,
                            stiffness = 1
                        };

                        wheel.forwardFriction = new WheelFrictionCurve
                        {
                            extremumSlip = 0.4f,
                            extremumValue = gripFriction,
                            asymptoteValue = gripFriction,
                            asymptoteSlip = 0.8f,
                            stiffness = 1
                        };
                    }
                    break;
                default:
                    break;
            }
        }

    }

    private void ApplyDriftForces(float steeringInput, float accelinput)
    {
        if(accelinput>0.15){
            float maxForwardForce = driftForcesData.maxForwardForce; 
            float minForwardForce = driftForcesData.minForwardForce; 
            float forwardForce = Mathf.Lerp(minForwardForce, maxForwardForce, speed / 100f);

        
            Vector3 forwardDir = carTran.forward;
            carRb.AddForce(forwardForce * forwardDir.normalized, ForceMode.Force);


            Vector3 sidewaysDir = -steeringInput * carTran.right;

            float maxSidewaysForce = driftForcesData.maxSidewaysForce; 
            float minSidewaysForce = driftForcesData.minSidewaysForce; 
            float sidewaysForce = Mathf.Lerp(maxSidewaysForce, minSidewaysForce, speed / 100f);

            //for a better Ta7meees lock the max steering to 25 or 30 as max if the speed is less than 21 
            //maybe add a forcemultiplyier if still at the pos for moer than 3sec and the steering is less than 30

            carRb.AddForceAtPosition(sidewaysForce * sidewaysDir.normalized, colliders.GetWheelCollider(3).transform.position, ForceMode.Force);
        }else if(accelinput<-0.5){
            
            Vector3 sidewaysDir = -steeringInput * carTran.right;

            float maxSidewaysForce = driftForcesData.maxSidewaysForce*1.25f; 
            float minSidewaysForce = driftForcesData.minSidewaysForce; 
            float sidewaysForce = Mathf.Lerp(maxSidewaysForce, minSidewaysForce, -speed / 100f);

            carRb.AddForceAtPosition(sidewaysForce * sidewaysDir.normalized, colliders.GetWheelCollider(0).transform.position, ForceMode.Force);


        }
    }

    public float GetDriftFactor()
    {
        float driftFactor = 0;
        for (int i = 2; i < 4; i++)
        {
            WheelCollider wheel = colliders.GetWheelCollider(i);
            WheelHit wheelHit;
            if (wheel.GetGroundHit(out wheelHit))
            {
                if (wheelHit.sidewaysSlip < 0)
                {
                    driftFactor = (1 - steerInput) * Mathf.Abs(wheelHit.sidewaysSlip);
                }
                else if (wheelHit.sidewaysSlip > 0)
                {
                    driftFactor = (1 + steerInput) * Mathf.Abs(wheelHit.sidewaysSlip);
                }
            }
        }
        return driftFactor;
    }







}