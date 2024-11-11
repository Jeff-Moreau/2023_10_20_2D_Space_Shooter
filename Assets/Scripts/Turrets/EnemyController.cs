/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretBase.cs
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
using System.Collections.Generic;
using TMPro;

#region Public Enums: For Cross-Class References

public enum EnemyType
{
    Turret,
    ShipOne,
    ShipTwo
}

#endregion

namespace TrenchWars
{
    public class EnemyController : Entity, ITakeDamage
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.EnemyData _myData = null;
        [SerializeField] private Data.WeaponData _myMainWeaponData = null;
        [SerializeField] private Data.WeaponData _mySecondaryWeaponData = null;
        [Header("COLLIDERS >=========================================")]
        [SerializeField] protected BoxCollider2D _myTriggerCollider = null;
        [Header("SPAWN POINTS >======================================")]
        [SerializeField] private GameObject _mainWeapon = null;
        [SerializeField] private GameObject _weaponAttachmentPoint = null;
        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _audioSourceTakeDamage = null;
        [SerializeField] private AudioSource _audioSourceWeaponSound = null;
        #endregion
        #region Private Fields: For Internal Use

        private int _currentFirePosition;

        private bool _canFireWeapon;
        private bool _canTakeDamage;
        private bool _isWeaponFiring;
        private bool _movingDown;
        private float _currentHealth;

        private Coroutine _weaponActive;

        private GameObject _thePlayer;
        private GameObject _currentWeapon;

        private WeaponBase _currentWeaponScript;

        private ObjectPoolManager _levelObjectManager;
        private LevelControl _levelControl;
        private Vector3 _randomPatternOffset;
        private Vector3 _startPosition;
        private Vector3 _previousPosition;

        #endregion

        //METHOD
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();
            _levelControl = FindAnyObjectByType<LevelControl>();

            if (_levelObjectManager == null)
            {
                Debug.Log($"{gameObject.name} Cannot find the Level Object Manager!");
            }
        }

        private void Start()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            _canTakeDamage = false;
            _movingDown = false;
            _startPosition = transform.position;
            //_canFireWeapon = false;
            _isWeaponFiring = false;
            _currentFirePosition = 0;
            _currentHealth = _myData.GetMaxHealth;
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            EquipWeapon(_mainWeapon);
            _thePlayer = GameObject.FindGameObjectWithTag("Player");
            _weaponActive = null;
            _isWeaponFiring = false;
            _myRenderer.enabled = true;
            _myTriggerCollider.enabled = true;
            _randomPatternOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            _movingDown = false;
            _previousPosition = transform.position;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            switch (_myData.GetEnemyType)
            {
                case EnemyType.Turret:
                    TargetPlayer();
                    transform.position -= new Vector3(0, _myData.GetMovementSpeed * _levelControl.LevelSpeed * Time.deltaTime, 0);
                    break;

                case EnemyType.ShipOne:
                    TargetPlayer();
                    Vector3 directionToPlayer = _thePlayer.transform.position - transform.position;
                    Vector3 patternMovement = _randomPatternOffset * 0.75f;
                    Vector3 targetPosition = _thePlayer.transform.position + patternMovement;
                    Vector3 movementDirection = (targetPosition - transform.position).normalized;
                    transform.position += _myData.GetMovementSpeed * Time.deltaTime * movementDirection;
                    break;

                case EnemyType.ShipTwo:
                    if (!_movingDown)
                    {
                        MoveInRandomPattern();
                        CheckHalfwayPoint();
                    }
                    else
                    {
                        MoveDownwards();
                    }

                    RotateTowardsMovementDirection();

                    break;

                default:
                    break;
            }

            if (_mainWeapon != null)
            {
                _currentWeaponScript.FireWeapon(gameObject);
            }
        }

        void MoveInRandomPattern()
        {
            float xOffset = Mathf.Sin(Time.time * _myData.GetMovementSpeed) * 4f;
            transform.position = new Vector3(_startPosition.x + xOffset, transform.position.y - (_myData.GetMovementSpeed * 2 * Time.deltaTime), 0);
        }

        void CheckHalfwayPoint()
        {
            if (transform.position.y <= _thePlayer.transform.position.y)
            {
                _movingDown = true;
            }
        }

        void MoveDownwards()
        {
            transform.position += _myData.GetMovementSpeed * 5 * Time.deltaTime * Vector3.down;
        }

        void RotateTowardsMovementDirection()
        {
            Vector3 movementDirection = transform.position - _previousPosition;

            if (movementDirection != Vector3.zero)
            {
                float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            _previousPosition = transform.position;
        }

        private void TargetPlayer()
        {
            if (_thePlayer != null)
            {
                Vector3 rotation = _thePlayer.transform.position - transform.position;
                float zAxisRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, zAxisRotation);
            }
            else
            {
                _thePlayer = GameObject.FindGameObjectWithTag("Player");
            }
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnBecameVisible()
        {
            _canTakeDamage = true;
            //_canFireWeapon = true;
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void EquipWeapon(GameObject newWeapon)
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            if (_weaponAttachmentPoint != null)
            {
                _currentWeapon = Instantiate(newWeapon, _weaponAttachmentPoint.transform.position, _weaponAttachmentPoint.transform.rotation);
                _currentWeapon.transform.SetParent(_weaponAttachmentPoint.transform);
                _currentWeaponScript = _currentWeapon.GetComponent<WeaponBase>();
            }
        }

        private void OnDeath()
        {
            LevelActions.UpdateEnemiesKilled?.Invoke();
            //Update score with a value
            LevelActions.DropAPickup?.Invoke(gameObject.transform, _myData.GetMovementSpeed);
            //Make this call from pooled explosions and not instantiate a new one
            Instantiate(_myData.GetExplosionAnimation, transform.position, transform.rotation);
            StartCoroutine(WaitForSoundToStop());
            _currentHealth = _myData.GetMaxHealth;
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator WaitForSoundToStop()
        {
            // Happens when called
            _myTriggerCollider.enabled = false;
            _myRenderer.enabled = false;

            // Wait for this to happen
            yield return new WaitUntil(() => !_audioSourceWeaponSound.isPlaying && !_audioSourceTakeDamage.isPlaying);

            // Then do this after waiting
            gameObject.SetActive(false);
        }

        #endregion
        #region Public Methods: For External Interactions

        public void TakeDamage(float incomingDamage)
        {
            if (_canTakeDamage)
            {
                _currentHealth -= incomingDamage;
                _audioSourceTakeDamage.PlayOneShot(_myData.GetTakeDamageSound);

                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    OnDeath();
                }
            }
        }

        public void HealDamage(float incomingHeal)
        {
            return;
        }

        #endregion
        #region Private Deactivation Methods: For Class Exit and Cleanup

        private void OnBecameInvisible()
        {
            //_canFireWeapon = false;
            _canTakeDamage = false;
            _currentHealth = _myData.GetMaxHealth;
            if (!_audioSourceWeaponSound.isPlaying && !_audioSourceTakeDamage.isPlaying)
            {
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(WaitForSoundToStop());
            }
        }

        #endregion
    }
}