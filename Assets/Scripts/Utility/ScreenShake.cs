/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ScreenShake.cs
 * Date Created: October 20, 2024
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

namespace TrenchWars
{
	public class ScreenShake : MonoBehaviour
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private float _shakeMagnitude = 0.1f;
        [SerializeField] private float _dampingSpeed = 1.0f;

        #endregion
        #region Private Fields: For Internal Use

        private Vector3 _initialPosition;
        private float _currentShakeDuration;

        #endregion

        //METHODS
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _initialPosition = transform.localPosition;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            if (_currentShakeDuration > 0)
            {
                transform.localPosition = _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
                _currentShakeDuration -= Time.deltaTime * _dampingSpeed;
            }
            else
            {
                _currentShakeDuration = 0f;
                transform.localPosition = _initialPosition;
            }
        }

        #endregion
        #region Public Methods: For External Interactions

        public void TriggerShake(float duration)
        {
            _currentShakeDuration = duration;
        }

        #endregion
    }
}