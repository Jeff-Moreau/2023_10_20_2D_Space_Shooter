/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HUD.cs
 * Date Created: October 21, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 5, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrenchWars
{
	public class HUD : MonoBehaviour
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private TextMeshProUGUI ScoreText = null;
		[SerializeField] private Slider ShieldLevel = null;
		[SerializeField] private Slider SpecialMeter = null;
		[SerializeField] private Slider HealthBar = null;

        #endregion

        //METHODS
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
		{
			PlayerActions.UpdateScore += UpdateScore;
			PlayerActions.CurrentHealth += UpdateHealth;
			PlayerActions.ShieldTimer += UpdateShield;
			PlayerActions.SpecialTimer += UpdateSpecial;
		}

        private void OnDisable()
		{
            PlayerActions.UpdateScore -= UpdateScore;
            PlayerActions.CurrentHealth -= UpdateHealth;
            PlayerActions.ShieldTimer -= UpdateShield;
            PlayerActions.SpecialTimer -= UpdateSpecial;
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void UpdateSpecial(float aAmount)
        {
            SpecialMeter.value = aAmount;
        }

        private void UpdateShield(float aAmount)
        {
            ShieldLevel.value = aAmount;
        }

        private void UpdateHealth(float aAmount)
        {
            HealthBar.value = aAmount;
        }

        private void UpdateScore(int aAmount)
        {
            ScoreText.text = aAmount.ToString();
        }

        #endregion
    }
}