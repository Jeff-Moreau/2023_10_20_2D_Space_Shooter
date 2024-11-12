/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretBase.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 12, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using System.Collections;

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
        [SerializeField] protected Data.EnemyData _myData = null;
        [SerializeField] protected Data.WeaponData _myMainWeaponData = null;
        [Header("COLLIDERS >=========================================")]
        [SerializeField] protected BoxCollider2D _myTriggerCollider = null;
        [Header("SPAWN POINTS >======================================")]
        [SerializeField] protected GameObject _mainWeapon = null;
        [SerializeField] protected GameObject _weaponAttachmentPoint = null;
        [Header("AUDIO >=============================================")]
        [SerializeField] protected AudioSource _audioSourceTakeDamage = null;
        [SerializeField] protected AudioSource _audioSourceWeaponSound = null;
        #endregion
        #region Private Fields: For Internal Use

        private bool _movingDown; // Ship Two
        protected bool _canTakeDamage; // All enemies

        protected float _currentHealth; // All enemies

        private Vector3 _startPosition; // Ship Two
        private Vector3 _previousPosition; // Ship Two
        private Vector3 _randomPatternOffset; // Ship Two

        protected GameObject _thePlayer; // All enemies
        private GameObject _currentWeapon;

        private WeaponBase _currentWeaponScript;

        protected LevelControl _levelControl; // All enemies

        protected ObjectPoolManager _levelObjectManager; // All enemies

        #endregion

        //METHOD
        #region Private Initialization Methods: For Class Setup

        protected virtual void Awake()
        {
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (_levelObjectManager == null)
            {
                Debug.Log($"{gameObject.name} Cannot find the Level Object Manager!");
            }
        }

        protected virtual void Start()
        {
            InitializeFields();
        }

        protected virtual void InitializeFields()
        {
            _movingDown = false;
            _canTakeDamage = false;
            _startPosition = transform.position; // Ship Two
            _currentHealth = _myData.GetMaxHealth;
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        protected virtual void OnEnable()
        {
            _levelControl = FindObjectOfType<LevelControl>();

            if (_levelObjectManager == null)
            {
                Debug.Log($"{gameObject.name} Cannot find the Level Controller!");
            }

            _movingDown = false;
            _myRenderer.enabled = true;
            _myTriggerCollider.enabled = true;
            _previousPosition = transform.position;
            _thePlayer = GameObject.FindGameObjectWithTag("Player");
            _randomPatternOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            EquipWeapon(_mainWeapon);
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        protected virtual void Update()
        {
            switch (_myData.GetEnemyType)
            {
                case EnemyType.Turret:
/*                    TargetPlayer();
                    transform.position -= new Vector3(0, _myData.GetMovementSpeed * _levelControl.LevelSpeed * Time.deltaTime, 0);*/
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

        protected void TargetPlayer()
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

        protected void OnBecameVisible()
        {
            _canTakeDamage = true;
            //_canFireWeapon = true;
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        protected void EquipWeapon(GameObject newWeapon)
        {
            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            if (_weaponAttachmentPoint != null)
            {
                _currentWeapon = Instantiate(newWeapon, _weaponAttachmentPoint.transform.position, _weaponAttachmentPoint.transform.rotation);
                _currentWeapon.GetComponent<SpriteRenderer>().sortingOrder = _myRenderer.sortingOrder + 1;
                _currentWeapon.transform.SetParent(_weaponAttachmentPoint.transform);
                _currentWeaponScript = _currentWeapon.GetComponent<WeaponBase>();
            }
        }

        protected void OnDeath()
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

        protected IEnumerator WaitForSoundToStop()
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

        public virtual void TakeDamage(float incomingDamage)
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

        public virtual void HealDamage(float incomingHeal)
        {
            return;
        }

        #endregion
        #region Private Deactivation Methods: For Class Exit and Cleanup

        protected void OnBecameInvisible()
        {
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