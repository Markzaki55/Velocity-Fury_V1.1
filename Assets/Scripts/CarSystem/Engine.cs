
using UnityEngine;
interface IEngine{
    
    int CalculateRPM( );
    int CalculateSpeed();
    float GetDriveTorque();
    void  Accelerate(float accelerateInput);
}

class Engine : IEngine{
     public CarDriveType carDriveType;
     public CarWheelColliders colliders;
     float accelerateInput;
     float enginePower;
     GearBox gearbox;
     Rigidbody rb;
     float driveTorque;


     float rpm;
     public float idlerpm=800;
     float HighestRpm = 8100;
     AnimationCurve TourqeCurve;
     MazdaController Controller;


    public Engine(float enginePower, CarDriveType carDriveType, CarWheelColliders colliders, GearBox gearbox, float accelerateInput,Rigidbody rb,AnimationCurve tourqeCurve,MazdaController controller)
{
    this.Controller = controller;
    this.enginePower = enginePower;
    this.carDriveType = carDriveType;
    this.colliders = colliders;
    this.gearbox = gearbox;
    this.accelerateInput = accelerateInput;
    this.rb =rb;
    this.TourqeCurve = tourqeCurve;


   
}
 float calcTourqe ()  {
    return (TourqeCurve.Evaluate(CalculateRPM() / HighestRpm) * enginePower / CalculateRPM()) * gearbox.GetCurrentGearRatio() * gearbox.GetFinalDriveRatio() * 5252f /4 ;
    
 }

   public void Accelerate(float accelerateInput)
    {
       //AdjustPowerBasedOnWheelSpin();
       EventManger.RpmChangeEvent(CalculateRPM());
       EventManger.SpeedChangeEvent(CalculateSpeed());
        
                
                driveTorque = calcTourqe () * accelerateInput;
        switch (carDriveType)
        {
            case CarDriveType.FrontWheelDrive:
                colliders.FRWheel.motorTorque = driveTorque;
                colliders.FlWheel.motorTorque = driveTorque;
                break;

            case CarDriveType.RearWheelDrive:
                colliders.RlWheel.motorTorque = driveTorque;
                colliders.RRWheel.motorTorque = driveTorque;
                break;

            case CarDriveType.AllWheelDrive:
               
                colliders.FRWheel.motorTorque = driveTorque/2;
                colliders.FlWheel.motorTorque = driveTorque/2;
                colliders.RlWheel.motorTorque = driveTorque/2;
                colliders.RRWheel.motorTorque = driveTorque/2;
                break;
        }
        
    }
    void AdjustPowerBasedOnWheelSpin()
{
    if (gearbox.GetCurrentGearNumber() == (1 | 2) && Mathf.Abs(colliders.RRWheel.rpm) > 2000f)
    {
        enginePower *= 0f;
        rb.AddForce(rb.gameObject.transform.forward *1000f);
    }
}
    public float GetDriveTorque(){
        return  driveTorque;
    }
public int CalculateRPM()
{
    float wheelSpeed = Mathf.Abs((colliders.FRWheel.rpm + colliders.FlWheel.rpm + colliders.RlWheel.rpm + colliders.RRWheel.rpm) / 6f);
    float gearRatio = gearbox.GetCurrentGearRatio();
    float finalDriveRatio = gearbox.GetFinalDriveRatio();
    float engineRPM = (wheelSpeed * gearRatio  * finalDriveRatio) ;
    rpm = Mathf.Lerp(rpm, Mathf.Max(idlerpm , engineRPM), Time.deltaTime * 3f);
    float rpmclamp = Mathf.Clamp(rpm,600,8300);
    return Mathf.RoundToInt(rpmclamp);
}


public int CalculateSpeed()
{
    return Mathf.RoundToInt(rb.velocity.magnitude*3.6f);
}


}

