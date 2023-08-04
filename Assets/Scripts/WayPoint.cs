using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
   public WayPoint NEXTwaypoint;
   public WayPoint PREVIOUSwaypoint;
[Range(0f,20f)]
   public float width = 1;

   public Vector3 GETPOS(){
    Vector3 MinBound = transform.position + transform.right* width/2;
    Vector3 MaxBound = transform.position - transform.right* width/2;
    return Vector3.Lerp(MinBound,MaxBound,Random.Range(0f,1f));

   }
   
}
