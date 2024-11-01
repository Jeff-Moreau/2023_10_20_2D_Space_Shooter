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
        #region Private Variables/Fields used in this Class Only

        private int mMaxPoolSize;
        private int mPrefabPoolSize;
        private GameObject mThePrefab;
        private List<GameObject> mPrefabList;

        #endregion

        //GETTERS AND SETTERS
        #region Public Getters/Accessors for use Outside of this Class Only

        public int GetPoolSize => mPrefabPoolSize;
        public int GetMaxPoolSize => mMaxPoolSize;

        #endregion
        #region Public Setters/Mutators for use Outside of this Class Only

        public void SetPoolSize(int aPoolSize)
        {
            mPrefabPoolSize = aPoolSize;
            InitializePrefabPool();
        }

        public void SetPrefab(GameObject aPrefab) => mThePrefab = aPrefab;
        public void SetMaxPoolSize(int aSize) => mMaxPoolSize = aSize;

        #endregion

        //FUNCTIONS
        #region Private Initialization Functions/Methods used in this Class Only

        private void Awake()
        {
            InitializeVariables();
            InitializePrefabPool();
        }

        private void InitializeVariables()
        {
            mThePrefab = null;
            mMaxPoolSize = 4;
            mPrefabPoolSize = 2;
            mPrefabList = new List<GameObject>();
        }

        private void InitializePrefabPool()
        {
            mPrefabList.Clear();

            for (int i = 0 ; i < mPrefabPoolSize ; i++)
            {
                if (mThePrefab != null)
                {
                    AddNewPrefab();
                }
            }
        }

        #endregion
        #region Private Implementation Functions/Methods used in this Class Only

        private void AddNewPrefab()
        {
            GameObject newPrefab = Instantiate(mThePrefab);
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

            if (mPrefabList.Count < mMaxPoolSize)
            {
                AddNewPrefab();
                return mPrefabList[^1];
            }

            return null;
        }

        #endregion
    }
}