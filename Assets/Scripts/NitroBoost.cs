using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroBoost : MonoBehaviour
{
    [SerializeField] float nitroValue;
    [SerializeField] float nitroMaxValue;
    bool isBoosting;
    [SerializeField] GameObject[] Boosters = new GameObject[1];
    ParticleSystem[] bossterEffects;
    Rigidbody CarRb;
    [SerializeField] float BoostPower;
    AudioSource nitroAudio;

    bool BoostKey;


    private void Awake()
    {

    }
    void Start()
    {
        CarRb = GetComponent<Rigidbody>();
        nitroAudio = GameObject.FindGameObjectWithTag("NitroAudio").GetComponent<AudioSource>();
        bossterEffects = new ParticleSystem[Boosters.Length];
        for (int i = 0; i < Boosters.Length; i++)
        {
            bossterEffects[i] = Boosters[i].GetComponent<ParticleSystem>();
        }


    }
    public  float GetNitroValue()
    {
        return nitroValue;
    }


    void Update()
    {
        BoostKey = Input.GetKey(KeyCode.LeftShift);
        UseNitro();
        nitroEffects();
        Debug.Log(nitroValue);




    }

    void UseNitro()
{
    if (!BoostKey || nitroValue <= 5f)
    {
        isBoosting = false;
        nitroValue += (nitroValue >= nitroMaxValue) ? 0 : Time.deltaTime / 2;
    }
    else if (BoostKey && nitroValue > 5f)
    {
        isBoosting = true;
        CarRb.AddForce(transform.forward * BoostPower);
        nitroValue -= Time.deltaTime / 3;
    }
}
    void nitroEffects()
    {
        if(isBoosting){
        foreach (ParticleSystem boosterEffect in bossterEffects)
            {
                if (!boosterEffect.isPlaying)
                {
                    nitroAudio.Play();
                    boosterEffect.Play();
                }
            }
    }
    else if(!isBoosting){
         foreach (ParticleSystem boosterEffect in bossterEffects)
            {
                if (boosterEffect.isPlaying)
                {
                    nitroAudio.Stop();
                    boosterEffect.Stop();
                }
            }
    }

    }
  

}
