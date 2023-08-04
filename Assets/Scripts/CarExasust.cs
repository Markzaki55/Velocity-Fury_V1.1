// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarExasust : MonoBehaviour
// {
//     [SerializeField] MazdaController controller;
//    public AudioSource maxRpmSound;
// public AudioSource increasingHighSound;
// public AudioSource decreasingHighSound;
// public AudioSource increasingLowSound;
// public AudioSource decreasingLowSound;
// public AudioSource idleSound;
// public float minRPM = 400f;
// public float maxRPM = 8200f;
// public float highRPM = 7900f;
// public float lowRPM = 1100f;

// private float previousRPM;

// private void Update()
// {
//     float currentRPM = controller.Rpm;

//     if (currentRPM > maxRPM)
//     {
//         PlayMaxRpmSound();
//     }
//     else if (currentRPM > highRPM)
//     {
//         if (currentRPM > previousRPM)
//         {
//             PlayIncreasingHighSound();
//         }
//         else
//         {
//             PlayDecreasingHighSound();
//         }
//     }
//     else if (currentRPM > lowRPM)
//     {
//         if (currentRPM > previousRPM)
//         {
//             PlayIncreasingLowSound();
//         }
//         else
//         {
//             PlayDecreasingLowSound();
//         }
//     }
//     else
//     {
//         PlayIdleSound();
//     }

//     previousRPM = currentRPM;
// }

// private void PlayMaxRpmSound()
// {
//     maxRpmSound.Play();
// }

// private void PlayIncreasingHighSound()
// {
//     increasingHighSound.Play();
//     decreasingHighSound.Stop();
// }

// private void PlayDecreasingHighSound()
// {
//     decreasingHighSound.Play();
//     increasingHighSound.Stop();
// }

// private void PlayIncreasingLowSound()
// {
//     increasingLowSound.Play();
//     decreasingLowSound.Stop();
// }

// private void PlayDecreasingLowSound()
// {
//     decreasingLowSound.Play();
//     increasingLowSound.Stop();
// }

// private void PlayIdleSound()
// {
//     idleSound.Play();
//     increasingHighSound.Stop();
//     decreasingHighSound.Stop();
//     increasingLowSound.Stop();
//     decreasingLowSound.Stop();
// }

// private void OnDisable()
// {
//     maxRpmSound.Stop();
//     increasingHighSound.Stop();
//     decreasingHighSound.Stop();
//     increasingLowSound.Stop();
//     decreasingLowSound.Stop();
//     idleSound.Stop();
// }}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarExasust : MonoBehaviour
// {
//     [SerializeField] MazdaController controller;
//     public AudioClip maxRpmClip;
//     public AudioClip increasingHighClip;
//     public AudioClip decreasingHighClip;
//     public AudioClip increasingLowClip;
//     public AudioClip decreasingLowClip;
//     public AudioClip idleClip;
//     public float minRPM = 400f;
//     public float maxRPM = 8200f;
//     public float highRPM = 7600f;
//     public float lowRPM = 1100f;
//     public float minPitch = 0.5f;
//     public float maxPitch = 1.5f;
//     public float pitchLerpSpeed = 5f;

//     private AudioSource audioSource;
//     private float previousRPM;
//     private float targetPitch;

//     private void Start()
//     {
//         audioSource = GetComponent<AudioSource>();
//     }

//     private void Update()
//     {
//         float currentRPM = controller.Rpm;

//         if (currentRPM > maxRPM)
//         {
//             PlayMaxRpmSound();
//             targetPitch = maxPitch;
//         }
//         else if (currentRPM > highRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingHighSound();
//             }
//             else
//             {
//                 PlayDecreasingHighSound();
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, (currentRPM - highRPM) / (maxRPM - highRPM));
//         }
//         else if (currentRPM > lowRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingLowSound();
//             }
//             else
//             {
//                 PlayDecreasingLowSound();
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, (currentRPM - lowRPM) / (highRPM - lowRPM));
//         }
//         else
//         {
//             PlayIdleSound();
//             targetPitch = minPitch;
//         }

