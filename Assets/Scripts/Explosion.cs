/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: Explosion.cs
 * Date Created: October 20, 2023
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

using UnityEngine;
using System.Collections;

namespace TrenchWars
{
    public class Explosion : Entity
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Space(10)]
        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _myAudioSource = null;
        [SerializeField] private AudioClip _theExplosionSound = null;
        [Space(10)]
        [Header("ANIMATION >=========================================")]
        [SerializeField] private AnimationClip _theExplosionAnimation = null;

        #endregion
        #region Private Fields: For Internal Use

        private ScreenShake _shakeCamera;

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            _shakeCamera = Camera.main.GetComponent<ScreenShake>(); //Camera Actions instead
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _myAnimator.Play("Explosion");
            _shakeCamera.TriggerShake(0.1f);
            _myAudioSource.PlayOneShot(_theExplosionSound);
            StartCoroutine(AnimationFinishedPlaying(_theExplosionAnimation.length));
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            transform.position -= new Vector3(0, 1 * Time.deltaTime, 0); // Change this to speed and direction of calling gameobject
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator AnimationFinishedPlaying(float animationLength)
        {
            // Wait for this
            yield return new WaitForSeconds(animationLength);

            // Then do this
            gameObject.SetActive(false);
        }

        #endregion
    }
}