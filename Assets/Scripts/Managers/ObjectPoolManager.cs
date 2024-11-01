/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPoolManager.cs
 * Date Created: October 29, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 30, 2024
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
        //VARIABLES
        #region Private Variables/Fields Exposed to Inspector for Editing

        [SerializeField] private List<ObjectPoolStruct> TheProjectiles = null;
        [SerializeField] private List<ObjectPoolStruct> TheEnemies = null;
        [SerializeField] private List<ObjectPoolStruct> ThePickups = null;

        #endregion
        #region Private Variables/Fields used in this Class Only

        private Dictionary<GameObject, ObjectPool> mProjectileList;
        private Dictionary<GameObject, ObjectPool> mEnemyList;
        private Dictionary<GameObject, ObjectPool> mPickupList;

        #endregion

        //FUCNTIONS
        #region Private Initialization Functions/Methods used in this Class Only

        private void Awake() => InitializePools();

        private void InitializePools()
        {
            mProjectileList = new Dictionary<GameObject, ObjectPool>();
            mEnemyList = new Dictionary<GameObject, ObjectPool>();
            mPickupList = new Dictionary<GameObject, ObjectPool>();

            InitializePool(TheProjectiles, mProjectileList);
            InitializePool(TheEnemies, mEnemyList);
            InitializePool(ThePickups, mPickupList);
        }

        private void InitializePool(List<ObjectPoolStruct> aThePool, Dictionary<GameObject, ObjectPool> aTheList)
        {
            foreach (ObjectPoolStruct aPool in aThePool)
            {
                ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
                objectPool.SetPrefab(aPool.ThePrefab);
                objectPool.SetPoolSize(aPool.MinimumPoolSize);
                objectPool.SetMaxPoolSize(aPool.MaximumPoolSize);

                aTheList.Add(aPool.ThePrefab, objectPool);
            }
        }

        #endregion
        #region Public Functions/Methods for use Outside of this Class

        public GameObject GetEnemy(GameObject aPrefab)
        {
            if (mEnemyList.TryGetValue(aPrefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {aPrefab.name} Pool initialized!");
            return null;
        }

        public GameObject GetPickup(GameObject aPrefab)
        {
            if (mPickupList.TryGetValue(aPrefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {aPrefab.name} Pool initialized!");
            return null;
        }

        public GameObject GetProjectile(GameObject aPrefab)
        {
            if (mProjectileList.TryGetValue(aPrefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {aPrefab.name} Pool initialized!");
            return null;
        }

        #endregion
    }
}