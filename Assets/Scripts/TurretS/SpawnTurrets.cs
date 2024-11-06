/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: SpawnTurrets.cs
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

namespace TrenchWars
{
    public class SpawnTurrets : MonoBehaviour
    {
        /*//[SerializeField] private List<GameObject> mTurretSpawns = null;
        //[SerializeField] private TurretPool mTurret = null;
        //[SerializeField] private TextMeshProUGUI mTurretKills = null;
        //[SerializeField] private TurretText mCurrentKills = null;

        //private float mSpawnInterval;
        //private float mSpawnTimer;

        void Start()
        {
            //mSpawnInterval = 3.3f;
            //mSpawnTimer = 0.0f;
        }

        void Update()
        {
            //mSpawnTimer -= Time.deltaTime;
            //int spawnCount = Random.Range(0, 100);

            *//*if (mSpawnTimer <= 0 && spawnCount > 50 && mCurrentKills.GetturretKills <= 19)
            {
                for (int i = 0 ; i < mTurretSpawns.Count ; i++)
                {
                    GameObject newTurret = mTurret.GetTurret();

                    if (newTurret != null)
                    {
                        newTurret.transform.position = mTurretSpawns[i].transform.position;
                    }

                    for (int j = 0 ; j < mTurret.GetTurrets.Count ; j++)
                    {
                        newTurret.SetActive(true);
                    }

                }

                mSpawnTimer = mSpawnInterval;
            }
            else if (mSpawnTimer <= 0 && mCurrentKills.GetturretKills <= 19)
            {
                GameObject newTurret = mTurret.GetTurret();

                if (newTurret != null)
                {
                    int randSpawn = Random.Range(0, mTurretSpawns.Count);
                    newTurret.transform.position = mTurretSpawns[randSpawn].transform.position;
                }

                for (int i = 0 ; i < mTurret.GetTurrets.Count ; i++)
                {
                    newTurret.SetActive(true);
                }

                mSpawnTimer = mSpawnInterval;
            }*//*
        }*/
    }
}