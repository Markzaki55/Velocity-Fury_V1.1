
using UnityEngine;

public class GearBox
{
    private readonly float[] gearRatios;
    private readonly float finalDriveRatio;

    private int currentGear = 0;
    private float currentRatio = 0;

  
    private float timeSinceLastShift = 0f;
    private bool waitingToShiftUp = true;

    public GearBox(float[] gearRatios, float finalDriveRatio)
    {
        this.gearRatios = gearRatios;
        this.finalDriveRatio = finalDriveRatio;
         EventManger.GearChangeEvent(GetCurrentGearNumber());
        currentRatio = gearRatios[currentGear] * finalDriveRatio;
    }

    public void AutoShifting(int currentRPM, float shiftDelay)
    {
        const int SHIFT_UP_RPM_THRESHOLD = 7950;
        const int SHIFT_DOWN_RPM_THRESHOLD = 3800;

        if (currentRPM >= SHIFT_UP_RPM_THRESHOLD)
        {
            if (waitingToShiftUp)
            {
                timeSinceLastShift += Time.deltaTime;
                if (timeSinceLastShift >= shiftDelay)
                {
                    ShiftUp();
                    waitingToShiftUp = false;
                    timeSinceLastShift = 0f;
                }
            }
            else
            {
                waitingToShiftUp = true;
                timeSinceLastShift = 0f;
            }
        }
        else if (currentRPM <= SHIFT_DOWN_RPM_THRESHOLD)
        {
            ShiftDown();
        }
    }
    public void ManualShift()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShiftUp();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ShiftDown();
        }

    }


    public void ShiftUp()
    {
        if (currentGear < gearRatios.Length - 1)
        {
           
            currentGear++;
             EventManger.GearChangeEvent(GetCurrentGearNumber());
            currentRatio = gearRatios[currentGear] * finalDriveRatio;
        }
    }

    public void ShiftDown()
    {
        if (currentGear > 0)
        {
            
            currentGear--;
            EventManger.GearChangeEvent(GetCurrentGearNumber());
            currentRatio = gearRatios[currentGear] * finalDriveRatio;
        }
    }
    public float GetFinalDriveRatio()
    {
        return finalDriveRatio;
    }
    public float GetDriveTorque(float engineTorque)
    {
        return engineTorque * currentRatio;
    }
    public float GetCurrentGearRatio()
    {
        return currentRatio;
    }
    public int GetCurrentGearNumber()
    {
        return currentGear + 1;
    }
}
