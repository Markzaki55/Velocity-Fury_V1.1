using System;
using UnityEngine;

public interface IInputHandler
{
    float HorizontalInput { get; }

    float VerticalInput { get; }
    bool DriftInput { get; }
    float BrakeInput { get; }
}

public class KeyboardInputHandler : IInputHandler
{
    
    public Func<string, float> GetAxis { get; set; } = Input.GetAxis;
    public Func<KeyCode, bool> GetKey { get; set; } = Input.GetKey;

    public float HorizontalInput => GetAxis("Horizontal");

    public float VerticalInput => (BrakeInput == 1) ? 0 : GetAxis("Vertical");

    public bool DriftInput => GetKey(KeyCode.H);
    public float BrakeInput => GetKey(KeyCode.Space) ? 1f : 0f;

}

    public class AIInputHandler : IInputHandler
    {

        CarAiNAV Carai;
        public AIInputHandler()
        {
            GameObject carai = GameObject.FindWithTag("AI");
            this.Carai = carai.GetComponent<CarAiNAV>();
        }

        public float HorizontalInput
        {
            get { return Carai.GetHorizontalInput(); }
        }

    public float VerticalInput => Carai.GetVerticalInput();
    public bool DriftInput
        {
            get { return false; }
        }

        public float BrakeInput
        {
            get { return 0f; }
        }

    }
