/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: TurretShooting.cs
 * Date Created: October 20, 2023
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
 ****************************************************************************************
 * TODO: TRANSFER ALL THIS TO THE BASE CLASS
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;

namespace TrenchWars
{
    public class TurretShooting : MonoBehaviour
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private Data.TurretData _myData = null;
        [SerializeField] private GameObject _mySpawnPoint = null;
        [SerializeField] private AudioSource _audioSourceWeaponSound = null;

        #endregion
        #region Private Fields: For Internal Use

        private GameObject _thePlayer;
        private float _shootTimer;
        private ObjectPoolManager _levelObjectManager;

        #endregion

        //METHODS
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _thePlayer = GameObject.FindGameObjectWithTag("Player");
            _levelObjectManager = FindObjectOfType<ObjectPoolManager>();

            if (_levelObjectManager == null)
            {
                Debug.Log($"{gameObject.name} Cannot find the Level Object Manager!");
            }

            _shootTimer = 1.5f;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
        {
            _shootTimer -= Time.deltaTime;
            TargetPlayer();
            ShootPlayer();
        }

        private void ShootPlayer()
        {
            if (_shootTimer <= 0)
            {
                GameObject myProjectile = _levelObjectManager.GetProjectile(_myData.GetProjectileUsed);

                if (myProjectile != null)
                {
                    myProjectile.GetComponent<ProjectileBase>().Owner = transform.parent.gameObject;
                    myProjectile.transform.SetPositionAndRotation(_mySpawnPoint.transform.position, _mySpawnPoint.transform.rotation);
                    //myProjectile.transform.position = ProjectileSpawnPoints[mCurrentFirePosition].transform.position;
                    //mCurrentFirePosition = (mCurrentFirePosition + 1) % ProjectileSpawnPoints.Count;
                    myProjectile.SetActive(true);
                    _shootTimer = 0;
                }

                //_audioSourceWeaponSound.PlayOneShot(_myData.GetShootingSound);
                _shootTimer = 1.5f;
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
    }
}