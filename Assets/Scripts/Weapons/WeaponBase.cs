/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: WeaponBase.cs
 * Date Created: November 8, 2024
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
	public class WeaponBase : Entity
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.WeaponData _myData = null;
        [Header("SPAWN POINTS >======================================")]
        [SerializeField] private GameObject[] _projectileSpawnPoints = null;
        [Header("AUDIO >=============================================")]
        [SerializeField] private AudioSource _myAudioSource = null;

        #endregion
        #region Private Fields: For Internal Use

        private bool _isFiring;

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
			InitializeFields();
		}
		
		private void InitializeFields()
		{
            _isFiring = false;
		}

        #endregion
        #region Public Methods: For External Interactions

        public void FireWeapon(GameObject owner)
		{
            if (!_isFiring)
            {
                StartCoroutine(Firing(owner));
            }
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator Firing(GameObject owner)
        {
            _isFiring = true;
            _myAudioSource.PlayOneShot(_myData.GetFireSound);

            for (int i = 0 ; i < _projectileSpawnPoints.Length ; i++)
            {
                GameObject myProjectile = _levelObjectManager.GetProjectile(_myData.GetProjectileType);

                if (myProjectile != null)
                {
                    myProjectile.GetComponent<ProjectileBase>().Owner = owner;
                    myProjectile.transform.position = _projectileSpawnPoints[i].transform.position;
                    myProjectile.transform.rotation = _projectileSpawnPoints[i].transform.rotation;
                    Vector2 launchDirection = _projectileSpawnPoints[i].transform.right;
                    myProjectile.SetActive(true);
                    myProjectile.GetComponent<ProjectileBase>().Launch(launchDirection);
                }
            }

            yield return new WaitForSeconds(_myData.GetFireRate);

            _isFiring = false;
        }

        #endregion
    }
}