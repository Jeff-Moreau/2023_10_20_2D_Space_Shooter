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

namespace TrenchWars
{
    public class EnemyController : Entity, ITakeDamage
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.TurretData _myData = null;
        [SerializeField] private Data.WeaponData _myMainWeaponData = null;
        [SerializeField] private Data.WeaponData _mySecondaryWeaponData = null;
        [Header("COLLIDERS >=========================================")]
        [SerializeField] protected BoxCollider2D _myTriggerCollider = null;
        [Header("SPAWN POINTS >======================================")]
        [SerializeField] private List<GameObject> _projectileSpawnPoints = null;
        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _audioSourceTakeDamage = null;
        [SerializeField] private AudioSource _audioSourceWeaponSound = null;

        #endregion
        #region Private Fields: For Internal Use

        private int _currentFirePosition;

        private bool _canFireWeapon;
        private bool _canTakeDamage;
        private bool _isWeaponFiring;

        private float _currentHealth;
        private float _fireWeaponTimer;

        private Coroutine _weaponActive;

        private GameObject _thePlayer;

        private ObjectPoolManager _levelObjectManager;

        #endregion

        //METHOD
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();

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
            _canFireWeapon = false;
            _isWeaponFiring = false;
            _currentFirePosition = 0;
            _currentHealth = _myData.GetMaxHealth;
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _thePlayer = GameObject.FindGameObjectWithTag("Player");
            _fireWeaponTimer = 2.0f;
            _weaponActive = null;
            _isWeaponFiring = false;
            _myRenderer.enabled = true;
            _myTriggerCollider.enabled = true;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            transform.position -= new Vector3(0, _myData.GetMovementSpeed * Time.deltaTime, 0);

            TargetPlayer();

            if (_canFireWeapon && !_isWeaponFiring)
            {
                if (_weaponActive != null)
                {
                    StopCoroutine(_weaponActive);
                    _weaponActive = StartCoroutine(WeaponBurst());
                }
                else
                {
                    _weaponActive = StartCoroutine(WeaponBurst());
                }
            }
        }

        private void FireWeapon()
        {
            GameObject myProjectile = _levelObjectManager.GetProjectile(_myMainWeaponData.GetProjectileType);

            if (myProjectile != null)
            {
                myProjectile.GetComponent<ProjectileBase>().Owner = transform.gameObject;
                myProjectile.transform.SetPositionAndRotation(_projectileSpawnPoints[_currentFirePosition].transform.position, _projectileSpawnPoints[_currentFirePosition].transform.rotation);
                myProjectile.transform.rotation = _projectileSpawnPoints[_currentFirePosition].transform.rotation;
                _currentFirePosition = (_currentFirePosition + 1) % _projectileSpawnPoints.Count;
                myProjectile.SetActive(true);
            }
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
            _canFireWeapon = true;
        }

        #endregion
        #region Private Implementation Methods: For Class Use

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

        private IEnumerator WeaponBurst()
        {
            // Happens when called
            _isWeaponFiring = true;
            _audioSourceWeaponSound.PlayOneShot(_myMainWeaponData.GetFireSound);

            for (int i = 0 ; i < 4 ; i++)
            {
                FireWeapon();
                // Wait for this to happen
                yield return new WaitForSeconds(_myMainWeaponData.GetFireRate);
            }

            // Wait for this to happen
            yield return new WaitForSeconds(_fireWeaponTimer);

            // Then do this after waiting
            _isWeaponFiring = false;
        }

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
            _canFireWeapon = false;
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