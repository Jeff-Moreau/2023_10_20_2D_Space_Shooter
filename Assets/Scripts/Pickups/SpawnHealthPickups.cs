/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: SpawnHealthPickups.cs
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

using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthPickups : MonoBehaviour
{
    [SerializeField] private List<GameObject> mPickupSpawns = null;
    [SerializeField] private HealthPickupPool mHealthPickup = null;

    private float mSpawnInterval;
    private float mSpawnTimer;

    void Start()
    {
        mSpawnInterval = 15;
        mSpawnTimer = 0;
    }

    void Update()
    {
        mSpawnTimer -= Time.deltaTime;

        if (mSpawnTimer <= 0)
        {
            var newHealthPickup = mHealthPickup.GetHealthPickup();

            if (newHealthPickup != null)
            {
                var randSpawn = Random.Range(0, mPickupSpawns.Count);
                newHealthPickup.transform.position = mPickupSpawns[randSpawn].transform.position;
            }

            for (int i = 0; i < mHealthPickup.GetHealthPickups.Count; i++)
            {
                newHealthPickup.SetActive(true);
            }

            mSpawnTimer = mSpawnInterval;
        }
    }
}