//         previousRPM = currentRPM;

//         audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * pitchLerpSpeed);
//     }

//     private void PlayMaxRpmSound()
//     {
//         audioSource.clip = maxRpmClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayIncreasingHighSound()
//     {
//         audioSource.clip = increasingHighClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayDecreasingHighSound()
//     {
//         audioSource.clip = decreasingHighClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayIncreasingLowSound()
//     {
//         audioSource.clip = increasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayDecreasingLowSound()
//     {
//         audioSource.clip = decreasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayIdleSound()
//     {
//         audioSource.clip = idleClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void OnDisable()
//     {
//         audioSource.Stop();
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarExasust : MonoBehaviour
// {
//     [SerializeField] MazdaController controller;
//     public AudioClip maxRpmClip;
//     public AudioClip increasingHighClip;
//     public AudioClip decreasingHighClip;
//     public AudioClip increasingLowClip;
//     public AudioClip decreasingLowClip;
//     public AudioClip idleClip;
//     public float minRPM = 400f;
//     public float maxRPM = 8200f;
//     public float highRPM = 7900f;
//     public float medRPM = 4000f;
//     public float lowRPM = 1100f;
//     public float minPitch = 0.5f;
//     public float maxPitch = 1.5f;
//     public float pitchLerpSpeed = 5f;
//     public float delay = 2f;

//     private AudioSource audioSource;
//     private float previousRPM;
//     private float targetPitch;
//     private bool isDelaying;
//     private float delayTime;

//     private void Start()
//     {
//         audioSource = GetComponent<AudioSource>();
//     }

//     private void Update()
//     {
//         float currentRPM = controller.Rpm;

//         if (currentRPM > maxRPM)
//         {
//             PlayMaxRpmSound();
//             targetPitch = maxPitch;
//         }
//         else if (currentRPM > highRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingHighSound();
//             }
//             else
//             {
//                 if (isDelaying)
//                 {
//                     PlaySilence();
//                 }
//                 else
//                 {
//                     PlayDecreasingHighSound();
//                 }
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, (currentRPM - highRPM) / (maxRPM - highRPM));
//         }
//         else if (currentRPM > medRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingMedSound();
//             }
//             else
//             {
//                 if (isDelaying)
//                 {
//                     PlaySilence();
//                 }
//                 else
//                 {
//                     PlayDecreasingMedSound();
//                 }
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, (currentRPM - medRPM) / (highRPM - medRPM));
//         }
//         else if (currentRPM > lowRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingLowSound();
//             }
//             else
//             {
//                 if (isDelaying)
//                 {
//                     PlaySilence();
//                 }
//                 else
//                 {
//                     PlayDecreasingLowSound();
//                 }
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, (currentRPM - lowRPM) / (medRPM - lowRPM));
//         }
//         else
//         {
//             PlayIdleSound();
//             targetPitch = minPitch;
//         }

//         previousRPM = currentRPM;

//         audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * pitchLerpSpeed);
//     }

//     private void PlayMaxRpmSound()
//     {
//         audioSource.clip = maxRpmClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayIncreasingHighSound()
//     {
//         audioSource.clip = increasingHighClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = true;
//         delayTime = Time.time + delay;
//     }

//     private void PlayDecreasingHighSound()
//     {
//         audioSource.clip = decreasingHighClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlayIncreasingMedSound()
//     {
//         audioSource.clip = increasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = true;
//         delayTime = Time.time + delay;
//     }

//     private void PlayDecreasingMedSound()
//     {
//         audioSource.clip = decreasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlayIncreasingLowSound()
//     {
//         audioSource.clip = increasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = true;
//         delayTime = Time.time + delay;
//     }

//     private void PlayDecreasingLowSound()
//     {
//         audioSource.clip = decreasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlayIdleSound()
//     {
//         audioSource.clip = idleClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlaySilence()
//     {
//         audioSource.clip = null;
//         isDelaying = false;    
//     }

