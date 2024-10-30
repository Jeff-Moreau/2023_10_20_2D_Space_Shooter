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

        [SerializeField] private List<PoolStruct> Pools = null;

        #endregion
        #region Private Variables/Fields used in this Class Only

        private Dictionary<GameObject, ObjectPool> mPoolLookup;

        #endregion

        //FUCNTIONS
        #region Private Initialization Functions/Methods used in this Class Only

        private void Awake() => InitializePools();

        private void InitializePools()
        {
            mPoolLookup = new Dictionary<GameObject, ObjectPool>();

            foreach (PoolStruct aPool in Pools)
            {
                ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
                objectPool.SetPrefab(aPool.ThePrefab);
                objectPool.SetPoolSize(aPool.MaxSize);
                objectPool.SetMaxPoolSize(aPool.MaxSize);

                mPoolLookup.Add(aPool.ThePrefab, objectPool);
            }
        }

        #endregion
        #region Public Functions/Methods for use Outside of this Class

        public GameObject GetObject(GameObject aPrefab)
        {
            if (mPoolLookup.TryGetValue(aPrefab, out ObjectPool objectPool))
            {
                return objectPool.GetAPrefab();
            }

            Debug.LogWarning($"There is no {aPrefab.name} Pool initialized!");
            return null;
        }

        #endregion
    }
}