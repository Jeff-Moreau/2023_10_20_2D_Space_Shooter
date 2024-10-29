/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPool.cs
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

using UnityEngine;
using System.Collections.Generic;

namespace TrenchWars
{
	public class ObjectPool : MonoBehaviour
	{
        //VARIABLES
        #region Inspector Variable Declarations and Initializations

        [SerializeField] private GameObject ThePrefab = null;
        [SerializeField] private int PrefabPoolSize = 1;
        [SerializeField] private int MaxPoolSize = 20;

        #endregion
        #region Private Variable Declarations Only
        
        private List<GameObject> mPrefabList;

        #endregion

        //GETTERS AND SETTERS
        #region Accessors/Getters

        public int GetPoolSize => PrefabPoolSize;
        public int GetMaxPoolSize => MaxPoolSize;

        #endregion
        #region Mutators/Setters

        public void SetPoolSize(int poolSize)
        {
            PrefabPoolSize = poolSize;
            InitializePrefabPool();
        }

        public void SetPrefab(GameObject prefab) => ThePrefab = prefab;
        public void SetMaxPoolSize(int size) => MaxPoolSize = size;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        private void Awake()
        {
            InitializeVariables();
            InitializePrefabPool();
        }

        private void InitializeVariables() => mPrefabList = new List<GameObject>();

        #endregion
        #region Implementation Methods/Functions

        private void InitializePrefabPool()
        {
            mPrefabList.Clear();

            for (int i = 0 ; i < PrefabPoolSize ; i++)
            {
                AddNewPrefab();
            }
        }

        private void AddNewPrefab()
        {
            GameObject newPrefab = Instantiate(ThePrefab);
            newPrefab.SetActive(false);
            mPrefabList.Add(newPrefab);
        }

        #endregion
        #region Public Methods/Functions

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
                return mPrefabList[mPrefabList.Count - 1];
            }

            return null;
        }

        #endregion
    }
}