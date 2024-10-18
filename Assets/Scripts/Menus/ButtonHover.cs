/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ButtonHover.cs
 * Date Created: October 18, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 18, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;
using UnityEngine.EventSystems;

namespace TrenchWars
{
	public class ButtonHover : MonoBehaviour, IPointerEnterHandler
	{
		//FUNCTIONS
		#region Public Methods/Functions

		public void OnPointerEnter(PointerEventData aEventData)
		{
			Manager.AudioManager.Access.PlaySound(eSoundFX.UIHoverButton, eSoundFXSource.Normal);
		}

        #endregion
    }
}