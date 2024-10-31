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
		
		//[SerializeField] private int NumberOfGuesses = 0; // Example
		
		#endregion
		
		//GETTERS
		#region Accessors/Getters
		
		//public int GetNumberOfGuesses => NumberOfGuesses; // Example
		
		#endregion
	}
}