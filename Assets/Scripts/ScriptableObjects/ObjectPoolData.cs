/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPoolData.cs
 * Date Created: October 30, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 30, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars.Data
{
	[CreateAssetMenu(fileName = "ObjectPoolData", menuName = "Data/New ObjectPoolData", order = 0)]
	public class ObjectPoolData : ScriptableObject
	{
		//VARIABLES
		#region Inspector Variable Declarations and Initializations
		
		[SerializeField] private GameObject PrefabToUse = null;
		[SerializeField] private int MinimumSize = 1;
		[SerializeField] private int MaximumSize = 10;
		
		#endregion
		
		//GETTERS
		#region Accessors/Getters
		
		public GameObject GetPrefab => PrefabToUse;
		public int GetMinimumSize => MinimumSize;
		public int GetMaximumSize => MaximumSize;
		
		#endregion
	}
}