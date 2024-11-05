/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: GameManager.cs
 * Date Created: October 18, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 5, 2024
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

        private static GameManager mInstance;
		
		private void InitializeSingleton()
		{
			if (mInstance != null && mInstance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				mInstance = this;
				DontDestroyOnLoad(gameObject);
			}
		}
	
		public static GameManager Access => mInstance;

        #endregion

        //ENUMS
        #region Private Enums: For Internal Use

        private enum eLevel
		{
			None,
			LevelOne,
			LevelTwo
		}

        #endregion

        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [SerializeField] private GameObject TheSaveLoadManager = null;
		[SerializeField] private GameObject TheAudioManager = null;
		[SerializeField] private GameObject TheInputManager = null;
		[SerializeField] private GameObject[] TheLevels = null;

        #endregion
        #region Private Fields: For Internal Use

        private int mCurrenLives; //Should be a Scriptable object
		private int mCurrentScore;
		private GameObject mCurrentLevel;

		#endregion

		//PROPERTIES
        #region Public Properties: For Accessing Class Fields

		public int Score
        {
            get => mCurrentScore;
            set => mCurrentScore = value;
        }

		public int Lives
		{
			get => mCurrenLives;
			set => mCurrenLives = value;
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
			InitializeVariables();
		}

		private void LoadEssentials()
		{
            if (!TheSaveLoadManager.activeInHierarchy)
            {
                if (TheSaveLoadManager != null)
                {
                    Instantiate(TheSaveLoadManager);
                    TheSaveLoadManager.SetActive(true);
                }
            }

            if (!TheAudioManager.activeInHierarchy)
            {
                if (TheAudioManager != null)
                {
                    Instantiate(TheAudioManager);
                    TheAudioManager.SetActive(true);
                }
            }

            if (!TheInputManager.activeInHierarchy)
            {
                if (TheInputManager != null)
                {
                    Instantiate(TheInputManager);
                    TheInputManager.SetActive(true);
                }
            }
        }

		private void InitializeVariables()
		{
			mCurrenLives = 3;
			mCurrentScore = 0;
			mCurrentLevel = TheLevels[(int)eLevel.LevelOne];
		}

        #endregion
        #region Public Methods: For External Interactions

        public void StartGame()
        {
            mCurrentLevel.SetActive(true);
        }

        #endregion
    }
}