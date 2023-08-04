using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InzializePrices : MonoBehaviour
{
   [SerializeField] int[] carPrices;
    [SerializeField] bool[] Lockedcars;
    [SerializeField ] int currency;
    [SerializeField] int CurrentCar;
    private void Awake() {
        SaveManger.Load();
    }
    private void Update() {
        currency = SaveManger.currency;
       CurrentCar = SaveManger.currentCar;
        carPrices = SaveManger.carPrices;
        Lockedcars = SaveManger.carsUnlocked;
    }
}
