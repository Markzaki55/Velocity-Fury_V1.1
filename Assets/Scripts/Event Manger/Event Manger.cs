using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManger 
{
    public static event Action<int> onGearChange;
    public static event Action<int> OnRpmChange;
    public static event Action<float> OnSpeedChange;
   // public static event Action<float> OnAngleChange;

    public static void GearChangeEvent(int GearNum){

        onGearChange?.Invoke(GearNum);
    }
   public static void RpmChangeEvent(int RpmNum){

        OnRpmChange?.Invoke(RpmNum);
    }
    public static void SpeedChangeEvent(int speed){

        OnSpeedChange?.Invoke(speed);
    }
}
