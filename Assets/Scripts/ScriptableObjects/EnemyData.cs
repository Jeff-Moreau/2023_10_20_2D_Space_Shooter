/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: EnemySO.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 5, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/New EnemyData")]
    public class EnemyData : EntityData
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private float _maxShield = 0.0f;
        [SerializeField] private int _scoreValue = 0;
        [SerializeField] private EnemyType _enemyType = EnemyType.Turret;
        [SerializeField] private GameObject _explosionAnimation = null;
        [SerializeField] private AudioClip _takeDamageSound = null;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetMaxShield => _maxShield;
        public int GetScoreValue => _scoreValue;
        public EnemyType GetEnemyType => _enemyType;
        public GameObject GetExplosionAnimation => _explosionAnimation;
        public AudioClip GetTakeDamageSound => _takeDamageSound;

        #endregion
    }
}