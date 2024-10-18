/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HealthPickup.cs
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

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float mSpeed = 5.0f;
    [SerializeField] private float mDistanceToNextWaypoint = 1.0f;
    [SerializeField] private Transform mWaypointPosition = null;

    private Transform[] mWaypoints;
    private int mCurrentWaypoint;

    private void Start()
    {
        var temp = mWaypointPosition.GetComponentsInChildren<Transform>();
        mWaypoints = new Transform[temp.Length - 1];

        for (int i = 2; i < temp.Length; i++)
        {
            mWaypoints[i - 2] = temp[i];
        }

        mCurrentWaypoint = 0;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, mWaypoints[mCurrentWaypoint].position, mSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, mWaypoints[mCurrentWaypoint].position) < mDistanceToNextWaypoint)
        {
            mCurrentWaypoint++;

            if (mCurrentWaypoint >= mWaypoints.Length)
            {
                mCurrentWaypoint = 0;
            }
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}