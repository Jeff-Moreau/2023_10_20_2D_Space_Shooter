/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerData.cs
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
	[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/New PlayerData", order = 0)]
	public class PlayerData : ScriptableObject
	{
		//VARIABLES
		#region Inspector Variable Declarations and Initializations

		[SerializeField] private float MaxHealth = 5;
		[SerializeField] private float MoveSpeed = 10;

		#endregion

		//GETTERS
		#region Accessors/Getters

		public float GetMaxHealth => MaxHealth;
		public float GetMoveSpeed => MoveSpeed;
		
		#endregion
	}
}