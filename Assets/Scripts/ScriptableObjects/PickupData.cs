/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PickupData.cs
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
	[CreateAssetMenu(fileName = "PickupData", menuName = "Data/New PickupData", order = 0)]
	public class PickupData : ScriptableObject
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private float _moveSpeed = 0.0f;
		[SerializeField] private float _healAmount = -0.0f;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetMoveSpeed => _moveSpeed;
		public float GetHealAmount => _healAmount;
		
		#endregion
	}
}