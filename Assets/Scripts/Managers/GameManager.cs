/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: GameManager.cs
 * Date Created: October 18, 2024
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

namespace TrenchWars.Manager
{
	public class GameManager : MonoBehaviour
	{
        //SINGLETON
        #region Singleton Handling: Instance Initialization and Access

        private static GameManager _instance;
		
		private void InitializeSingleton()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			}
		}
	
		public static GameManager Access => _instance;

        #endregion

        //ENUMS
        #region Private Enums: For Internal Use

        private enum Level
		{
			None,
			LevelOne,
			LevelTwo
		}

        #endregion

        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private GameObject _theSaveLoadManager = null;
		[SerializeField] private GameObject _theAudioManager = null;
		[SerializeField] private GameObject _theInputManager = null;
		[SerializeField] private GameObject[] _theLevels = null;

        #endregion
        #region Private Fields: For Internal Use

        private int _currentLives; //Should be a Scriptable object
		private int _currentScore;
		private GameObject _currentLevel;

		#endregion

		//PROPERTIES
        #region Public Properties: For Accessing Class Fields

		public int Score
        {
            get => _currentScore;
            set => _currentScore = value;
        }

		public int Lives
		{
			get => _currentLives;
			set => _currentLives = value;
		}

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            InitializeSingleton();
        }

        private void Start()
		{
			LoadEssentials();
			InitializeFields();
		}

		private void LoadEssentials()
		{
            if (!_theSaveLoadManager.activeInHierarchy)
            {
                if (_theSaveLoadManager != null)
                {
                    Instantiate(_theSaveLoadManager);
                    _theSaveLoadManager.SetActive(true);
                }
            }

            if (!_theAudioManager.activeInHierarchy)
            {
                if (_theAudioManager != null)
                {
                    Instantiate(_theAudioManager);
                    _theAudioManager.SetActive(true);
                }
            }

            if (!_theInputManager.activeInHierarchy)
            {
                if (_theInputManager != null)
                {
                    Instantiate(_theInputManager);
                    _theInputManager.SetActive(true);
                }
            }
        }

		private void InitializeFields()
		{
			_currentLives = 3;
			_currentScore = 0;
			_currentLevel = _theLevels[(int)Level.LevelOne];
		}

        #endregion
        #region Public Methods: For External Interactions

        public void StartGame()
        {
            _currentLevel.SetActive(true);
        }

        #endregion
    }
}