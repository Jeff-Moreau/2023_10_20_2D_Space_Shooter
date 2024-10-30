/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPool.cs
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
	public class ObjectPool : MonoBehaviour
	{
        //VARIABLES
        #region Private Variables/Fields Exposed to Inspector for Editing

        [SerializeField] private GameObject ThePrefab = null;
        [SerializeField] private int PrefabPoolSize = 10;
        [SerializeField] private int MaxPoolSize = 20;

        #endregion
        #region Private Variables/Fields used in this Class Only

        private List<GameObject> mPrefabList;

        #endregion

        //GETTERS AND SETTERS
        #region Public Getters/Accessors for use Outside of this Class Only

        public int GetPoolSize => PrefabPoolSize;
        public int GetMaxPoolSize => MaxPoolSize;

        #endregion
        #region Public Setters/Mutators for use Outside of this Class Only

        public void SetPoolSize(int poolSize)
        {
            PrefabPoolSize = poolSize;
            InitializePrefabPool();
        }

        public void SetPrefab(GameObject prefab) => ThePrefab = prefab;
        public void SetMaxPoolSize(int size) => MaxPoolSize = size;

        #endregion

        //FUNCTIONS
        #region Private Initialization Functions/Methods used in this Class Only

        private void Awake()
        {
            InitializeVariables();
            InitializePrefabPool();
        }

        private void InitializeVariables() => mPrefabList = new List<GameObject>();
        private void InitializePrefabPool()
        {
            mPrefabList.Clear();

            for (int i = 0 ; i < PrefabPoolSize ; i++)
            {
                AddNewPrefab();
            }
        }

        #endregion
        #region Private Implementation Functions/Methods used in this Class Only

        private void AddNewPrefab()
        {
            GameObject newPrefab = Instantiate(ThePrefab);
            newPrefab.SetActive(false);
            mPrefabList.Add(newPrefab);
        }

        #endregion
        #region Public Functions/Methods for use Outside of this Class

        public GameObject GetAPrefab()
        {
            for (int i = 0 ; i < mPrefabList.Count ; i++)
            {
                if (!mPrefabList[i].activeInHierarchy)
                {
                    return mPrefabList[i];
                }
            }

            if (mPrefabList.Count < MaxPoolSize)
            {
                AddNewPrefab();
                return mPrefabList[^1];
            }

            return null;
        }

        #endregion
    }
}