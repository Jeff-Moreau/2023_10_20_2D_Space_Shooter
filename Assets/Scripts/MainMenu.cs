/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: MainMenu.cs
 * Date Created: October 20, 2023
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

namespace TrenchWars
{
    public class MainMenu : MonoBehaviour
    {
        //VARIABLES
        #region Constant Variable Declarations and Initializations

        // private const int MY_AGE = 44;  // Example

        #endregion
        #region Inspector Variable Declarations and Initializations to empty or null

        [SerializeField] private GameObject MainMenuScreen = null;
        [SerializeField] private GameObject SettingsScreen = null;
        [SerializeField] private GameObject HighScoreScreen = null;

        #endregion
        #region Private Variable Declarations Only

        // private int mMyInt;  // Example

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        /*private void Awake()
		{
			//used for when the object is FIRST activated and ONLY ONCE
		}*/

        /*private void OnEnable()
		{
			//Anytime the Object is set to active this is called
		}*/

        /*private void OnDisable()
		{
			
		}*/

        private void Start() => InitializeVariables();

        private void InitializeVariables()
        {
            Manager.AudioManager.Access.PlayMusic(eMusic.MainMenu, eMusicSource.Normal, true);
        }

        #endregion
        #region Physics Methods/Functions

        /*private void FixedUpdate()
		{
			#NOTRIM#
		}*/

        /*private void OnCollisionEnter(Collision collision)
		{
			#NOTRIM#
		}*/

        /*private void OnCollisionStay(Collision collision)
		{
			#NOTRIM#
		}*/

        /*private void OnCollisionExit(Collision collision)
		{
			#NOTRIM#
		}*/

        /*private void OnTriggerEnter(Collider other)
		{
			#NOTRIM#
		}*/

        /*private void OnTriggerStay(Collider other)
		{
			#NOTRIM#
		}*/

        /*private void OnTriggerExit(Collider other)
		{
			#NOTRIM#
		}*/

        #endregion
        #region Implementation Methods/Functions

        /*private void Update()
		{
			#NOTRIM#
		}*/

        /*private void LateUpdate()
		{
			//Just like Updated but done after Update
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
			#NOTRIM#
		}*/

        /*private void OnDestroy()
		{
			#NOTRIM#
		}*/

        #endregion
        #region Old Code

        /*[SerializeField] private GameObject mThisScreen = null;
        [SerializeField] private GameObject mPlayScreen = null;
        [SerializeField] private GameObject mPlayScreenUI = null;
        [SerializeField] private GameObject mManagers = null;
        [SerializeField] private GameObject mPlayer = null;
        [SerializeField] private AudioSource mThisMusic = null;

        private void Start()
        {
            mThisMusic.Play();
        }

        public void ClickStartGame()
        {
            mThisMusic.Stop();
            mThisScreen.SetActive(false);
            mManagers.SetActive(true);
            mPlayScreen.SetActive(true);
            mPlayScreenUI.SetActive(true);
            mPlayer.SetActive(true);
        }

        public void ClickExitGame()
        {
            Application.Quit();
            EditorApplication.isPlaying = false; // Remove before building the game
        }*/

        #endregion
    }
}