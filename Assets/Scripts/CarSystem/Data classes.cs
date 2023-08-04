using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarWheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FlWheel;
    public WheelCollider RRWheel;
    public WheelCollider RlWheel;
    public WheelCollider GetWheelCollider(int index)
    {
        return index switch
        {
            0 => FRWheel,
            1 => FlWheel,
            2 => RRWheel,
            3 => RlWheel,
            _ => null,
        };
    }
    public bool CheckForWheelSpin(float SlipLimit)
    {
        bool isSlipping = false;
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelHit;
            GetWheelCollider(i).GetGroundHit(out wheelHit);

            isSlipping = Mathf.Abs(wheelHit.sidewaysSlip) + Mathf.Abs(wheelHit.forwardSlip) > SlipLimit;

        }
        return isSlipping;
    }

}

[System.Serializable]
public class CarWheelTeansForm
{
    public Transform FRWheelP;
    public Transform FlWheelP;
    public Transform RRWheelP;
    public Transform RlWheelP;
    public Transform GetWheelMesh(int index)
    {
        return index switch
        {
            0 => FRWheelP,
            1 => FlWheelP,
            2 => RRWheelP,
            3 => RlWheelP,
            _ => null,
        };
    }
}

[System.Serializable]
public class DriftForcesData
{
    public float maxForwardForce = 800f;
    public float minForwardForce = 0f;

    public float maxSidewaysForce = 11000f;
    public float minSidewaysForce = 500f;
}
public enum CounterSteer{
    onDrifting,
    onNormal,
    noCounterSteer,
}
 public enum Steeringtype {
    FrontWheelSteer,
    allWheelSteer
 }

public enum Driver
{
    Player,
    Ai
}
public enum CarAnim
{
    rotatemessEngineOnX,
    rotatemessEngineOnZ,
}
public enum CarDriveType
{
    FrontWheelDrive,
    RearWheelDrive,
    AllWheelDrive
}
enum DriveMode
{
    auto,
    Manual
}
enum DriveType
{
    CarWithHandbreak,
    DriftCar
}