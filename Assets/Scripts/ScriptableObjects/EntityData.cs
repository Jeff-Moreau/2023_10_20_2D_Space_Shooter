/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: EntityData.cs
 * Date Created: Novemeber 6, 2024
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
	[CreateAssetMenu(fileName = "EntityData", menuName = "Data/New EntityData", order = 0)]
	public class EntityData : ScriptableObject
	{
		//FIELDS
		#region Private Serialized Fields: For Inspector Editable Values

		[SerializeField] private string _name = "";
		[SerializeField] private float _maxHealth = 0.0f;
		[SerializeField] private float _movementSpeed = 0.0f;
		
		#endregion
		
		//PROPERTIES
		#region Public Properties: For Accessing Class Fields
		
		public string GetName => _name;
		public float GetMaxHealth => _maxHealth;
		public float GetMovementSpeed => _movementSpeed;
		
		#endregion
	}
}