/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PoolStruct.cs
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
	public struct PoolStruct
	{
        public GameObject ThePrefab;
        public int StartingSize;
        public int MaxSize;
    }
}