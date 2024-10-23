/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: InputActions.cs
 * Date Created: October 23, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 23, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using System;
using UnityEngine;

//ENUMERATORS
#region Public Enumerator Declarations Only

// public enum eEnumName  // Example
// {
// 		Hey,
//		You
// }

#endregion

namespace TrenchWars
{
	public class InputActions : MonoBehaviour
	{
        public static Action InteractKey;
        public static Action FireKey;
        public static Action SpecialKey;
    }
}