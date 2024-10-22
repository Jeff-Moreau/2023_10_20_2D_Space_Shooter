/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HUDActions.cs
 * Date Created: October 21, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 21, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using System;
using UnityEngine;

namespace TrenchWars
{
	public class HUDActions : MonoBehaviour
	{
        public static Action<int> UpdateScore;
        public static Action<float> UpdateShield;
        public static Action<float> UpdateHealth;
        public static Action<float> UpdateSpecial;
    }
}