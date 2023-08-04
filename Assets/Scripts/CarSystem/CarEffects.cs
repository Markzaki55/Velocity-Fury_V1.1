// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;

// public class CarEffects : MonoBehaviour
// {
//    public Volume volume;
//     public MazdaController carController;
//     public float minSpeed = 90f;
//     public float maxSpeed = 160f;
//     public float intensityAtMaxSpeed = -0.5f;
//     public float intensityAtMinSpeed = 0f;
//     public float lerpSpeed = 0.1f;

//     private LensDistortion lensDistortion;
//     private float targetIntensity;

//     void Start() {
//         carController = GetComponent<MazdaController>();
//         if (volume.profile.TryGet<LensDistortion>(out lensDistortion)) {
//             lensDistortion.intensity.value = intensityAtMinSpeed;
//         }
//     }


//     void Update() {
//     float carSpeed = carController.Speed;

//     if (carSpeed < minSpeed) {
//         targetIntensity = intensityAtMinSpeed;
//     } else if (carSpeed >= minSpeed && carSpeed <= maxSpeed) {
//         float targetIntensityRatio = Mathf.InverseLerp(minSpeed, maxSpeed, carSpeed);
//         targetIntensity = Mathf.Lerp(intensityAtMaxSpeed, intensityAtMinSpeed, targetIntensityRatio);
//     } else {
//         targetIntensity = intensityAtMinSpeed;
//     }

//     if (carSpeed > 90f) {
//         targetIntensity = intensityAtMaxSpeed;
//     }
//     if ( carSpeed >155){
//         targetIntensity = -0.85f;
//     }

//     lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetIntensity, lerpSpeed);
// }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;

// public class CarEffects : MonoBehaviour
// {
//     public Volume volume;
//     public MazdaController carController;

//     private const float MinSpeed = 90f;
//     private const float MaxSpeed = 160f;
//     private const float IntensityAtMaxSpeed = -0.5f;
//     private const float IntensityAtMinSpeed = 0f;
//     private const float MaxIntensity = -0.85f;
//     private const float LerpSpeed = 0.1f;

//     private LensDistortion lensDistortion;
//     private float targetIntensity;

//     private float SpeedThreshold1 => MinSpeed;
//     private float SpeedThreshold2 => MaxSpeed;
//     private float SpeedThreshold3 => 155f;

//     void Start()
//     {
//         carController = GetComponent<MazdaController>();
//         if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
//         {
//             lensDistortion.intensity.value = IntensityAtMinSpeed;
//         }
//     }

//     void Update()
//     {
//         float carSpeed = carController.Speed;

//         if (carSpeed < SpeedThreshold1)
//         {
//             targetIntensity = IntensityAtMinSpeed;
//         }
//         else if (carSpeed >= SpeedThreshold1 && carSpeed < SpeedThreshold2)
//         {
//             float targetIntensityRatio = Mathf.InverseLerp(SpeedThreshold1, SpeedThreshold2, carSpeed);
//             targetIntensity = Mathf.Lerp(IntensityAtMaxSpeed, IntensityAtMinSpeed, targetIntensityRatio);
//         }
//         else if (carSpeed >= SpeedThreshold2 && carSpeed <= SpeedThreshold3)
//         {
//             targetIntensity = IntensityAtMinSpeed;
//         }
//         else if (carSpeed > SpeedThreshold3)
//         {
//             targetIntensity = MaxIntensity;
//         }

//         lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetIntensity, LerpSpeed);
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CarEffects : MonoBehaviour
{
    public Volume volume;
    public MazdaController carController;

    private const float MinSpeed = 90f;
    private const float MaxSpeed = 160f;
    private const float IntensityAtMaxSpeed = -0.5f;
    private const float IntensityAtMinSpeed = 0f;
    private const float MaxIntensity = -0.85f;
    private const float LerpSpeed = 0.1f;

    private LensDistortion lensDistortion;
    private MotionBlur motionBlur;
    private float targetIntensity;
    private float targetMotionBlurIntensity;

    private float SpeedThreshold1 => MinSpeed;
    private float SpeedThreshold2 => MaxSpeed;
    private float SpeedThreshold3 => 155f;

    void Start()
    {
        carController = GetComponent<MazdaController>();
        if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
        {
            lensDistortion.intensity.value = IntensityAtMinSpeed;
        }
        if (volume.profile.TryGet<MotionBlur>(out motionBlur))
        {
            motionBlur.intensity.value = 0f;
        }
    }

    void Update()
    {
        float carSpeed = carController.Speed;

        if (carSpeed < SpeedThreshold1)
        {
            targetIntensity = IntensityAtMinSpeed;
            targetMotionBlurIntensity = 0f;
        }
        else if (carSpeed >= SpeedThreshold1 && carSpeed < SpeedThreshold2)
        {
            float targetIntensityRatio = Mathf.InverseLerp(SpeedThreshold1, SpeedThreshold2, carSpeed);
            targetIntensity = Mathf.Lerp(IntensityAtMaxSpeed, IntensityAtMinSpeed, targetIntensityRatio);
            targetMotionBlurIntensity = Mathf.Lerp(0f, 0.222f, targetIntensityRatio);
        }
        else if (carSpeed >= SpeedThreshold2 && carSpeed <= SpeedThreshold3)
        {
            targetIntensity = IntensityAtMinSpeed;
            targetMotionBlurIntensity = 0f;
        }
        else if (carSpeed > SpeedThreshold3)
        {
            targetIntensity = MaxIntensity;
            targetMotionBlurIntensity = 0.325f;
        }

        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetIntensity, LerpSpeed);
        motionBlur.intensity.value = Mathf.Lerp(motionBlur.intensity.value, targetMotionBlurIntensity, LerpSpeed);
    }
}