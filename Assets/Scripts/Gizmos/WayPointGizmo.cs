/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: WaypointGizmo.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 18, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

public class WaypointGizmo : MonoBehaviour
{
    [SerializeField] private float mSphereSize = 1;

    private Transform[] mWaypoints;

    private void Awake()
    {
        mWaypoints = GetComponentsInChildren<Transform>();
    }

    private void OnDrawGizmos()
    {
        var lastWayPoint = mWaypoints[^1].position;

        for(int i = 1; i < mWaypoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(mWaypoints[i].position, mSphereSize);
            Gizmos.DrawLine(lastWayPoint, mWaypoints[i].position);
            lastWayPoint = mWaypoints[i].position;
        }
    }
}