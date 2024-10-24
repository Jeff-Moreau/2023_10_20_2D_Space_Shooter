/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerController.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 23, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TrenchWars
{
    public class PlayerController : MonoBehaviour, ITakeDamage
    {
        //VARIABLES
        #region Private enum Variables/Fields used in this Class Only

        private enum eProjectileSpawn
        {
            Left,
            Middle,
            Right
        }

        #endregion
        #region Private Variables/Fields Exposed to Inspector for Editing

        [Header("Player Information")]
        [SerializeField] private Animator MyAnimator = null;
        [SerializeField] private Rigidbody2D MyRigidbody = null;

        [Header("Object Refrences")]
        [SerializeField] private GameObject ExplosionAnimation = null;
        [SerializeField] private GameObject ScrapeLeftAnimation = null;
        [SerializeField] private GameObject ScrapeRightAnimation = null;
        [SerializeField] private List<GameObject> ProjectileSpawnPoints = null;
        [SerializeField] private LaserPool mLaserPool = null;
        [SerializeField] private Data.ProjectileData mLaser = null;

        #endregion
        #region Private Variables/Fields used in this Class Only

        private float mNewXPos;
        private float mNewYPos;
        private float mShipXPos;
        private float mShipYPos;
        private float mShootTimer;
        private bool mCanUseSpecial;
        private bool mCanTakeDamage;
        private float mCurrentHealth;
        private int mCurrentFirePosition;
        private Coroutine mFillSpecialMeter;

        #endregion

        //FUNCTIONS
        #region Private Initialization Functions/Methods used in this Class Only

        private void OnEnable()
        {
            InputActions.FireKey += Shoot;
            InputActions.SpecialKey += Special;
        }

        private void OnDisable()
        {
            InputActions.FireKey -= Shoot;
            InputActions.SpecialKey -= Special;
        }

        private void Start()
        {
            InitializeVariables();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            FillSpecial();
        }

        private void InitializeVariables()
        {
            mFillSpecialMeter = null;
            mCurrentHealth = 5;
            mShipXPos = 0.0f;
            mShipYPos = 0.0f;
            mNewXPos = 0.0f;
            mNewYPos = 0.0f;
            mShootTimer = 0.3f;
            mCurrentFirePosition = 0;
            mCanTakeDamage = true;
            mCanUseSpecial = mFillSpecialMeter == null ? false : true;
        }

        #endregion
        #region Private Physics Functions/Methods used in this Class Only

        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*if (collision.gameObject.layer == 8)
            {
                mCurrentHealth -= 1;
                collision.gameObject.SetActive(false);
            }*/

            if (collision.gameObject.layer == 9)
            {
                mCurrentHealth = 5;
                //mSoundPower.PlayOneShot(mFullHealthSound);
                collision.gameObject.SetActive(false);
            }
        }


        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("LeftWall"))
            {
                /*if (!mSoundEffects.isPlaying)
                {
                    mSoundEffects.PlayOneShot(mWallImpactSound);
                }*/
                ScrapeLeftAnimation.SetActive(true);
            }

            if (collision.gameObject.CompareTag("RightWall"))
            {
                /*if (!mSoundEffects.isPlaying)
                {
                    mSoundEffects.PlayOneShot(mWallImpactSound);
                }*/
                ScrapeRightAnimation.SetActive(true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("LeftWall"))
            {
                mCurrentHealth -= 1;

                /*if (!mSoundEffects.isPlaying)
                {
                    mSoundEffects.PlayOneShot(mWarningBeepSound);
                    mSoundEffects.PlayOneShot(mWallImpactSound);
                }*/
            }

            if (collision.gameObject.CompareTag("RightWall"))
            {
                mCurrentHealth -= 1;

                /*if (!mSoundEffects.isPlaying)
                {
                    mSoundEffects.PlayOneShot(mWarningBeepSound);
                    mSoundEffects.PlayOneShot(mWallImpactSound);
                }*/
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("LeftWall"))
            {
                ScrapeLeftAnimation.SetActive(false);
            }

            if (collision.gameObject.CompareTag("RightWall"))
            {
                ScrapeRightAnimation.SetActive(false);
            }

            /*if (mSoundEffects.isPlaying)
            {
                mSoundEffects.Stop();
            }*/
        }

        #endregion
        #region Private Implementation Functions/Methods used in this Class Only

        void Update()
        {
            mShootTimer += Time.deltaTime;

            mShipXPos = Input.mousePosition.x;
            mShipYPos = Input.mousePosition.y;

            ChangeAnimations();

            KeepPlayerInBounds();

            MyRigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(mNewXPos, mNewYPos, -Camera.main.transform.position.z));

            /*if (mCurrentHealth <= 0)
            {
                *//*if (!mSoundPower.isPlaying)
                {
                    mSoundPower.PlayOneShot(mPowerOffSound);
                }*//*

                Instantiate(ExplosionAnimation, transform.position, transform.rotation);
                UIActions.KillCount?.Invoke(0);

                mCurrentHealth = 5;
                Debug.Log("Game Over");
            }*/
        }

        private void KeepPlayerInBounds()
        {
            if (mShipXPos <= (Screen.width - Screen.width) + 50)
            {
                mNewXPos = 50;
            }
            else if (mShipXPos >= (Screen.width - 50))
            {
                mNewXPos = Screen.width - 50;
            }
            else
            {
                mNewXPos = Input.mousePosition.x;
            }
            if (mShipYPos <= (Screen.height - Screen.height) + 120)
            {
                mNewYPos = 120;
            }
            else if (mShipYPos >= (Screen.height - 400))
            {
                mNewYPos = Screen.height - 400;
            }
            else
            {
                mNewYPos = Input.mousePosition.y;
            }
        }

        private void ChangeAnimations()
        {
            if (Input.GetAxis("Mouse X") <= -0.1)
            {
                MyAnimator.SetLayerWeight(1, 1);
                MyAnimator.SetLayerWeight(2, 0);
            }

            if (Input.GetAxis("Mouse X") >= 0.1)
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetLayerWeight(2, 1);
            }

            if (Input.GetAxis("Mouse X") == 0)
            {
                MyAnimator.SetLayerWeight(0, 1);
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetLayerWeight(2, 0);
            }
        }

        private void Special()
        {
            if (mCanUseSpecial)
            {
                var newLaser = new GameObject[3];

                for (int i = 0 ; i < ProjectileSpawnPoints.Count ; i++)
                {
                    newLaser[i] = mLaserPool.GetLaserProjectile();

                    if (newLaser[i] != null)
                    {
                        newLaser[i].transform.position = ProjectileSpawnPoints[i].transform.position;
                    }

                    for (int j = 0 ; j < mLaserPool.GetLaserProjectiles.Count ; j++)
                    {
                        newLaser[i].SetActive(true);
                    }
                }
                mCanUseSpecial = false;
                FillSpecial();
            }
        }

        private void Shoot()
        {
            if (mShootTimer >= 0.3f)
            {
                var newLaser = mLaserPool.GetLaserProjectile();

                if (newLaser != null)
                {
                    newLaser.transform.position = ProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                    mCurrentFirePosition = (mCurrentFirePosition + 1) % ProjectileSpawnPoints.Count;
                }

                for (int i = 0 ; i < mLaserPool.GetLaserProjectiles.Count ; i++)
                {
                    newLaser.SetActive(true);
                }

                mShootTimer = 0;
            }
        }

        private void FillSpecial()
        {
            if (mFillSpecialMeter != null)
            {
                StopCoroutine(mFillSpecialMeter);
            }

            mFillSpecialMeter = StartCoroutine(FillSlider());
        }

        #endregion
        #region Private Coroutines continue until done without interuption

        private IEnumerator FillSlider()
        {
            float elapsedTime = 0f;
            HUDActions.UpdateSpecial(0);

            while (elapsedTime < 5)
            {
                HUDActions.UpdateSpecial(Mathf.Lerp(0f, 1f, elapsedTime / 5));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            HUDActions.UpdateSpecial(1);
            mCanUseSpecial = true;
            mFillSpecialMeter = null;
        }
        #endregion
        #region Public Interface Functions/Methods

        public void TakeDamage(float aDamage)
        {
            if (mCanTakeDamage)
            {
                if (mCurrentHealth - aDamage <= 0)
                {
                    Instantiate(ExplosionAnimation, transform.position, transform.rotation);
                }
                else
                {
                    mCurrentHealth -= aDamage;
                }
            }
        }

        #endregion
    }
}