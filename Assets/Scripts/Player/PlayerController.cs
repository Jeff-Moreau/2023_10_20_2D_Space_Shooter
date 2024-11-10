/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: PlayerController.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 10, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using System.Collections;

namespace TrenchWars
{
    public class PlayerController : Entity, ITakeDamage
    {
        //ENUMS
        #region Private Enums: For Internal Use

        private enum ProjectileSpawn
        {
            Left,
            Middle,
            Right
        }

        #endregion

        //FIELDS
        #region Private Constants: For Class-Specific Fixed Values

        private const string MOUSE_X = "Mouse X";
        private const string VERTIVAL = "Vertical";
        private const string LEFT_WALL = "LeftWall";
        private const string RIGHT_WALL = "RightWall";
        private const string HORIZONTAL = "Horizontal";

        #endregion
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] protected Data.PlayerData _myData = null;
        [Header("SPAWN POINTS >======================================")]
        [SerializeField] private GameObject _startingWeapon = null;
        [SerializeField] private GameObject _weaponAttachmentPoint = null;
        [Header("ANIMATION >=========================================")]
        [SerializeField] private GameObject _explosionAnimation = null;
        [SerializeField] private GameObject _scrapeLeftAnimation = null;
        [SerializeField] private GameObject _scrapeRightAnimation = null;
        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _myAudioSource = null; // change for multiple audios

        #endregion
        #region Private Fields: For Internal Use

        private int _currentFirePosition;

        private bool _canUseSpecial;
        private bool _canTakeDamage;

        private float _newXPos;
        private float _newYPos;
        private float _shipXPos;
        private float _shipYPos;
        private float _shootTimer;
        private float _myCurrentHealth;

        private Coroutine _flash;
        private Coroutine _fillSpecialMeter;

        private GameObject _currentWeapon;

        private WeaponBase _currentWeaponScript;

        private ObjectPoolManager _levelObjectManager;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public float GetCurrentHealth => _myCurrentHealth;

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Start()
        {
            InitializeFields();
            Cursor.visible = false; // should be in level or game manager
            Cursor.lockState = CursorLockMode.Confined; // should be in level or game manager

            FillSpecial();
        }

        private void InitializeFields()
        {
            _fillSpecialMeter = null;
            _myCurrentHealth = _myData.GetMaxHealth;
            PlayerActions.CurrentHealth?.Invoke(_myCurrentHealth / _myData.GetMaxHealth);
            _shipXPos = 0.0f;
            _shipYPos = 0.0f;
            _newXPos = 0.0f;
            _newYPos = 0.0f;
            _shootTimer = 0.3f;
            _currentFirePosition = 0;
            _canTakeDamage = true;
            _canUseSpecial = _fillSpecialMeter == null ? false : true;
            EquipWeapon(_startingWeapon);
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (_levelObjectManager == null)
            {
                Debug.LogError("ObjectPoolManager not found in the scene!");
            }

            PlayerActions.CurrentHealth?.Invoke(_myCurrentHealth / _myData.GetMaxHealth);
            InputActions.FireKey += Shoot;
            InputActions.SpecialKey += Special;
        }

        private void OnDisable()
        {
            InputActions.FireKey -= Shoot;
            InputActions.SpecialKey -= Special;
        }

        #endregion
        #region Private Physics Methods: For Object Interactions

        private void OnCollisionStay2D(Collision2D whatImTouching)
        {
            if (whatImTouching.gameObject.CompareTag(LEFT_WALL))
            {
                _scrapeLeftAnimation.SetActive(true);
            }

            if (whatImTouching.gameObject.CompareTag(RIGHT_WALL))
            {
                _scrapeRightAnimation.SetActive(true);
            }
        }

        private void OnCollisionEnter2D(Collision2D whatIHit)
        {
            if (whatIHit.gameObject.CompareTag(LEFT_WALL))
            {
                TakeDamage(1);
            }

            if (whatIHit.gameObject.CompareTag(RIGHT_WALL))
            {
                TakeDamage(1);
            }
        }

