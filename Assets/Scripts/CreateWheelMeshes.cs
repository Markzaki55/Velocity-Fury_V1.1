// // using UnityEngine;
// // using UnityEditor;

// // public class CreateWheelMeshes : EditorWindow
// // {
// //     private MeshRenderer wheelMesh;

// //     [MenuItem("Window/Create Wheel Meshes")]
// //     public static void ShowWindow()
// //     {
// //         EditorWindow.GetWindow(typeof(CreateWheelMeshes));
// //     }

// //     private void OnGUI()
// //     {
// //         wheelMesh = (MeshRenderer)EditorGUILayout.ObjectField("Wheel Mesh", wheelMesh, typeof(MeshRenderer), true);

// //         if (GUILayout.Button("Create Wheel Meshes"))
// //         {
// //             if (wheelMesh != null)
// //             {
// //                 // Create the front left wheel mesh
// //                 GameObject frontLeftWheel = CreateWheel("FL Wheel Mesh", new Vector3(-1.0f, 0.0f, 1.0f), wheelMesh);
// //                 frontLeftWheel.transform.localScale = new Vector3(100, 100, 100);

// //                 // Create the front right wheel mesh
// //                 GameObject frontRightWheel = CreateWheel("FR Wheel Mesh", new Vector3(1.0f, 0.0f, 1.0f), wheelMesh);
// //                 frontRightWheel.transform.localScale = new Vector3(100, 100, 100);

// //                 // Create the rear left wheel mesh
// //                 GameObject rearLeftWheel = CreateWheel("RL Wheel Mesh", new Vector3(-1.0f, 0.0f, -1.0f), wheelMesh);
// //                 rearLeftWheel.transform.localScale = new Vector3(100, 100, 100);

// //                 // Create the rear right wheel mesh
// //                 GameObject rearRightWheel = CreateWheel("RR Wheel Mesh", new Vector3(1.0f, 0.0f, -1.0f), wheelMesh);
// //                 rearRightWheel.transform.localScale = new Vector3(100, 100, 100);

// //                 Debug.Log("Wheel meshes created successfully.");
// //             }
// //             else
// //             {
// //                 Debug.LogError("Please specify a wheel mesh.");
// //             }
// //         }
// //     }

// //     private GameObject CreateWheel(string name, Vector3 position, MeshRenderer meshRenderer)
// //     {
// //         GameObject wheel = new GameObject(name);
// //         wheel.transform.position = position;
// //         wheel.transform.rotation = Quaternion.identity;
// //         MeshFilter meshFilter = wheel.AddComponent<MeshFilter>();
// //         meshFilter.sharedMesh = meshRenderer.GetComponent<MeshFilter>().sharedMesh;
// //         MeshRenderer meshRendererComponent = wheel.AddComponent<MeshRenderer>();
// //         meshRendererComponent.sharedMaterials = meshRenderer.sharedMaterials;
// //         return wheel;
// //     }
// // }

// // using UnityEngine;
// // using UnityEditor;

// // public class CreateWheelMeshes : EditorWindow
// // {
// //     private MeshRenderer wheelMesh;

// //     [MenuItem("Window/Create Wheel Meshes")]
// //     public static void ShowWindow()
// //     {
// //         EditorWindow.GetWindow(typeof(CreateWheelMeshes));
// //     }

// //     private void OnGUI()
// //     {
// //         wheelMesh = (MeshRenderer)EditorGUILayout.ObjectField("Wheel Mesh", wheelMesh, typeof(MeshRenderer), true);

// //         if (GUILayout.Button("Create Wheel Meshes"))
// //         {
// //             if (wheelMesh != null)
// //             {
// //                 // Create the front left wheel mesh
// //                 GameObject frontLeftWheel = CreateWheel("FL Wheel Mesh", new Vector3(-1.0f, 0.0f, 1.0f), Quaternion.identity, wheelMesh);
// //                 frontLeftWheel.transform.localScale = new Vector3(100, 100, 100);

// //                 // Create the front right wheel mesh
// //                 GameObject frontRightWheel = CreateWheel("FR Wheel Mesh", new Vector3(1.0f, 0.0f, 1.0f), Quaternion.identity, wheelMesh);
// //                 frontRightWheel.transform.localScale = new Vector3(100, 100, 100);
// //                 frontRightWheel.transform.GetChild(0).Rotate(0, -180, 0);

// //                 // Create the rear left wheel mesh
// //                 GameObject rearLeftWheel = CreateWheel("RL Wheel Mesh", new Vector3(-1.0f, 0.0f, -1.0f), Quaternion.identity, wheelMesh);
// //                 rearLeftWheel.transform.localScale = new Vector3(100, 100, 100);

// //                 // Create the rear right wheel mesh
// //                 GameObject rearRightWheel = CreateWheel("RR Wheel Mesh", new Vector3(1.0f, 0.0f, -1.0f), Quaternion.identity, wheelMesh);
// //                 rearRightWheel.transform.localScale = new Vector3(100, 100, 100);
// //                 rearRightWheel.transform.GetChild(0).Rotate(0, -180, 0);

// //                 Debug.Log("Wheel meshes created successfully.");
// //             }
// //             else
// //             {
// //                 Debug.LogError("Please specify a wheel mesh.");
// //             }
// //         }
// //     }

// //     private GameObject CreateWheel(string name, Vector3 position, Quaternion rotation, MeshRenderer meshRenderer)
// //     {
// //         GameObject wheel = new GameObject(name);
// //         wheel.transform.position = position;
// //         wheel.transform.rotation = Quaternion.identity;

