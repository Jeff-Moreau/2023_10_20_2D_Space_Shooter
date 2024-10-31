/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PickupData.cs
 * Date Created: October 31, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 31, 2024
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
		//VARIABLES
		#region Inspector Variable Declarations and Initializations

		[SerializeField] private float MoveSpeed = 0.0f;
		[SerializeField] private float HealAmount = -0.0f;

		#endregion

		//GETTERS
		#region Accessors/Getters

		public float GetMoveSpeed => MoveSpeed;
		public float GetHealAmount => HealAmount;
		
		#endregion
	}
}