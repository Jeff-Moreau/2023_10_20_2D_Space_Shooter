/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretSO.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 20, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars.Data
{
    [CreateAssetMenu(fileName = "TurretData", menuName = "Data/New TurretData")]
    public class TurretData : ScriptableObject
    {
        //VARIABLES
        #region Inspector Variable Declarations and Initializations

        [SerializeField] private float mRateOfFire = 0.0f;
        [SerializeField] private float mHealth = 0.0f;
        [SerializeField] private float mMoveSpeed = 0.0f;
        [SerializeField] private GameObject mProjectileUsed = null;
        [SerializeField] private EnemyLaserPool mLaser = null;

        #endregion
        //GETTERS
        #region Accessors/Getters

        public float GetRateOfFire => mRateOfFire;
        public float GetHealth => mHealth;
        public float GetMoveSpeed => mMoveSpeed;
        public GameObject GetProjectileUsed => mProjectileUsed;
        public EnemyLaserPool GetLaserPool => mLaser;

        #endregion
    }
}