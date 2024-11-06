/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ProjectileSO.cs
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
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Data/New ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private float _movementSpeed = 0.0f;
        [SerializeField] private float _damage = 1.0f;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetDamage => _damage;
        public float GetMovementSpeed => _movementSpeed;

        #endregion
    }
}