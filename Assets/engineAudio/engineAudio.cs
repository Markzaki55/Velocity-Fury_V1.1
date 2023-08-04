
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineAudio : MonoBehaviour{

    //eq
    private GameObject audioObject;
    private SEF_Equalizer eq;

    [Range(0,1)]public float startOffValue = 0.35f;

    public float Load;
    public float loadLerpSpeed = 15;

    public AudioClip lowAccelClip;                                              
    public AudioClip lowDecelClip;                                              
    public AudioClip highAccelClip;                                             
    public AudioClip highDecelClip; 

    public AudioClip MaxRpmSound;
    [Header("Tubro Sound")]
    public AudioClip Turbo;   
    [Range(0,2)]public float turboVolume;  


    [Header("Pitch")]
    [Range(0.5f,1)]public float Pitch = 1f;                              
    [Range(.5f,3)]public float lowPitchMin = 1f;                                              
    [Range(2,7)]public float lowPitchMax = 6f;                                              
    [Range(0,1)]public float highPitchMultiplier = 0.25f;       
    [Range(0,1)]public float pitchMultiplier = 1f;                              
   

    private float accFade = 0;
    private float acceleration;
    private float maxRolloffDistance = 500;                                      
    private AudioSource m_LowAccel; 
    private AudioSource m_LowDecel; 
    private AudioSource m_HighAccel;
    private AudioSource m_HighDecel;
    private AudioSource m_Turbo;

    private AudioSource MaxRPMSource;


    public MazdaController controller;

    
    [SerializeField] private float maxRPM = 8200f;

    private void Start(){

        m_HighAccel = SetUpEngineAudioSource(highAccelClip);
        m_LowAccel = SetUpEngineAudioSource(lowAccelClip);
        m_LowDecel = SetUpEngineAudioSource(lowDecelClip);
        m_HighDecel = SetUpEngineAudioSource(highDecelClip);

        MaxRPMSource = SetUpEngineAudioSource(MaxRpmSound);
      
        if(Turbo != null)m_Turbo = SetUpEngineAudioSource(Turbo);

        // Create a new game object to hold the SEF_Equalizer component
        audioObject = new GameObject("AudioObject");
        audioObject.transform.SetParent(transform);
        audioObject.transform.localPosition = Vector3.zero;

        // Add the SEF_Equalizer component to the new game object
        eq = audioObject.AddComponent<SEF_Equalizer>();

        controller = transform.root.gameObject.GetComponent<MazdaController>();
        lowPitchMax = (maxRPM/ 1000) / 2;
        
    }

    void filter(){
        Load = accFade;
        //Load =  Mathf.Abs(IN.vertical) ; 
        eq.midFreq = Mathf.Lerp(eq.midFreq , startOffValue + (Load / 1.5f ) ,loadLerpSpeed* Time.deltaTime);
        eq.highFreq = Mathf.Lerp(eq.highFreq , startOffValue + (Load / 1.5f ) , loadLerpSpeed * Time.deltaTime);        
    }


    private void FixedUpdate(){
                    
        accFade = Mathf.Lerp(accFade,Mathf.Abs( acceleration ), 15 * Time.deltaTime );

            if(controller.AccelerateInput ==1)
                acceleration = 1;
            else acceleration = 0;

            float pitch = ULerp(lowPitchMin, lowPitchMax, controller.Rpm / maxRPM);
            pitch = Mathf.Min(lowPitchMax, pitch);
            m_LowAccel.pitch = pitch*pitchMultiplier;
            m_LowDecel.pitch = pitch*pitchMultiplier;
            m_HighAccel.pitch = pitch*highPitchMultiplier*pitchMultiplier;
            m_HighDecel.pitch = pitch*highPitchMultiplier*pitchMultiplier;
          //  MaxRPMSource.pitch = pitch*highPitchMultiplier;

            float decFade = 1 - accFade;
            float highFade = Mathf.InverseLerp(0.2f, 0.8f, controller.Rpm / maxRPM);
            float lowFade = 1 - highFade;
            
            highFade = 1 - ((1 - highFade)*(1 - highFade));
            lowFade = 1 - ((1 - lowFade)*(1 - lowFade));
            //accFade = 1 - ((1 - accFade)*(1 - accFade));
            decFade = 1 - ((1 - decFade)*(1 - decFade));
            m_LowAccel.volume = lowFade*accFade;
            m_LowDecel.volume = lowFade*decFade;
            m_HighAccel.volume =(controller.Rpm<maxRPM-180)? highFade*accFade:0;
            m_HighDecel.volume = highFade*decFade;
            MaxRPMSource.volume = (controller.Rpm> maxRPM-200)?1:0f; 
                
            
        filter();
    }

    private AudioSource SetUpEngineAudioSource(AudioClip clip){
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.spatialBlend = 1;
        source.loop = true;
        source.dopplerLevel = 0;
        source.time = Random.Range(0f, clip.length);
        source.Play();
        source.minDistance = 5;
        source.maxDistance = maxRolloffDistance;
        return source;
    }


    private float ULerp(float from, float to, float value){
        return (1.0f - value)*from + value*to;
    }
}
