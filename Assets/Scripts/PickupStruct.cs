/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PickupStruct.cs
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
 
using System;
using UnityEngine;

namespace TrenchWars
{
	[Serializable]
	public struct PickupStruct
	{
		public GameObject ThePrefab;
		public float DropChance;
	}
}