/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerData.cs
 * Date Created: October 31, 2024
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
	[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/New PlayerData", order = 0)]
	public class PlayerData : ScriptableObject
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private float _maxHealth = 5;
		[SerializeField] private float _moveSpeed = 10;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetMaxHealth => _maxHealth;
		public float GetMoveSpeed => _moveSpeed;
		
		#endregion
	}
}