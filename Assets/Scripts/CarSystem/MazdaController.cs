using UnityEngine;
using Photon.Pun;


public class MazdaController : MonoBehaviour
{
    PhotonView view;
    [Header("ENUMS")]
    [SerializeField] CounterSteer counterType;
    [SerializeField] Steeringtype steertype;
    [SerializeField] Driver driver;
    [SerializeField] DriveMode driveMode;
    [SerializeField] DriveType driveType;
    [SerializeField] CarAnim carAnim;
    [SerializeField] CarDriveType carDriveType;
    [Header("Data Classes")]
    [SerializeField] private DriftForcesData driftForcesData;
    [SerializeField] private CarWheelColliders wheelColliders;
    [SerializeField] private CarWheelTeansForm wheelMeshesP;
    [Header("Power And Helpers")]
    [SerializeField] private float enginePower;
    [SerializeField] private float maxsteerangle;
    [SerializeField] private float maxBrakeTorque = 2000.0f;
    [SerializeField] private float antiRollVal = 5000f;
    [SerializeField] private float downForce = 50f;

    [SerializeField][Range(0, 1)] private float steerHelper;
    [Header("AnimationCurves")]
    [SerializeField] private AnimationCurve steeringCurve;
    [SerializeField] private AnimationCurve torqueCurve;


    private Rigidbody carRb;
    private ISteeringWheel steeringWheel;
    private float accelerateInput;
    private float steerInput;
    private bool Driftnput;
    private float steerAngle;
    private int IntsteerAngle;
    private float brakeInput;
    private float oldRotation;
    [Header("TheCenterOFMass")]
    [SerializeField] private Vector3 CenterofMass;
    IEngine engine;
    IWheelSync WheelsGS;

    [Header("GearBox")]
    [SerializeField][Range(0.1f, 1.5f)] float AutoShiftDelay = 0.8f;
    [SerializeField] private float[] gearRatios = { 2.66f, 1.78f, 1.30f, 1.00f, 0.74f, 0.50f };
    [SerializeField] private float finalDriveRatio = 3.42f;
    private GearBox gearBox;
    [Header("THE SLip Limit For Spawning trail and smoke ")]
    public float SlipLimit;

    [Header("Drift Multiplier")]
    public float handBrakeFrictionMultiplier = 2f;

    private AddEffectsToWheels effects;

    [Header("Effects Prefabs")]
    [SerializeField] GameObject skidtrail;
    [SerializeField] GameObject smokeprefap;

    public bool isDrifting;
    IDriftSettingHandle driftSetting;



    IInputHandler inputHandler;


    public int Speed => engine.CalculateSpeed();

    public int Rpm => engine.CalculateRPM();
    public float DriveTourqe => engine.GetDriveTorque();
    // public int GearNum => gearBox.GetCurrentGearNumber();
    public int IntsteerAnglel { get => IntsteerAngle; set => IntsteerAngle = value; }
    public float AccelerateInput { get => accelerateInput; set => accelerateInput = value; }

    private void Awake()
    {

        view = GetComponent<PhotonView>();
        if (view == null)
        {
            view = gameObject.AddComponent<PhotonView>();
        }

        inputHandler = (driver == Driver.Player) ? new KeyboardInputHandler() : new AIInputHandler();
        carRb = GetComponent<Rigidbody>();
        effects = new AddEffectsToWheels(wheelColliders, skidtrail, smokeprefap, SlipLimit);
        WheelsGS = new SyncWheelGraphics(wheelColliders, wheelMeshesP, carAnim);

        gearBox = new GearBox(gearRatios, finalDriveRatio);
        engine = new Engine(enginePower, carDriveType, wheelColliders, gearBox, AccelerateInput, carRb, torqueCurve, this);
        steeringWheel = new SteeringWheel(steertype);
        driftSetting = new DriftSetting(wheelColliders, carRb, handBrakeFrictionMultiplier, Speed, this, steerInput, driveType, transform, driftForcesData);
        WheelsGS.SetWheel();


    }
    void shiftinglogic()
    {
        if (driveMode == DriveMode.auto) { gearBox.AutoShifting(Rpm, AutoShiftDelay); }
        if (driveMode == DriveMode.Manual) { gearBox.ManualShift(); }
    }



    private void Start()
    {

        effects.Initialize();
        wheelColliders.GetWheelCollider(0).attachedRigidbody.centerOfMass = CenterofMass;
        carRb = GetComponent<Rigidbody>();


    }
    private void Update()
    {
        if (!view.IsMine) return;
        shiftinglogic();
        GetInput();
        isDrifting = driftSetting.GetDriftState();


    }

    private void FixedUpdate()

    {
        if (!view.IsMine) return;
        WheelsGS.ApplyWheelPose();
        driftSetting.ApplyDrift(Driftnput, steerInput, AccelerateInput);
        AdjustDrag();
        engine.Accelerate(AccelerateInput);
        effects.UpdateEffects();
        ApplyBrakes(brakeInput);
        steeringWheel.Steer(wheelColliders, steerAngle, steerInput);
        SteerHelper();
        AntiRoll();
        AddDownForce();


    }

    void AdjustDrag()
    {
        if (AccelerateInput != 0)
        {
            carRb.drag = 0.005f;
        }
        if (AccelerateInput == 0)
        {
            carRb.drag = 0.1f;
        }
    }



