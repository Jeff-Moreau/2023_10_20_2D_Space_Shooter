/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: WeaponBase.cs
 * Date Created: November 8, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 9, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

namespace TrenchWars
{
	public class WeaponBase : Entity
	{
		//FIELDS
		#region Private Serialized Fields: For Inspector Editable Values

		[SerializeField] private Data.WeaponData _myData = null;
		[SerializeField] private List<GameObject> _projectileSpawnPoints = null;

        #endregion
        #region Private Fields: For Internal Use

        private int _currentFirePosition;

        private float _shootTimer;

        private ObjectPoolManager _levelObjectManager;

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
		{
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (_levelObjectManager == null)
            {
                Debug.LogError("ObjectPoolManager not found in the scene!");
            }
        }
		
		private void Start()
		{
			InitializeVariables();
		}
		
		private void InitializeVariables()
		{
            _shootTimer = 0.3f;
            _currentFirePosition = 0;
		}

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
		{
			_shootTimer += Time.deltaTime;
		}

        #endregion
        #region Public Methods: For External Interactions

        public void FireWeapon(GameObject owner)
		{
            // replace with coroutine
            if (_shootTimer >= 0.3f)
            {
                GameObject myProjectile = _levelObjectManager.GetProjectile(_myData.GetProjectileType);

                if (myProjectile != null)
                {
                    myProjectile.GetComponent<ProjectileBase>().Owner = owner;
                    myProjectile.transform.position = _projectileSpawnPoints[_currentFirePosition].transform.position;
                    _currentFirePosition = (_currentFirePosition + 1) % _projectileSpawnPoints.Count;
                    myProjectile.SetActive(true);
                    _shootTimer = 0;
                }
            }
        }

        #endregion
    }
}