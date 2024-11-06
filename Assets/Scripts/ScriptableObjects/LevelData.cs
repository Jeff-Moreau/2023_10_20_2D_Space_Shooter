/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: LevelData.cs
 * Date Created: October 20, 2024
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
	[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New LevelData", order = 0)]
	public class LevelData : ScriptableObject
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private Music _levelMusic = Music.None;
		[SerializeField] private int _phaseTwoKillsNeeded = 20;
		[SerializeField] private int _phaseBossKillsNeeded = 40;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public Music GetMusic => _levelMusic;
		public int GetPhaseTwoKillsNeeded => _phaseTwoKillsNeeded;
		public int GetPhaseBossKillsNeeded => _phaseBossKillsNeeded;
		
		#endregion
	}
}