// //         // Create a child GameObject for the wheel mesh
// //         GameObject meshObject = new GameObject("Mesh");
// //         meshObject.transform.SetParent(wheel.transform);
// //         meshObject.transform.localPosition = Vector3.zero;
// //         meshObject.transform.localRotation = rotation;
// //         MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
// //         meshFilter.sharedMesh = meshRenderer.GetComponent<MeshFilter>().sharedMesh;
// //         MeshRenderer meshRendererComponent = meshObject.AddComponent<MeshRenderer>();
// //         meshRendererComponent.sharedMaterials = meshRenderer.sharedMaterials;

// //         return wheel;
// //     }
// // }

using UnityEngine;
using UnityEditor;

public class CreateWheelMeshes : EditorWindow
{
    private MeshRenderer wheelMesh;

    [MenuItem("Window/Create Wheel Meshes")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreateWheelMeshes));
    }

    private void OnGUI()
    {
        wheelMesh = (MeshRenderer)EditorGUILayout.ObjectField("Wheel Mesh", wheelMesh, typeof(MeshRenderer), true);

        if (GUILayout.Button("Create Wheel Meshes"))
        {
            if (wheelMesh != null)
            {
                // Create a parent GameObject for all the wheel meshes
                GameObject carWheels = new GameObject("carWheels");

                // Create the front left wheel mesh
                GameObject frontLeftWheel = CreateWheel("FL Wheel ", new Vector3(-1.0f, 0.0f, 1.0f), Quaternion.identity, wheelMesh, carWheels);
                frontLeftWheel.transform.localScale = new Vector3(1, 1, 1);

                // Create the front right wheel mesh
                GameObject frontRightWheel = CreateWheel("FR Wheel ", new Vector3(1.0f, 0.0f, 1.0f), Quaternion.identity, wheelMesh, carWheels);
                frontRightWheel.transform.localScale = new Vector3(1, 1, 1);
                frontRightWheel.transform.GetChild(0).Rotate(0, -180, 0);

                // Create the rear left wheel mesh
                GameObject rearLeftWheel = CreateWheel("RL Wheel ", new Vector3(-1.0f, 0.0f, -1.0f), Quaternion.identity, wheelMesh, carWheels);
                rearLeftWheel.transform.localScale = new Vector3(1, 1, 1);

                // Create the rear right wheel mesh
                GameObject rearRightWheel = CreateWheel("RR Wheel ", new Vector3(1.0f, 0.0f, -1.0f), Quaternion.identity, wheelMesh, carWheels);
                rearRightWheel.transform.localScale = new Vector3(1, 1, 1);
                rearRightWheel.transform.GetChild(0).Rotate(0, -180, 0);

                // Create a parent GameObject for all the wheel colliders
                GameObject carColliders = new GameObject("carColliders");

                // Create the front left wheel collider
                GameObject frontLeftCollider = CreateWheelCollider("FL Wheel Collider", frontLeftWheel.transform.position, carColliders);
                frontLeftCollider.transform.localScale = new Vector3(1, 1, 1);

                // Create the front right wheel collider
                GameObject frontRightCollider = CreateWheelCollider("FR Wheel Collider", frontRightWheel.transform.position, carColliders);
                frontRightCollider.transform.localScale = new Vector3(1, 1, 1);

                // Create the rear left wheel collider
                GameObject rearLeftCollider = CreateWheelCollider("RL Wheel Collider", rearLeftWheel.transform.position, carColliders);
                rearLeftCollider.transform.localScale = new Vector3(1, 1, 1);

                // Create the rear right wheel collider
                GameObject rearRightCollider = CreateWheelCollider("RR Wheel Collider", rearRightWheel.transform.position, carColliders);
                rearRightCollider.transform.localScale = new Vector3(1, 1, 1);

                Debug.Log("Wheel meshes and colliders created successfully.");
            }
            else
            {
                Debug.LogError("Please specify a wheel mesh.");
            }
        }
    }

    private GameObject CreateWheel(string name, Vector3 position, Quaternion rotation, MeshRenderer meshRenderer, GameObject parent)
{
    GameObject wheel = new GameObject(name);
    wheel.transform.SetParent(parent.transform);
    wheel.transform.position = position;
    wheel.transform.rotation = Quaternion.identity;

    // Create a child GameObject for the wheel mesh
    GameObject meshObject = new GameObject(name + " Mesh"); // <-- Include the name of the wheel in the child object name
    meshObject.transform.SetParent(wheel.transform);
    meshObject.transform.localPosition = Vector3.zero;
    meshObject.transform.localRotation = Quaternion.identity;
    MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
    meshFilter.sharedMesh = meshRenderer.GetComponent<MeshFilter>().sharedMesh;
    MeshRenderer meshRendererComponent = meshObject.AddComponent<MeshRenderer>();
    meshRendererComponent.sharedMaterials = meshRenderer.sharedMaterials;

    return wheel;
}

    private GameObject CreateWheelCollider(string name, Vector3 position, GameObject parent)
    {
        GameObject wheelCollider = new GameObject(name);
        wheelCollider.transform.SetParent(parent.transform);
        wheelCollider.transform.position = position;
        wheelCollider.transform.rotation = Quaternion.identity;
        WheelCollider colliderComponent = wheelCollider.AddComponent<WheelCollider>();
        colliderComponent.radius = wheelMesh.transform.localScale.x / 2f; // Set the wheel collider radius based on the wheel mesh scale

        return wheelCollider;
    }
}