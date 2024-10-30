/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ScreenShake.cs
 * Date Created: October 20, 2024
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
 
using UnityEngine;

namespace TrenchWars
{
	public class ScreenShake : MonoBehaviour
	{
        //[SerializeField] private float ShakeDuration = 0.1f;
        [SerializeField] private float ShakeMagnitude = 0.1f;
        [SerializeField] private float DampingSpeed = 1.0f;

        private Vector3 mInitialPosition;
        private float mCurrentShakeDuration;

        private void OnEnable() => mInitialPosition = transform.localPosition;

        private void Update()
        {
            if (mCurrentShakeDuration > 0)
            {
                transform.localPosition = mInitialPosition + (Random.insideUnitSphere * ShakeMagnitude);
                mCurrentShakeDuration -= Time.deltaTime * DampingSpeed;
            }
            else
            {
                mCurrentShakeDuration = 0f;
                transform.localPosition = mInitialPosition;
            }
        }

        public void TriggerShake(float aDuration) => mCurrentShakeDuration = aDuration;
    }
}