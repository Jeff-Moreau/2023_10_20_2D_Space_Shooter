/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPoolManager.cs
 * Date Created: October 29, 2024
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
using System.Collections.Generic;

namespace TrenchWars
{
	public class ObjectPoolManager : MonoBehaviour
	{
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private List<ObjectPoolStruct> _theProjectiles = null;
        [SerializeField] private List<ObjectPoolStruct> _theEnemies = null;
        [SerializeField] private List<ObjectPoolStruct> _thePickups = null;

        #endregion
        #region Private Fields: For Internal Use

        private Dictionary<GameObject, ObjectPool> _enemyList;
        private Dictionary<GameObject, ObjectPool> _pickupList;
        private Dictionary<GameObject, ObjectPool> _projectileList;

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            _projectileList = new Dictionary<GameObject, ObjectPool>();
            _enemyList = new Dictionary<GameObject, ObjectPool>();
            _pickupList = new Dictionary<GameObject, ObjectPool>();

            InitializePool(_theProjectiles, _projectileList);
            InitializePool(_theEnemies, _enemyList);
            InitializePool(_thePickups, _pickupList);
        }

        private void InitializePool(List<ObjectPoolStruct> thePool, Dictionary<GameObject, ObjectPool> theList)
        {
            foreach (ObjectPoolStruct pool in thePool)
            {
                ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
                objectPool.Prefab = pool.ThePrefab;
                objectPool.PoolSize = pool.MinimumPoolSize;
                objectPool.MaxPoolSize = pool.MaximumPoolSize;

                theList.Add(pool.ThePrefab, objectPool);
            }
        }

        #endregion
        #region Public Methods: For External Interactions

        public GameObject GetEnemy(GameObject prefab)
        {
            if (_enemyList.TryGetValue(prefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {prefab.name} Pool initialized!");
            return null;
        }

        public GameObject GetRandomEnemy()
        {
            var key = new List<GameObject>(_enemyList.Keys);
            int randomEnemy = Random.Range(0, key.Count);
            GameObject randomkey = key[randomEnemy];
            return GetEnemy(randomkey);
        }

        public GameObject GetPickup(GameObject prefab)
        {
            if (_pickupList.TryGetValue(prefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {prefab.name} Pool initialized!");
            return null;
        }

        public GameObject GetProjectile(GameObject prefab)
        {
            if (_projectileList.TryGetValue(prefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {prefab.name} Pool initialized!");
            return null;
        }

        #endregion
    }
}