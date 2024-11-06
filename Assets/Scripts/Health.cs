/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: Health.cs
 * Date Created: November 6, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description: Can take Damage, Can be Healed, Can Die
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using System;
using UnityEngine;

namespace TrenchWars
{
    [DisallowMultipleComponent]
    public class Health : MonoBehaviour, ITakeDamage
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("ENTITY DATA >=======================================")]
        [SerializeField] private Data.EntityData _myData = null;

        #endregion
        #region Private Fields: For Internal Use

        private float _myCurrentHealth;

		#endregion

		public Action<float> OnHealthChange;
		public Action OnDeath;

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            _myCurrentHealth = _myData.GetMaxHealth;
            OnHealthChange?.Invoke(_myCurrentHealth);
        }

		#endregion
		#region Private Real-Time Methods: For Per-Frame Game Logic
		
		private void Update()
		{
			if (_myCurrentHealth <= 0)
			{
				Die();
			}
		}

		#endregion
		#region Private Fields: For Internal Use

		private void Die()
		{
			OnDeath?.Invoke();
		}

        #endregion
        #region Public Methods: For External Interactions

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public void HealDamage(float heal)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}