/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: SaveLoadManager.cs
 * Date Created: October 20, 2024
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

namespace TrenchWars.Manager
{
	public class SaveLoadManager : MonoBehaviour
	{
        //SINGLETON
        #region Singleton Handling: Instance Initialization and Access

        private static SaveLoadManager _instance;
		
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
	
		public static SaveLoadManager Access => _instance;

        #endregion

        //FIELDS

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            InitializeSingleton();
        }

		#endregion
	}
}