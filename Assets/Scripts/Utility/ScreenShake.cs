/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ScreenShake.cs
 * Date Created: October 20, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 20, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
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
	public class ScreenShake : MonoBehaviour
	{
        // Duration of the shake
        public float shakeDuration = 0.5f;

        // Magnitude of the shake effect (how much the camera moves)
        public float shakeMagnitude = 0.7f;

        // How quickly the shake should dampen out
        public float dampingSpeed = 1.0f;

        // Original position of the camera
        private Vector3 initialPosition;

        // Tracks the remaining time for shaking
        private float currentShakeDuration = 0f;

        private void OnEnable()
        {
            initialPosition = transform.localPosition;
        }

        private void Update()
        {
            if (currentShakeDuration > 0)
            {
                // Randomize camera position within the defined magnitude range
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

                // Decrease the shake duration over time
                currentShakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                // Reset camera position when shaking is done
                currentShakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }

        // Public method to trigger the camera shake effect
        public void TriggerShake(float duration)
        {
            currentShakeDuration = duration;
        }
    }
}