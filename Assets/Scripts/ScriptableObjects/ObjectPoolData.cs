/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPoolData.cs
 * Date Created: October 30, 2024
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
	[CreateAssetMenu(fileName = "ObjectPoolData", menuName = "Data/New ObjectPoolData", order = 0)]
	public class ObjectPoolData : ScriptableObject
	{
		//FIELDS
		#region Private Serialized Fields: For Inspector Editable Values

		[SerializeField] private GameObject _prefabToUse = null;
		[SerializeField] private int _minimumSize = 1;
		[SerializeField] private int _maximumSize = 10;

		#endregion

		//PROPERTIES
		#region Public Properties: For Accessing Class Fields

		public GameObject GetPrefab => _prefabToUse;
		public int GetMinimumSize => _minimumSize;
		public int GetMaximumSize => _maximumSize;
		
		#endregion
	}
}