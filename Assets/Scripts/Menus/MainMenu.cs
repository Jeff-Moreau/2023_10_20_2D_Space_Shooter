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

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        #region Public Button Methods/Functions

        public void ExitGameButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIExitButton, eSoundFXSource.Normal);
            StartCoroutine(WaitForSoundToFinish());
        }

        private static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private IEnumerator WaitForSoundToFinish()
        {
            while (Manager.AudioManager.Access.GetSoundFXSource.isPlaying)
            {
                yield return null;
            }

            ExitGame();
        }

        public void StartGameButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIButtonClick, eSoundFXSource.Normal);
            SceneManager.LoadScene("LevelOne");
        }

        public void SettingsButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIButtonClick, eSoundFXSource.Normal);
            MainMenuScreen.SetActive(false);
            SettingsScreen.SetActive(true);
        }

        public void HighScoreButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIButtonClick, eSoundFXSource.Normal);
            MainMenuScreen.SetActive(false);
            HighScoreScreen.SetActive(true);
        }

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