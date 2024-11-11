/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: LevelControl.cs
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

namespace TrenchWars
{
    public class LevelControl : MonoBehaviour
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.LevelData _myData = null;
        [SerializeField] private Data.PlayerData _thePlayerData = null;
        [Header("SPAWN LOCATIONS >===================================")]
        [SerializeField] private GameObject _playerSpawnLocation = null;
        [SerializeField] private GameObject[] _turretSpawnLocations = null;
        [Header("OBJECT REFERENCES >=================================")]
        [SerializeField] private ObjectPoolManager _levelObjectManager = null;
        [SerializeField] private GameObject _thePlayer = null;
        [SerializeField] private GameObject _theTurret = null; // Use object pool for this
        [SerializeField] private GameObject _theHealthPickup = null; // Use object pool for this
        [SerializeField] private GameObject _theWeaponTwoUpgrade = null;

        #endregion
        #region Private Fields: For Internal Use

        private int _currentEnemyKills;

        private float _spawnTimer;
        private float _playerHealth;
        private float _spawnTimeLimit;

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Start()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            _spawnTimeLimit = 5; // Scriptable Object for this
            _currentEnemyKills = 0;
            _spawnTimer = _spawnTimeLimit;
            _playerHealth = _thePlayerData.GetMaxHealth;
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            Instantiate(_thePlayer, _playerSpawnLocation.transform.position, _playerSpawnLocation.transform.rotation);
            LevelActions.UpdateEnemiesKilled += AddKills;
            LevelActions.DropAPickup += DropPickup;
            PlayerActions.CurrentHealth += PlayerHealth;
        }

        private void OnDisable()
        {
            LevelActions.UpdateEnemiesKilled -= AddKills;
            LevelActions.DropAPickup -= DropPickup;
            PlayerActions.CurrentHealth -= PlayerHealth;
        }

        #endregion
        #region Private Real-Time Methods: For Per-Frame Game Logic

        private void Update()
		{
            // Turn all of this into a coroutine
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _spawnTimeLimit)
            {
                GameObject newTurret = _levelObjectManager.GetEnemy(_theTurret);
                int randomSpawn = Random.Range(0, 3);

                if (newTurret != null)
                {
                    newTurret.transform.position = _turretSpawnLocations[randomSpawn].transform.position;
                    newTurret.SetActive(true);
                    _spawnTimer = 0;
                    _spawnTimeLimit = Random.Range(3, 10);
                }
            }
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void AddKills()
        {
            _currentEnemyKills += 1;
        }

        private void PlayerHealth(float amount)
        {
            _playerHealth = amount * _thePlayerData.GetMaxHealth;
        }

        private void DropPickup(Transform dropLocation, float moveSpeed)
        {
            //Debug.Log(_playerHealth);

            if (_playerHealth < _thePlayerData.GetMaxHealth)
            {
                float randomChance = Random.Range(0, 100);

                if (randomChance <= 50)
                {
                    GameObject newPickup = _levelObjectManager.GetPickup(_theHealthPickup);

                    if (newPickup != null)
                    {
                        newPickup.transform.SetPositionAndRotation(dropLocation.position, dropLocation.rotation);
                        newPickup.SetActive(true);
                    }
                }
            }
            else
            {
                float randomChance = Random.Range(0, 100);

                if (randomChance <= 30)
                {
                    GameObject newPickup = _levelObjectManager.GetPickup(_theWeaponTwoUpgrade);

                    if (newPickup != null)
                    {
                        newPickup.transform.SetPositionAndRotation(dropLocation.position, dropLocation.rotation);
                        newPickup.SetActive(true);
                    }
                }
            }
        }

        #endregion
    }
}