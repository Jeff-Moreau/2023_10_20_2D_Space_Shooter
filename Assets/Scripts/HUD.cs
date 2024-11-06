/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: HUD.cs
 * Date Created: October 21, 2024
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrenchWars
{
	public class HUD : MonoBehaviour
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private TextMeshProUGUI _scoreText = null;
		[SerializeField] private Slider _shieldLevel = null;
		[SerializeField] private Slider _specialMeter = null;
		[SerializeField] private Slider _healthBar = null;

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

        private void UpdateSpecial(float amount)
        {
            _specialMeter.value = amount;
        }

        private void UpdateShield(float amount)
        {
            _shieldLevel.value = amount;
        }

        private void UpdateHealth(float amount)
        {
            _healthBar.value = amount;
        }

        private void UpdateScore(int amount)
        {
            _scoreText.text = amount.ToString();
        }

        #endregion
    }
}