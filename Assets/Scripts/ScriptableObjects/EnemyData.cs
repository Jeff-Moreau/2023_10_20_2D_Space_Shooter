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
    public class EnemyData : ScriptableObject
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private string _name = "";
        [SerializeField] private float _maxLife = 0.0f;
        [SerializeField] private float _maxShield = 0.0f;
        [SerializeField] private float _movementSpeed = 0.0f;
        [SerializeField] private float _shootingSpeed = 0.0f;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public string GetName => _name;
        public float GetMaxLife => _maxLife;
        public float GetMaxShield => _maxShield;
        public float GetMovementSpeed => _movementSpeed;
        public float GetShootingSpeed => _shootingSpeed;

        #endregion
    }
}