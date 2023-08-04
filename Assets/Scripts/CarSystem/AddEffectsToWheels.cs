using UnityEngine;

public class AddEffectsToWheels {
    private CarWheelColliders colliders;
    private float sliplimit;
    private GameObject skidTrailPrefab;
    private GameObject smokePrefab;

    private GameObject[] skidTrailObjects;
    private ParticleSystem[] smokeParticles;
    private TrailRenderer[] skidcomponent;
    

    public AddEffectsToWheels(CarWheelColliders colliders, GameObject skidTrailPrefab, GameObject smokePrefab, float sliplimit) {
        this.colliders = colliders;
        this.skidTrailPrefab = skidTrailPrefab;
        this.smokePrefab = smokePrefab;
        this.sliplimit = sliplimit;

        skidTrailObjects = new GameObject[4];
        smokeParticles = new ParticleSystem[4];
        skidcomponent = new TrailRenderer[4];
    }

    public void Initialize() {
        for (int i = 0; i < 4; i++) {
            skidTrailObjects[i] = GameObject.Instantiate(skidTrailPrefab);
            skidTrailObjects[i].transform.parent = colliders.GetWheelCollider(i).transform;
            skidTrailObjects[i].transform.localPosition = new Vector3(0, -colliders.GetWheelCollider(i).radius, 0);

            smokeParticles[i] = GameObject.Instantiate(smokePrefab).GetComponent<ParticleSystem>();
            smokeParticles[i].transform.parent = colliders.GetWheelCollider(i).transform;
            smokeParticles[i].transform.localPosition = Vector3.zero;
            smokeParticles[i].transform.localRotation = Quaternion.identity;

            skidcomponent[i] = skidTrailObjects[i].GetComponent<TrailRenderer>();
        }
    }

    public void UpdateEffects() {
        bool isSlipping = colliders.CheckForWheelSpin(sliplimit);

        for (int i = 0; i < 4; i++) {
            if (isSlipping) {
                smokeParticles[i].gameObject.SetActive(true);
                smokeParticles[i].Play();
                skidcomponent[i].emitting = true;
                
            } else {
                smokeParticles[i].Stop();
                skidcomponent[i].emitting = false;
                
            }
        }
    }
}