//     // You can also add a public method that allows you to change the delay time at runtime
//     public void SetDelay(float newDelay)
//     {
//         delay = newDelay;
//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarExasust : MonoBehaviour
// {
//     #region Audio Clips
//     [SerializeField] private AudioClip maxRpmClip;
//     [SerializeField] private AudioClip increasingHighClip;
//     [SerializeField] private AudioClip decreasingHighClip;
//     [SerializeField] private AudioClip increasingLowClip;
//     [SerializeField] private AudioClip decreasingLowClip;
//     [SerializeField] private AudioClip idleClip;
//     #endregion

//     #region RPM Settings
//     [SerializeField] private MazdaController controller;
//     [SerializeField] private float minRPM = 400f;
//     [SerializeField] private float maxRPM = 8200f;
//     [SerializeField] private float highRPM = 7900f;
//     [SerializeField] private float medRPM = 4000f;
//     [SerializeField] private float lowRPM = 1100f;
//     [SerializeField, Range(0.5f, 1.5f)] private float minPitch = 0.5f;
//     [SerializeField, Range(0.5f, 1.5f)] private float maxPitch = 1.5f;
//     [SerializeField] private float pitchLerpSpeed = 5f;
//     [SerializeField] private float delay = 2f;
//     #endregion

//     private AudioSource audioSource;
//     private float previousRPM;
//     private float targetPitch;
//     private bool isDelaying;
//     private float delayTime;

//     private void Start()
//     {
//         audioSource = GetComponent<AudioSource>();
//     }

//     private void Update()
//     {
//         float currentRPM = controller.Rpm;

//         if (currentRPM > maxRPM)
//         {
//             PlayMaxRpmSound();
//             targetPitch = maxPitch;
//         }
//         else if (currentRPM > highRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingHighSound();
//             }
//             else
//             {
//                 if (isDelaying)
//                 {
//                     PlaySilence();
//                 }
//                 else
//                 {
//                     PlayDecreasingHighSound();
//                 }
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(highRPM, maxRPM, currentRPM));
//         }
//         else if (currentRPM > medRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingMedSound();
//             }
//             else
//             {
//                 if (isDelaying)
//                 {
//                     PlaySilence();
//                 }
//                 else
//                 {
//                     PlayDecreasingMedSound();
//                 }
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(medRPM, highRPM, currentRPM));
//         }
//         else if (currentRPM > lowRPM)
//         {
//             if (currentRPM > previousRPM)
//             {
//                 PlayIncreasingLowSound();
//             }
//             else
//             {
//                 if (isDelaying)
//                 {
//                     PlaySilence();
//                 }
//                 else
//                 {
//                     PlayDecreasingLowSound();
//                 }
//             }
//             targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(lowRPM, medRPM, currentRPM));
//         }
//         else
//         {
//             PlayIdleSound();
//             targetPitch = minPitch;
//         }

//         previousRPM = currentRPM;

//         audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * pitchLerpSpeed);
//     }

//     private void PlayMaxRpmSound()
//     {
//         audioSource.clip = maxRpmClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//     }

//     private void PlayIncreasingHighSound()
//     {
//         audioSource.clip = increasingHighClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = true;
//         delayTime = Time.time+ delay;
//     }

//     private void PlayDecreasingHighSound()
//     {
//         audioSource.clip = decreasingHighClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlayIncreasingMedSound()
//     {
//         audioSource.clip = increasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = true;
//         delayTime = Time.time + delay;
//     }

//     private void PlayDecreasingMedSound()
//     {
//         audioSource.clip = decreasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlayIncreasingLowSound()
//     {
//         audioSource.clip = increasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = true;
//         delayTime = Time.time + delay;
//     }

//     private void PlayDecreasingLowSound()
//     {
//         audioSource.clip = decreasingLowClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlayIdleSound()
//     {
//         audioSource.clip = idleClip;
//         if (!audioSource.isPlaying) audioSource.Play();
//         isDelaying = false;
//     }

