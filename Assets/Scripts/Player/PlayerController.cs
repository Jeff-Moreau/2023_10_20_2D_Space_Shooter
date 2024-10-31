/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerController.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 29, 2024
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
        [SerializeField] private Data.PlayerData MyData = null;
        [SerializeField] private Animator MyAnimator = null;
        [SerializeField] private Rigidbody2D MyRigidbody = null;
        [SerializeField] private AudioSource MyAudioSource = null;

        [Header("Object Refrences")]
        [SerializeField] private GameObject ExplosionAnimation = null;
        [SerializeField] private GameObject ScrapeLeftAnimation = null;
        [SerializeField] private GameObject ScrapeRightAnimation = null;
        [SerializeField] private List<GameObject> ProjectileSpawnPoints = null;
        [SerializeField] private GameObject ProjectilePrefab = null;

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
        private ObjectPoolManager mLevelObjectManager;

        #endregion

        //FUNCTIONS
        #region Private Initialization Functions/Methods used in this Class Only

        private void OnEnable()
        {
            mLevelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (mLevelObjectManager == null)
            {
                Debug.LogError("ObjectPoolManager not found in the scene!");
            }

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

        private void OnTriggerEnter2D(Collider2D aCollision)
        {
            if (aCollision.gameObject.layer == 9)
            {
                mCurrentHealth = 5;
                aCollision.gameObject.SetActive(false);
            }
        }

        private void OnCollisionStay2D(Collision2D aCollision)
        {
            if (aCollision.gameObject.CompareTag("LeftWall"))
            {
                ScrapeLeftAnimation.SetActive(true);
            }

            if (aCollision.gameObject.CompareTag("RightWall"))
            {
                ScrapeRightAnimation.SetActive(true);
            }
        }

        private void OnCollisionEnter2D(Collision2D aCollision)
        {
            if (aCollision.gameObject.CompareTag("LeftWall"))
            {
                mCurrentHealth -= 1;
            }

            if (aCollision.gameObject.CompareTag("RightWall"))
            {
                mCurrentHealth -= 1;
            }
        }

        private void OnCollisionExit2D(Collision2D aCollision)
        {
            if (aCollision.gameObject.CompareTag("LeftWall"))
            {
                ScrapeLeftAnimation.SetActive(false);
            }

            if (aCollision.gameObject.CompareTag("RightWall"))
            {
                ScrapeRightAnimation.SetActive(false);
            }
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
        }

        private void KeepPlayerInBounds()
        {
            mNewXPos = mShipXPos <= Screen.width - Screen.width + 50
                ? 50
                : mShipXPos >= (Screen.width - 50) ? Screen.width - 50 : Input.mousePosition.x;

            mNewYPos = mShipYPos <= Screen.height - Screen.height + 120
                ? 120
                : mShipYPos >= (Screen.height - 400) ? Screen.height - 400 : Input.mousePosition.y;
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
                GameObject[] myProjectile = new GameObject[3];

                for (int i = 0 ; i < ProjectileSpawnPoints.Count ; i++)
                {
                    myProjectile[i] = mLevelObjectManager.GetObject(ProjectilePrefab);

                    if (myProjectile[i] != null)
                    {
                        myProjectile[i].transform.position = ProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                        mCurrentFirePosition = (mCurrentFirePosition + 1) % ProjectileSpawnPoints.Count;
                        myProjectile[i].SetActive(true);
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
                GameObject myProjectile = mLevelObjectManager.GetObject(ProjectilePrefab);

                if (myProjectile != null)
                {
                    myProjectile.GetComponent<ProjectileBase>().SetOwner(gameObject);
                    myProjectile.transform.position = ProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                    mCurrentFirePosition = (mCurrentFirePosition + 1) % ProjectileSpawnPoints.Count;
                    myProjectile.SetActive(true);
                    mShootTimer = 0;
                }
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