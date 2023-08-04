// use this only if all wheels rotations is 0
using UnityEngine;
interface IWheelSync
{
   void SetWheel();
    void ApplyWheelPose();
}



// public class SyncWheelGraphicsR000:IWheelSync {
//     private CarWheelColliders colliders;
//     private CarWheelMesh meshes;

//      public SyncWheelGraphicsR000(CarWheelColliders colliders, CarWheelMesh meshes)
//     {
//         this.colliders = colliders;
//         this.meshes=meshes;
//     }
//      public void ApplyWheelPose()
//     {
//         SetWheel(colliders.FRWheel, meshes.FRWheel);
//         SetWheel(colliders.FlWheel, meshes.FlWheel);
//         SetWheel(colliders.RRWheel, meshes.RRWheel);
//         SetWheel(colliders.RlWheel, meshes.RlWheel);
//     }



//     private void SetWheel(WheelCollider collider, MeshRenderer mesh)
//     {
//         Quaternion quat;
//         Vector3 position;
//         collider.GetWorldPose(out position, out quat);
//         mesh.transform.rotation = quat;
//         mesh.transform.position = position;
//     }

//     void IWheelSync.SetWheel(WheelCollider collider, MeshRenderer mesh)
//     {
        
//     }
// }




//use this if the wheel meshes have different rotations make a game object called carColliders and make the wheel colliders a child ot it
//and make another empty gameobject and name it carWheels and make 4 empty gameobject for every wheel mesh and but the wheel mesh as a child of every empty game object with the same name




 public class SyncWheelGraphics:IWheelSync{
     WheelCollider[] wheelCollider;
      Transform[] wheelTransforms;
      private Vector3 wheelPosition;
     private Quaternion wheelRotation;
     private CarAnim carAnim;
     private CarWheelColliders colliders;
     private CarWheelTeansForm transforms;

     public SyncWheelGraphics(CarWheelColliders colliders,CarWheelTeansForm wheeltransform,CarAnim carAnim){
        this.colliders = colliders;
        this.transforms = wheeltransform;

        this.carAnim = carAnim;

     }
    
    public void SetWheel()
{
    wheelCollider = new WheelCollider[4];
    wheelTransforms = new Transform[4];

    for (int i = 0; i < 4; i++)
    {
        wheelCollider[i] = colliders.GetWheelCollider(i);
        wheelTransforms[i] = transforms.GetWheelMesh(i);
    }

}
public void ApplyWheelPose()
{
    for (int i = 0; i < wheelCollider.Length; i++) 
    {
        wheelCollider[i].GetWorldPose(out wheelPosition, out wheelRotation);
        wheelTransforms[i].transform.localRotation = Quaternion.Euler(0, wheelCollider[i].steerAngle, 0);

        if (carAnim == CarAnim.rotatemessEngineOnX) 
        {
            if (i % 2 != 0) 
            {
                wheelTransforms[i].transform.GetChild(0).transform.Rotate(wheelCollider[i].rpm * -6.6f * Time.deltaTime, 0, 0, Space.Self); //engine rotation
            }
            else 
            {                                   
                wheelTransforms[i].transform.GetChild(0).transform.Rotate(wheelCollider[i].rpm * 6.6f * Time.deltaTime, 0, 0, Space.Self); //engine rotation
            }
        }
        else if (carAnim == CarAnim.rotatemessEngineOnZ)
        {
            if (i % 2 != 0) 
            {
                wheelTransforms[i].transform.GetChild(0).transform.Rotate(0, 0, wheelCollider[i].rpm * -6.6f * Time.deltaTime, Space.Self); //engine rotation
            }
            else 
            {                                   
                wheelTransforms[i].transform.GetChild(0).transform.Rotate(0, 0, wheelCollider[i].rpm * 6.6f * Time.deltaTime, Space.Self); //engine rotation
            }
        }

        wheelTransforms[i].transform.position = wheelPosition;
    }
}
}