//     private void PlaySilence()
//     {
//         audioSource.clip = null;
//         isDelaying = false;
//     }

//     // You can also add a public method that allows you to change the delay time at runtime
//     public void SetDelay(float newDelay)
//     {
//         delay = newDelay;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExasust : MonoBehaviour
{
    #region Audio Clips
    [SerializeField] private AudioClip maxRpmClip;
    [SerializeField] private AudioClip increasingHighClip;
    [SerializeField] private AudioClip decreasingHighClip;
    [SerializeField] private AudioClip increasingLowClip;
    [SerializeField] private AudioClip decreasingLowClip;
    [SerializeField] private AudioClip idleClip;
    #endregion

    #region RPM Settings
    [SerializeField] private MazdaController controller;
    [SerializeField] private float minRPM = 400f;
    [SerializeField] private float maxRPM = 8200f;
    [SerializeField] private float highRPM = 7900f;
    [SerializeField] private float medRPM = 4000f;
    [SerializeField] private float lowRPM = 1100f;
    [SerializeField, Range(0.5f, 1.5f)] private float minPitch = 0.5f;
    [SerializeField, Range(0.5f, 1.5f)] private float maxPitch = 1.5f;
    [SerializeField] private float pitchLerpSpeed = 5f;
    [SerializeField] private float delay = 2f;
    #endregion

    private AudioSource audioSource;
    private float previousRPM;
    private float targetPitch;
    private bool isDelaying;
    private float delayTime;
    float accelInput;

    private void Start()
    {
        accelInput = controller.AccelerateInput;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float currentRPM = controller.Rpm;

        if (currentRPM > maxRPM)
        {
            PlayMaxRpmSound();
           // targetPitch = maxPitch;
        }
        else if (currentRPM > highRPM)
        { 
                PlayIncreasingHighSound();
                if(accelInput == 0){
                    PlayDecreasingHighSound();
                }
            
            targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(highRPM, maxRPM, currentRPM));
        }
        else if (currentRPM > medRPM)
        {
            
            
                PlayIncreasingHighSound();
                if(accelInput == 0){
                    PlayDecreasingHighSound();
                }
            
            
            targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(medRPM, highRPM, currentRPM));
        }
        else if (currentRPM > lowRPM)
        {
            
            
                PlayIncreasingLowSound();
                if(accelInput == 0){
                    PlayDecreasingLowSound();
                }
            
            
            targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(lowRPM, medRPM, currentRPM));
        }
        else
        {
            PlayIdleSound();
            targetPitch = minPitch;
        }

        //previousRPM = currentRPM;

        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * pitchLerpSpeed);
    }

    private void PlayMaxRpmSound()
    {
        audioSource.clip = maxRpmClip;
        if (!audioSource.isPlaying) audioSource.Play();
    }

    private void PlayIncreasingHighSound()
    {
        audioSource.clip = increasingHighClip;
        if (!audioSource.isPlaying) audioSource.Play();
        isDelaying = true;
        delayTime = Time.time+ delay;
    }

    private void PlayDecreasingHighSound()
    {
        audioSource.clip = decreasingHighClip;
        if (!audioSource.isPlaying) audioSource.Play();
        isDelaying = false;
    }

    private void PlayIncreasingLowSound()
    {
        audioSource.clip = increasingLowClip;
        if (!audioSource.isPlaying) audioSource.Play();
        isDelaying = true;
        delayTime = Time.time + delay;
    }

    private void PlayDecreasingLowSound()
    {
        audioSource.clip = decreasingLowClip;
        if (!audioSource.isPlaying) audioSource.Play();
        isDelaying = false;
    }

    private void PlayIdleSound()
    {
        audioSource.clip = idleClip;
        if (!audioSource.isPlaying) audioSource.Play();
        isDelaying = false;
    }

    private void PlaySilence()
    {
        audioSource.clip = null;
        isDelaying = false;
    }

    // You can also add a public method that allows you to change the delay time at runtime
    public void SetDelay(float newDelay)
    {
        delay = newDelay;
    }
}