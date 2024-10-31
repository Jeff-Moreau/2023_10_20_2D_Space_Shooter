/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: SharkBoss.cs
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
    public class SharkBoss : MonoBehaviour
    {
        [SerializeField] private Data.TurretData MyData = null;
        //[SerializeField] private GameObject[] mLaserSpawns = null;

        private GameObject mPlayer;
        private float mShootTimer;
        private float mCurrentHealth;

        private void Awake() => mPlayer = GameObject.FindGameObjectWithTag("Player");

        private void Start()
        {
            mCurrentHealth = MyData.GetHealth;
            mShootTimer = 1;
        }

        private void Update()
        {
            mShootTimer -= Time.deltaTime;
            TargetPlayer();
            ShootPlayer();

            if (mCurrentHealth <= 0)
            {
                TurretDie();
            }
        }

        private void TurretDie()
        {
            gameObject.SetActive(false);
            mCurrentHealth = MyData.GetHealth;
        }

        private void ShootPlayer()
        {
            if (mShootTimer <= 0)
            {
                mShootTimer = 1;
            }
        }

        private void TargetPlayer()
        {
            Vector3 rotation = mPlayer.transform.position - transform.position;
            float zAxisRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zAxisRotation + 90);
        }

        private void OnTriggerEnter2D(Collider2D aCollision)
        {
            if (aCollision.gameObject.layer == 7)
            {
                mCurrentHealth -= 1;
                aCollision.gameObject.SetActive(false);
            }
        }
    }
}