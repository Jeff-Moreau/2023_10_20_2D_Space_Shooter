/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ButtonHover.cs
 * Date Created: October 18, 2024
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
using UnityEngine.EventSystems;

namespace TrenchWars
{
	public class ButtonHover : MonoBehaviour, IPointerEnterHandler
	{
        //METHODS
        #region Public Methods: For External Interactions

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Should randomize the sounds here
            Manager.AudioManager.Access.PlaySound(SoundFX.UIHoverButton, SoundFXSource.Normal);
        }

        #endregion
    }
}