        private void OnCollisionExit2D(Collision2D whatIHit)
        {
            if (whatIHit.gameObject.CompareTag(LEFT_WALL))
            {
                _scrapeLeftAnimation.SetActive(false);
            }

            if (whatIHit.gameObject.CompareTag(RIGHT_WALL))
            {
                _scrapeRightAnimation.SetActive(false);
            }
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        void Update()
        {
            _shootTimer += Time.deltaTime;
            _shipXPos = Input.mousePosition.x;
            _shipYPos = Input.mousePosition.y;

            ChangeAnimations();

            KeepPlayerInBounds();

            //MyRigidbody.position = Camera.main.ScreenToWorldPoint(new Vector3(mNewXPos, mNewYPos, -Camera.main.transform.position.z));

            float moveX = Input.GetAxis(HORIZONTAL); // Left thumbstick X-axis
            float moveY = Input.GetAxis(VERTIVAL);   // Left thumbstick Y-axis

            // Calculate movement direction
            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

            // Apply velocity for movement
            _myRigidbody.velocity = moveDirection * _myData.GetMovementSpeed;
        }

        private void KeepPlayerInBounds()
        {
            _newXPos = _shipXPos <= Screen.width - Screen.width + 50
                ? 50
                : _shipXPos >= (Screen.width - 50) ? Screen.width - 50 : Input.mousePosition.x;

            _newYPos = _shipYPos <= Screen.height - Screen.height + 120
                ? 120
                : _shipYPos >= (Screen.height - 400) ? Screen.height - 400 : Input.mousePosition.y;
        }

        private void ChangeAnimations()
        {
            if (Input.GetAxis(MOUSE_X) <= -0.1 || Input.GetAxis(HORIZONTAL) <= -0.1)
            {
                _myAnimator.SetLayerWeight(1, 1);
                _myAnimator.SetLayerWeight(2, 0);
            }

            if (Input.GetAxis(MOUSE_X) >= 0.1 || Input.GetAxis(HORIZONTAL) <= 0.1)
            {
                _myAnimator.SetLayerWeight(1, 0);
                _myAnimator.SetLayerWeight(2, 1);
            }

            if (Input.GetAxis(MOUSE_X) == 0 || Input.GetAxis(HORIZONTAL) <= 0)
            {
                _myAnimator.SetLayerWeight(0, 1);
                _myAnimator.SetLayerWeight(1, 0);
                _myAnimator.SetLayerWeight(2, 0);
            }
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void Special()
        {
            /*if (_canUseSpecial)
            {
                var myProjectile = new GameObject[3];

                for (int i = 0 ; i < _projectileSpawnPoints.Count ; i++)
                {
                    myProjectile[i] = _levelObjectManager.GetProjectile(_projectilePrefab);

                    if (myProjectile[i] != null)
                    {
                        myProjectile[i].GetComponent<ProjectileBase>().Owner = gameObject;
                        myProjectile[i].transform.position = _projectileSpawnPoints[_currentFirePosition].transform.position;
                        _currentFirePosition = (_currentFirePosition + 1) % _projectileSpawnPoints.Count;
                        myProjectile[i].SetActive(true);
                    }
                }

                _canUseSpecial = false;
                FillSpecial();
            }*/
        }

        private void Shoot()
        {
            _currentWeaponScript.FireWeapon(gameObject);
        }

        private void FillSpecial()
        {
            if (_fillSpecialMeter != null)
            {
                StopCoroutine(_fillSpecialMeter);
            }

            _fillSpecialMeter = StartCoroutine(FillSlider());
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator FillSlider()
        {
            float elapsedTime = 0f;
            PlayerActions.SpecialTimer(0);

            while (elapsedTime < 5)
            {
                PlayerActions.SpecialTimer(Mathf.Lerp(0f, 1f, elapsedTime / 5));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            PlayerActions.SpecialTimer(1);
            _canUseSpecial = true;
            _fillSpecialMeter = null;
        }

        private IEnumerator Flash(int aFlashCount, float aDuration)
        {
            for (int i = 0 ; i < aFlashCount ; i++)
            {
                _myRenderer.enabled = !_myRenderer.enabled; // Toggle visibility
                yield return new WaitForSeconds(aDuration);
            }

            _myRenderer.enabled = true; // Ensure it's visible at the end
            _canTakeDamage = true;
        }

        #endregion
        #region Public Methods: For External Interactions

        public void TakeDamage(float incomingDamage)
        {
            if (_canTakeDamage)
            {
                if (_myCurrentHealth - incomingDamage <= 0)
                {
                    _myCurrentHealth = 0;
                    PlayerActions.CurrentHealth?.Invoke(0);
                    Instantiate(_explosionAnimation, transform.position, transform.rotation);
                    _canTakeDamage = false;

                    if (_flash == null)
                    {
                        _flash = StartCoroutine(Flash(10, 0.4f));
                    }
                    else
                    {
                        StopCoroutine(_flash);
                        _flash = StartCoroutine(Flash(10, 0.4f));
                    }
                }
                else
                {
                    _myCurrentHealth -= incomingDamage;
                    PlayerActions.CurrentHealth?.Invoke(_myCurrentHealth / _myData.GetMaxHealth);

                    if (_flash == null)
                    {
                        _flash = StartCoroutine(Flash(2, 0.1f));
                    }
                    else
                    {
                        StopCoroutine(_flash);
                        _flash = StartCoroutine(Flash(2, 0.1f));
                    }
                }
            }
        }

        public void HealDamage(float incomingHeal)
        {
            _myCurrentHealth = Mathf.Min(_myCurrentHealth + incomingHeal, _myData.GetMaxHealth);
            PlayerActions.CurrentHealth?.Invoke(_myCurrentHealth / _myData.GetMaxHealth);
        }

        public void EquipWeapon(GameObject newWeapon)
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            _currentWeapon = Instantiate(newWeapon, _weaponAttachmentPoint.transform.position, _weaponAttachmentPoint.transform.rotation);
            _currentWeapon.transform.SetParent(_weaponAttachmentPoint.transform);
            _currentWeaponScript = _currentWeapon.GetComponent<WeaponBase>();
        }

        #endregion
    }
}