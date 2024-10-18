/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretSO.cs
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

[CreateAssetMenu(fileName = "TurretBase", menuName = "ScriptableObjects/TurretBase")]
public class TurretSO : ScriptableObject
{
    [SerializeField] private float mRateOfFire = 0.0f;
    [SerializeField] private float mHealth = 0.0f;
    [SerializeField] private float mMoveSpeed = 0.0f;
    [SerializeField] private GameObject mProjectileUsed = null;
    [SerializeField] private EnemyLaserPool mLaser = null;

    public float GetRateOfFire => mRateOfFire;
    public float GetHealth => mHealth;
    public float GetMoveSpeed => mMoveSpeed;
    public GameObject GetProjectileUsed => mProjectileUsed;
    public EnemyLaserPool GetLaserPool => mLaser;
}