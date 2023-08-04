using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAiNAV : MonoBehaviour
{
    [SerializeField] WayPoint currentwaypoint;
    [SerializeField] float stoppingDistance;

    void Update()
    {
        if (ReachedWaypoint())
        {
            currentwaypoint = currentwaypoint.NEXTwaypoint;
        }
    }

    public float GetVerticalInput()
    {
        Vector3 dirToMove = (currentwaypoint.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToMove);

        if (dot > 0f)
        {
            return 1f;
        }
        else
        {
            return -1f;
        }
    }

    public float GetHorizontalInput()
    {
        Vector3 dirToMove = (currentwaypoint.transform.position - transform.position).normalized;
        float angleToTurn = Vector3.SignedAngle(transform.forward, dirToMove, transform.up);

        //float steering = angleToTurn / 180f;
        return Mathf.Clamp(angleToTurn, -0.1f, 0.1f);
    }

    bool ReachedWaypoint()
    {
        return Vector3.Distance(transform.position, currentwaypoint.transform.position) < stoppingDistance;
    }
}