    private void GetInput()
    {
        AccelerateInput = inputHandler.VerticalInput;
        steerInput = inputHandler.HorizontalInput;
        brakeInput = inputHandler.BrakeInput;
        Driftnput = inputHandler.DriftInput;

        steerAngle = steerInput * steeringCurve.Evaluate(Speed);
        //counter Steer for drifit
        //its a life saaaaaver
        // if (Vector3.Dot(carRb.velocity, transform.forward) > 0.19f && isDrifting && Speed>20f) 
        // steerAngle += Vector3.SignedAngle(transform.forward, carRb.velocity + transform.forward, Vector3.up);
        applyCounterSteer();
        steerAngle = Mathf.Clamp(steerAngle, -maxsteerangle, maxsteerangle);
        IntsteerAnglel = Mathf.RoundToInt(steerAngle);


    }


    #region DriveHelpers
    void applyCounterSteer()
    {

        switch (counterType)
        {
            case CounterSteer.onDrifting:
                if (Vector3.Dot(carRb.velocity, transform.forward) > 0.19f && isDrifting && Speed > 20f)
                    steerAngle += Vector3.SignedAngle(transform.forward, carRb.velocity + transform.forward, Vector3.up);
                break;
            case CounterSteer.onNormal:
                if (Vector3.Dot(carRb.velocity, transform.forward) > 0.19f)
                    steerAngle += Vector3.SignedAngle(transform.forward, carRb.velocity + transform.forward, Vector3.up);
                break;
            case CounterSteer.noCounterSteer:
                break;
            default:
                if (Vector3.Dot(carRb.velocity, transform.forward) > 0.19f && isDrifting && Speed > 20f)
                    steerAngle += Vector3.SignedAngle(transform.forward, carRb.velocity + transform.forward, Vector3.up);
                break;


        }

    }
    public void SteerHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelCollider wheelCollider = wheelColliders.GetWheelCollider(i);
            if (wheelCollider == null)
                continue;

            if (!wheelCollider.GetGroundHit(out WheelHit wheelHit) || wheelHit.normal == Vector3.zero)
                continue;

            float rotationDiff = Mathf.Abs(oldRotation - transform.eulerAngles.y);
            if (rotationDiff < 10f)
            {
                float turnAdjust = (transform.eulerAngles.y - oldRotation) * steerHelper;
                Quaternion velRotation = Quaternion.AngleAxis(turnAdjust, Vector3.up);
                carRb.velocity = velRotation * carRb.velocity;
            }
            oldRotation = transform.eulerAngles.y;
        }
    }

    private void AntiRoll()
    {
        ApplyAntiRollForce(wheelColliders.FRWheel, wheelColliders.FlWheel);
        ApplyAntiRollForce(wheelColliders.RRWheel, wheelColliders.RlWheel);
    }

    private void ApplyAntiRollForce(WheelCollider wheelL, WheelCollider wheelR)
    {
        if (!wheelL.isGrounded || !wheelR.isGrounded)
            return;

        float travelL = 1.0f;
        float travelR = 1.0f;

        WheelHit hit;
        bool groundedL = wheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;

        bool groundedR = wheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * antiRollVal;

        if (groundedL)
            carRb.AddForceAtPosition(wheelL.transform.up * -antiRollForce, wheelL.transform.position);

        if (groundedR)
            carRb.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);
    }

    #endregion
    private void AddDownForce()
    {
        carRb.AddForce(-transform.up * downForce * Speed);
    }

    public void ApplyBrakes(float brake)
    {

        brake = Mathf.Clamp(brake, 0, 1);
        float brakeTorque = brake * maxBrakeTorque;

        wheelColliders.FRWheel.brakeTorque = brakeTorque;
        wheelColliders.FlWheel.brakeTorque = brakeTorque;
        wheelColliders.RRWheel.brakeTorque = brakeTorque * 0.3f;
        wheelColliders.RlWheel.brakeTorque = brakeTorque * 0.3f;
    }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream. IsWriting){
    //         stream.SendNext(carRb.position);
    //         stream.SendNext(carRb.rotation);
    //         stream.SendNext(carRb.velocity);
    //     }else{
    //         carRb.position =(Vector3)stream.ReceiveNext();
    //          carRb.rotation =(Quaternion)stream.ReceiveNext();
    //           carRb.velocity =(Vector3)stream.ReceiveNext();
    //     }
    //     float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
    //     carRb.position+= carRb.velocity*lag;


    // }
    //   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         // Send the position, rotation, and velocity data over the network
    //         stream.SendNext(carRb.position);
    //         stream.SendNext(carRb.rotation);
    //         stream.SendNext(carRb.velocity);
    //     }
    //     else
    //     {
    //         // Receive the position, rotation, and velocity data from the network
    //         Vector3 receivedPosition = (Vector3)stream.ReceiveNext();
    //         Quaternion receivedRotation = (Quaternion)stream.ReceiveNext();
    //         Vector3 receivedVelocity = (Vector3)stream.ReceiveNext();

    //         // Interpolate between the current position and the received position
    //         float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
    //         float timeToReachGoal = lag + Time.deltaTime;
    //         Vector3 targetPosition = receivedPosition + receivedVelocity * timeToReachGoal;
    //         carRb.position = Vector3.Lerp(carRb.position, targetPosition, Time.deltaTime * 10f);

    //         // Set the received rotation directly
    //         carRb.rotation = receivedRotation;

    //         // Extrapolate the velocity to predict the future position
    //         carRb.velocity = receivedVelocity;
    //         carRb.position += carRb.velocity * Time.deltaTime;
    //     }


}

// private void OnDrawGizmos()
// {
//     Gizmos.color = Color.green;
//     Gizmos.DrawSphere(CenterofMass, 10f);
// }

