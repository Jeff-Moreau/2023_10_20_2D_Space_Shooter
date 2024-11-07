/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretBase.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 7, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
    public class EnemyController : Entity, ITakeDamage
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.TurretData _myData = null;
        [SerializeField] private Data.WeaponData _myMainWeapon = null;
        [SerializeField] private Data.WeaponData _mySecondaryWeapon = null;

        [Header("SPAWN POINTS >======================================")]
        [SerializeField] private GameObject _mySpawnPoint = null;

        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _audioSourceTakeDamage = null;
        [SerializeField] private AudioSource _audioSourceWeaponSound = null;

        #endregion
        #region Private Fields: For Internal Use

        private float _currentHealth;
        private bool _canTakeDamage;
        private bool _canFireWeapon;
        private float _shootTimer;
        private GameObject _thePlayer;
        private ObjectPoolManager _levelObjectManager;

        #endregion

        //METHOD
        #region Private Initialization Methods: For Class Setup

        private void Start()
        {
            _currentHealth = _myData.GetMaxHealth;
            _canTakeDamage = false;
            _canFireWeapon = false;
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _thePlayer = GameObject.FindGameObjectWithTag("Player");
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (_levelObjectManager == null)
            {
                Debug.Log($"{gameObject.name} Cannot find the Level Object Manager!");
            }

            _shootTimer = Random.Range(0.5f, 2.5f);
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            transform.position -= new Vector3(0, _myData.GetMovementSpeed * Time.deltaTime, 0);
            _shootTimer -= Time.deltaTime;
            TargetPlayer();
            ShootPlayer();
        }

        private void ShootPlayer()
        {
            if (_shootTimer <= 0 && _canFireWeapon)
            {
                GameObject myProjectile = _levelObjectManager.GetProjectile(_myMainWeapon.GetProjectileType);

                if (myProjectile != null)
                {
                    myProjectile.GetComponent<ProjectileBase>().Owner = transform.gameObject;
                    myProjectile.transform.SetPositionAndRotation(_mySpawnPoint.transform.position, _mySpawnPoint.transform.rotation);
                    //myProjectile.transform.position = ProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                    //mCurrentFirePosition = (mCurrentFirePosition + 1) % ProjectileSpawnPoints.Count;
                    myProjectile.SetActive(true);
                    _shootTimer = 0;
                }

                _audioSourceWeaponSound.PlayOneShot(_myMainWeapon.GetFireSound);
                _shootTimer = _myMainWeapon.GetFireRate;
            }
        }

        private void TargetPlayer()
        {
            if (_thePlayer != null)
            {
                Vector3 rotation = _thePlayer.transform.position - transform.position;
                float zAxisRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, zAxisRotation - 90);
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
            gameObject.SetActive(false);
            _currentHealth = _myData.GetMaxHealth;
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
            gameObject.SetActive(false);
            _canTakeDamage = false;
            _canFireWeapon = false;
            _currentHealth = _myData.GetMaxHealth;
        }

        #endregion
    }
}