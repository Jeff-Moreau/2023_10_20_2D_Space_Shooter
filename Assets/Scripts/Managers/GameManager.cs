/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: GameManager.cs
 * Date Created: October 18, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: October 18, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/
 
using UnityEngine;

//ENUMERATORS
#region Public Enumerator Declarations Only

// public enum eEnumName  // Example
// {
// 		Hey,
//		You
// }

#endregion

namespace TrenchWars.Manager
{
	public class GameManager : MonoBehaviour
	{
		//SINGLETON
		#region Singleton
		
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

		//ENUMERATORS
		#region Private Enumerator Declarations Only

		// private enum eEnumName  // Example
		// {
		// 		Hey,
		//		You
		// }

		#endregion

		//VARIABLES
		#region Constant Variable Declarations and Initializations

		// private const int MY_AGE = 44;  // Example

		#endregion
		#region Inspector Variable Declarations and Initializations to empty or null

		[SerializeField] private GameObject TheSaveLoadManager = null;
		[SerializeField] private GameObject TheAudioManager = null;

		#endregion
		#region Private Variable Declarations Only

		private int mCurrentLevel;
		private int mCurrentScore;
		
		#endregion
		
		//GETTERS AND SETTERS
		#region Accessors/Getters
		
		#endregion
		#region Mutators/Setters
		
		#endregion
		
		//FUNCTIONS
		#region Initialization Methods/Functions
		
		private void Awake() => InitializeSingleton();

		/*private void OnEnable()
		{
			
		}*/

		/*private void OnDisable()
		{
			
		}*/

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
        }

		private void InitializeVariables()
		{
			mCurrentLevel = 1;
			mCurrentScore = 0;
		}
		
		#endregion
		#region Implementation Methods/Functions
		
		/*private void Update()
		{
			
		}*/
		
		/*private void LateUpdate()
		{
			
		}*/
		
		#endregion
		#region Private Methods/Functions
		
		/*private void Save()
		{
		
		}*/
		
		/*private void Load()
		{
		
		}*/
		
		#endregion
		#region Public Methods/Functions
		
		//Public made functions go here
		
		#endregion
		#region Closing Methods/Functions
		
		/*private void OnApplicationQuit()
		{
			
		}*/
		
		/*private void OnDestroy()
		{
			
		}*/
		
		#endregion
	}
}