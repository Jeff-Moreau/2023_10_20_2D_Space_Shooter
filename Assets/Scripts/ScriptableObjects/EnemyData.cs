/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: EnemySO.cs
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
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/New EnemyData")]
    public class EnemyData : ScriptableObject
    {
        //VARIABLES
        #region Inspector Variable Declarations and Initializations

        [SerializeField] private string Name = "";
        [SerializeField] private float MaxLife = 0.0f;
        [SerializeField] private float MaxShield = 0.0f;
        [SerializeField] private float MovementSpeed = 0.0f;
        [SerializeField] private float ShootingSpeed = 0.0f;

        #endregion

        //GETTERS
        #region Accessors/Getters

        public string GetName => Name;
        public float GetMaxLife => MaxLife;
        public float GetMaxShield => MaxShield;
        public float GetMovementSpeed => MovementSpeed;
        public float GetShootingSpeed => ShootingSpeed;

        #endregion
    }
}