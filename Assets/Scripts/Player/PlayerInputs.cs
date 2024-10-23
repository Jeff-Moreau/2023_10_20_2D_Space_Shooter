/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerInputs.cs
 * Date Created: October 20, 2023
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
using System.Collections;
using System.Collections.Generic;

namespace TrenchWars
{
    public class PlayerInputs : MonoBehaviour, ITakeDamage
    {
        #region Old Code

        private enum eProjectileSpawn
        {
            Left,
            Middle,
            Right
        }

        [Header("Player Information")]
        [SerializeField] private Animator mAnimator = null;
        [SerializeField] private Rigidbody2D mRigidbody = null;
        [SerializeField] private GameObject mLaserAmmo = null;

        [Header("Object Refrences")]
        [SerializeField] private GameObject mExplosion = null;
        [SerializeField] private GameObject ScrapeLeft = null;
        [SerializeField] private GameObject ScrapeRight = null;
        [SerializeField] private LaserPool mLaserPool = null;
        [SerializeField] private Data.ProjectileData mLaser = null;
        [SerializeField] private List<GameObject> mProjectileSpawnPoints = null;

        private float mNewXPos;
        private float mNewYPos;
        private float mShipXPos;
        private float mShipYPos;
        private float mShootTimer;
        private bool mCanUseSpecial;
        private float mCurrentHealth;
        private int mCurrentFirePosition;
        private Coroutine mFillSpecialMeter;

        public float GetHealth => mCurrentHealth;

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

        private void Special()
        {
            if (mCanUseSpecial)
            {
                var newLaser = new GameObject[3];

                for (int i = 0 ; i < mProjectileSpawnPoints.Count ; i++)
                {
                    newLaser[i] = mLaserPool.GetLaserProjectile();

                    if (newLaser[i] != null)
                    {
                        newLaser[i].transform.position = mProjectileSpawnPoints[i].transform.position;
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
                    newLaser.transform.position = mProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                    mCurrentFirePosition = (mCurrentFirePosition + 1) % mProjectileSpawnPoints.Count;
                }

                for (int i = 0 ; i < mLaserPool.GetLaserProjectiles.Count ; i++)
                {
                    newLaser.SetActive(true);
                }

                mShootTimer = 0;
            }
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
            mCanUseSpecial = mFillSpecialMeter == null ? false : true;
        }

        private void FillSpecial()
        {
            if (mFillSpecialMeter != null)
            {
                StopCoroutine(mFillSpecialMeter);
            }

            mFillSpecialMeter = StartCoroutine(FillSlider());
        }

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

        void Update()
        {
            mShootTimer += Time.deltaTime;

            mShipXPos = Input.mousePosition.x;
            mShipYPos = Input.mousePosition.y;

            ChangeAnimations();

            KeepPlayerInBounds();

            mRigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(mNewXPos, mNewYPos, -Camera.main.transform.position.z));

            if (mCurrentHealth <= 0)
            {
                /*if (!mSoundPower.isPlaying)
                {
                    mSoundPower.PlayOneShot(mPowerOffSound);
                }*/

                Instantiate(mExplosion, transform.position, transform.rotation);
                UIActions.KillCount?.Invoke(0);

                mCurrentHealth = 5;
                Debug.Log("Game Over");
            }
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
                mAnimator.SetLayerWeight(1, 1);
                mAnimator.SetLayerWeight(2, 0);
            }

            if (Input.GetAxis("Mouse X") >= 0.1)
            {
                mAnimator.SetLayerWeight(1, 0);
                mAnimator.SetLayerWeight(2, 1);
            }

            if (Input.GetAxis("Mouse X") == 0)
            {
                mAnimator.SetLayerWeight(0, 1);
                mAnimator.SetLayerWeight(1, 0);
                mAnimator.SetLayerWeight(2, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 8)
            {
                mCurrentHealth -= 1;
                collision.gameObject.SetActive(false);
            }

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
                ScrapeLeft.SetActive(true);
            }

            if (collision.gameObject.CompareTag("RightWall"))
            {
                /*if (!mSoundEffects.isPlaying)
                {
                    mSoundEffects.PlayOneShot(mWallImpactSound);
                }*/
                ScrapeRight.SetActive(true);
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
                ScrapeLeft.SetActive(false);
            }

            if (collision.gameObject.CompareTag("RightWall"))
            {
                ScrapeRight.SetActive(false);
            }

            /*if (mSoundEffects.isPlaying)
            {
                mSoundEffects.Stop();
            }*/
        }

        public void TakeDamage(float aDamage) => throw new System.NotImplementedException();

        #endregion
    }
}