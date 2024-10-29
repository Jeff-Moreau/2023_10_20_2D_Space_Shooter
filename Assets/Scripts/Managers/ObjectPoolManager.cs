/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPoolManager.cs
 * Date Created: October 29, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 29, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

//ENUMERATORS
#region Public Enumerator Declarations Only

// public enum eEnumName  // Example
// {
// 		Hey,
//		You
// }

#endregion

namespace TrenchWars
{
	public class ObjectPoolManager : MonoBehaviour
	{
        [System.Serializable]
        public class Pool
        {
            public GameObject prefab; // Prefab to pool
            public int initialSize; // Initial size of the pool
            public int maxSize; // Maximum size of the pool
        }

        [SerializeField] private GameObject PoolContainer = null;
        [SerializeField] private List<Pool> pools; // List of pools

        private Dictionary<GameObject, ObjectPool> poolDictionary; // Dictionary to access pools by prefab

        private void Awake()
        {
            InitializePools();
        }

        // Initialize the object pools based on the predefined settings
        private void InitializePools()
        {
            poolDictionary = new Dictionary<GameObject, ObjectPool>();

            foreach (var pool in pools)
            {
                ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
                objectPool.SetPrefab(pool.prefab);
                objectPool.SetPoolSize(pool.initialSize);
                objectPool.SetMaxPoolSize(pool.maxSize);
                objectPool.transform.SetParent(PoolContainer.transform);

                poolDictionary.Add(pool.prefab, objectPool);
            }
        }

        // Get an object from the specified prefab's pool
        public GameObject GetObject(GameObject prefab)
        {
            if (poolDictionary.TryGetValue(prefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab(); // Get object from the respective pool
            }

            Debug.LogWarning($"No pool found for prefab: {prefab.name}");
            return null; // Return null if the pool doesn't exist
        }
    }
}