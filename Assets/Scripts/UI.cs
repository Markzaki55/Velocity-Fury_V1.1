using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

class UI : MonoBehaviour{
    public TextMeshProUGUI rpmText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI steerAngle;
    public TextMeshProUGUI DriveTorqe;
    public TextMeshProUGUI gearnum;
    public MazdaController controller;

    int gearNum;
    int rpm;
     float speed;

    public Image Rpmneedle;
    public float minRPM = 600f; 
    public float maxRPM = 8200f; 
    NitroBoost nitro;
    [SerializeField] Slider nitroSlider;
    private void Start() {
        controller= GameObject.FindAnyObjectByType<MazdaController>();
        nitro = controller.gameObject.GetComponent<NitroBoost>();
        
    }
    private void OnEnable() {
        EventManger.OnSpeedChange +=HandleSpeed;
        EventManger.OnRpmChange+=HandleRpm;
        EventManger.onGearChange += HandleGearNum;
    }

    private void HandleSpeed(float Speed)
    {
        speed = Speed;
    }

    private void HandleRpm(int Rpm)
    {
        rpm = Rpm;
    }

    private void OnDisable() {
           EventManger.OnSpeedChange -=HandleSpeed;
        EventManger.onGearChange -= HandleGearNum;
         EventManger.OnRpmChange-=HandleRpm;
    }
    

    private void HandleGearNum(int GearNum)
    {
        gearNum = GearNum;
        
    }

    private void Update() {
    int angle = controller.IntsteerAnglel;
    //   int speed = controller.Speed;
     //  int gearNum = controller.GearNum;
       float Tourqe = Mathf.RoundToInt( controller.DriveTourqe);
//       float nitroValue = nitro.GetNitroValue();
       rpmText.text = $"rpm= {rpm}";
       speedText .text = $"{speed}";
       steerAngle.text = $"angle :{angle}";
       DriveTorqe.text = $"Tourqe :{Tourqe}";
       gearnum.text = $"GearNum:{gearNum} ";

     float Needleangle = -Mathf.Lerp(0f, 180f, Mathf.InverseLerp(minRPM, maxRPM, rpm));

        // Rotate the RPM needle based on the mapped angle
        Rpmneedle.rectTransform.rotation = Quaternion.Euler(0f, 0f, Needleangle);


       // nitroSlider.value = nitroValue/ 10f;

        
    }
}