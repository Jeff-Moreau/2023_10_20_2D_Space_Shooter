/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: SharkBoss.cs
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBoss : MonoBehaviour
{
    [SerializeField] private TurretSO mTurretData = null;
    [SerializeField] private BossLaserPool mBossLaserPool = null;
    [SerializeField] private GameObject[] mLaserSpawns = null;

    private GameObject mPlayer;
    private float mShootTimer;
    private float mCurrentHealth;

    private void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        mCurrentHealth = mTurretData.GetHealth;
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
        mCurrentHealth = mTurretData.GetHealth;
    }

    private void ShootPlayer()
    {
        if (mShootTimer <= 0)
        {
            var newLaser = mBossLaserPool.GetLaserProjectile();

            if (newLaser != null)
            {
                newLaser.transform.position = mLaserSpawns[0].transform.position;
                newLaser.transform.rotation = transform.rotation;
            }

            for (int i = 0; i < mBossLaserPool.GetLaserProjectiles.Count; i++)
            {
                newLaser.SetActive(true);
            }


            var newLaserTwo = mBossLaserPool.GetLaserProjectile();
            if (newLaserTwo != null)
            {
                newLaserTwo.transform.position = mLaserSpawns[1].transform.position;
                newLaserTwo.transform.rotation = transform.rotation;
            }

            for (int i = 0; i < mBossLaserPool.GetLaserProjectiles.Count; i++)
            {
                newLaserTwo.SetActive(true);
            }

            mShootTimer = 1;
        }
    }

    private void TargetPlayer()
    {
        var rotation = mPlayer.transform.position - transform.position;
        var zAxisRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zAxisRotation + 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            mCurrentHealth -= 1;
            collision.gameObject.SetActive(false);
        }
    }
}
