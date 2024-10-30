/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretShooting.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 30, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
    public class TurretShooting : MonoBehaviour
    {
        [SerializeField] private Data.TurretData mTurretData = null;
        [SerializeField] private GameObject mLaserSpawnOne = null;

        private GameObject mPlayer;
        private float mShootTimer;
        private ObjectPoolManager mLevelObjectManager;

        private void OnEnable()
        {
            mPlayer = GameObject.FindGameObjectWithTag("Player");
            mLevelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (mLevelObjectManager == null)
            {
                Debug.LogError("ObjectPoolManager not found in the scene!");
            }
            mShootTimer = 1.5f;
        }

        private void Update()
        {
            mShootTimer -= Time.deltaTime;
            TargetPlayer();
            ShootPlayer();
        }

        private void ShootPlayer()
        {
            if (mShootTimer <= 0)
            {
                GameObject myProjectile = mLevelObjectManager.GetObject(mTurretData.GetProjectileUsed);

                if (myProjectile != null)
                {
                    myProjectile.GetComponent<ProjectileBase>().SetOwner(transform.parent.gameObject);
                    myProjectile.transform.SetPositionAndRotation(mLaserSpawnOne.transform.position, mLaserSpawnOne.transform.rotation);
                    //myProjectile.transform.position = ProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                    //mCurrentFirePosition = (mCurrentFirePosition + 1) % ProjectileSpawnPoints.Count;
                    myProjectile.SetActive(true);
                    mShootTimer = 0;
                }

                mShootTimer = 1.5f;
            }
        }

        private void TargetPlayer()
        {
            Vector3 rotation = mPlayer.transform.position - transform.position;
            float zAxisRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zAxisRotation - 90);
        }

        private void OnTriggerEnter2D(Collider2D aCollision)
        {
            if (aCollision.gameObject.layer == 6)
            {
                ShootPlayer();
            }
        }
    }
}