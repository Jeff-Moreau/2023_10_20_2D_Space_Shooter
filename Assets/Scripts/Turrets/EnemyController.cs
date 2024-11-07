/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretBase.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
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
        [SerializeField] protected Data.TurretData _myData = null;

        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _audioSourceTakeDamage = null;
        
        #endregion
        #region Private Fields: For Internal Use

        private float _currentHealth;
        private bool _canTakeDamage;

        #endregion

        //METHOD
        #region Private Initialization Methods: For Class Setup

        private void Start()
        {
            _currentHealth = _myData.GetMaxHealth;
            _canTakeDamage = false;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            transform.position -= new Vector3(0, _myData.GetMovementSpeed * Time.deltaTime, 0);
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnBecameVisible()
        {
            _canTakeDamage = true;
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void OnDeath()
        {
            LevelActions.UpdateEnemiesKilled?.Invoke();
            //Update score witha value
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
            _currentHealth = _myData.GetMaxHealth;
        }

        #endregion
    }
}