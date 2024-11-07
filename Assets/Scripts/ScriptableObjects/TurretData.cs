/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretData.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars.Data
{
    [CreateAssetMenu(fileName = "TurretData", menuName = "Data/New TurretData")]
    public class TurretData : EntityData
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private float _rateOfFire = 2f;
        [SerializeField] private GameObject _projectileUsed = null;
        [SerializeField] private GameObject _explosionAnimation = null;
        [SerializeField] private AudioClip _takeDamageSound = null;
        [SerializeField] private AudioClip _shootingSound = null;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetRateOfFire => _rateOfFire;
        public GameObject GetProjectileUsed => _projectileUsed;
        public GameObject GetExplosionAnimation => _explosionAnimation;
        public AudioClip GetTakeDamageSound => _takeDamageSound;
        public AudioClip GetShootingSound => _shootingSound;

        #endregion
    }
}