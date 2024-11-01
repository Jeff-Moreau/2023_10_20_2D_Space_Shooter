/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerActions.cs
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
	public class PlayerActions : MonoBehaviour
	{
        public static Action<int> UpdateScore;
        public static Action<float> ShieldTimer;
        public static Action<float> CurrentHealth;
        public static Action<float> SpecialTimer;
    }
}