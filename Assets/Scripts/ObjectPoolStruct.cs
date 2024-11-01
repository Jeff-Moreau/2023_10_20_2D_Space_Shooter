/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPoolStruct.cs
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

using System;
using UnityEngine;

namespace TrenchWars
{
    [Serializable]
	public struct ObjectPoolStruct
	{
        public GameObject ThePrefab;
        public int MinimumPoolSize;
        public int MaximumPoolSize;
    }
}