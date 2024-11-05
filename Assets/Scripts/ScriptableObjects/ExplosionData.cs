/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ExplosionData.cs
 * Date Created: November 5, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 5, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

namespace TrenchWars.Data
{
	[CreateAssetMenu(fileName = "ExplosionData", menuName = "Data/New ExplosionData", order = 0)]
	public class ExplosionData : ScriptableObject
	{
		//FIELDS
		#region Private Serialized Fields: For Inspector Editable Values
		
		[SerializeField] private float _deactivateDelay = 0.0f;
		
		#endregion
		
		//PROPERTIES
		#region Public Properties: For Accessing Class Fields
		
		public float GetDeactivateDelay => _deactivateDelay;
		
		#endregion
	}
}