using UnityEngine;

public interface ISteeringWheel
{
    void Steer(CarWheelColliders Coliders, float steerAngle, float steerInput);
    
}

public class SteeringWheel : ISteeringWheel
{
    private Steeringtype Steertype;
    public SteeringWheel(Steeringtype steertype)
    {
        this.Steertype = steertype;
    }


    

    public void Steer(CarWheelColliders Coliders, float steerAngle, float steerInput)
    {
        switch (Steertype)
        {
            case Steeringtype.FrontWheelSteer:
                Coliders.FlWheel.steerAngle = steerAngle;
                Coliders.FRWheel.steerAngle = steerAngle;
                break;
            case Steeringtype.allWheelSteer:
                Coliders.FlWheel.steerAngle = steerAngle;
                Coliders.FRWheel.steerAngle = steerAngle;
                Coliders.RlWheel.steerAngle = steerAngle / 6;
                Coliders.RRWheel.steerAngle = steerAngle / 6;
                break;
            default:
                Coliders.FlWheel.steerAngle = steerAngle;
                Coliders.FRWheel.steerAngle = steerAngle;
                break;
        }



    }

}