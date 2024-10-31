/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PickupStruct.cs
 * Date Created:
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified:
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using System;
using UnityEngine;

namespace TrenchWars
{
	[Serializable]
	public struct PickupStruct
	{
		#region Public Variables
	
		public GameObject ThePrefab;
		public float DropChance;
	
		#endregion
	}
}