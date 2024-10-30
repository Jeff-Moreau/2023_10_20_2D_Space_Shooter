/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: Explosion.cs
 * Date Created: October 20, 2023
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
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private AudioSource mPlaySFX = null;
        [SerializeField] private AudioClip mExplosionSound = null;
        [SerializeField] private float mDelay = 0.0f;

        private ScreenShake shakeMe;
        private float mWaitTime;

        private void OnEnable() => mPlaySFX.PlayOneShot(mExplosionSound);

        private void Start()
        {
            mWaitTime = 0.0f;
            shakeMe = Camera.main.GetComponent<ScreenShake>();
            shakeMe.TriggerShake(0.1f);
        }

        private void Update()
        {
            transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);

            mWaitTime += Time.deltaTime;

            if (mWaitTime >= mDelay)
            {
                gameObject.SetActive(false);
            }
        }
    }
}