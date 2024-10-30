/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretSO.cs
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

namespace TrenchWars.Data
{
    [CreateAssetMenu(fileName = "TurretData", menuName = "Data/New TurretData")]
    public class TurretData : ScriptableObject
    {
        //VARIABLES
        #region Inspector Variable Declarations and Initializations

        [SerializeField] private float RateOfFire = 2f;
        [SerializeField] private float MaxHealth = 3f;
        [SerializeField] private float MoveSpeed = 1.2f;
        [SerializeField] private GameObject ProjectileUsed = null;
        [SerializeField] private GameObject ExplosionAnimation = null;
        [SerializeField] private AudioClip TakeDamageSound = null;
        [SerializeField] private AudioClip ShootingSound = null;

        #endregion
        //GETTERS
        #region Accessors/Getters

        public float GetRateOfFire => RateOfFire;
        public float GetHealth => MaxHealth;
        public float GetMoveSpeed => MoveSpeed;
        public GameObject GetProjectileUsed => ProjectileUsed;
        public GameObject GetExplosionAnimation => ExplosionAnimation;
        public AudioClip GetTakeDamageSound => TakeDamageSound;
        public AudioClip GetShootingSound => ShootingSound;

        #endregion
    }
}