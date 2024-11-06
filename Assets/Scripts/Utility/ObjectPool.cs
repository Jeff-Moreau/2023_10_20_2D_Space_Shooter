/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: ObjectPool.cs
 * Date Created: October 29, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 6, 2024
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
        //FIELDS
        #region Private Fields: For Internal Use

        private int _maxPoolSize;
        private int _prefabPoolSize;
        private GameObject _thePrefab;
        private List<GameObject> _prefabList;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public int PoolSize
        {
            get => _prefabPoolSize;
            set
            {
                _prefabPoolSize = value;
                InitializePrefabPool();
            }
        }

        public int MaxPoolSize
        {
            get => _maxPoolSize;
            set => _maxPoolSize = value;
        }

        public GameObject Prefab
        {
            get => _thePrefab;
            set => _thePrefab = value;
        }

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            InitializeVariables();
            InitializePrefabPool();
        }

        private void InitializeVariables()
        {
            _thePrefab = null;
            _maxPoolSize = 4; // scriptable object
            _prefabPoolSize = 2; // scriptable object
            _prefabList = new List<GameObject>();
        }

        private void InitializePrefabPool()
        {
            _prefabList.Clear();

            for (int i = 0 ; i < _prefabPoolSize ; i++)
            {
                if (_thePrefab != null)
                {
                    AddNewPrefab();
                }
            }
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void AddNewPrefab()
        {
            GameObject newPrefab = Instantiate(_thePrefab);
            newPrefab.SetActive(false);
            _prefabList.Add(newPrefab);
        }

        #endregion
        #region Public Methods: For External Interactions

        public GameObject GetAPrefab()
        {
            for (int i = 0 ; i < _prefabList.Count ; i++)
            {
                if (!_prefabList[i].activeInHierarchy)
                {
                    return _prefabList[i];
                }
            }

            if (_prefabList.Count < _maxPoolSize)
            {
                AddNewPrefab();
                return _prefabList[^1];
            }

            return null;
        }

        #endregion
    }
}