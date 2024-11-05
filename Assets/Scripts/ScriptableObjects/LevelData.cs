/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: LevelData.cs
 * Date Created: October 20, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 22, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars.Data
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New LevelData", order = 0)]
	public class LevelData : ScriptableObject
	{
		//VARIABLES
		#region Inspector Variable Declarations and Initializations

		[SerializeField] private Music LevelMusic = Music.None;
		[SerializeField] private int PhaseTwoKillsNeeded = 20;
		[SerializeField] private int PhaseBossKillsNeeded = 40;
		
		#endregion
		
		//GETTERS
		#region Accessors/Getters
		
		public Music GetMusic => LevelMusic;
		public int GetPhaseTwoKillsNeeded => PhaseTwoKillsNeeded;
		public int GetPhaseBossKillsNeeded => PhaseBossKillsNeeded;
		
		#endregion
	}
}