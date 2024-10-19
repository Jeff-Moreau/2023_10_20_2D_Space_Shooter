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
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TrenchWars
{
    public class MainMenu : MonoBehaviour
    {
        //VARIABLES
        #region Inspector Variable Declarations and Initializations to empty or null

        [SerializeField] private GameObject MainMenuScreen = null;
        [SerializeField] private GameObject SettingsScreen = null;
        [SerializeField] private GameObject HighScoreScreen = null;
        [SerializeField] private Slider MasterVolume = null;
        [SerializeField] private Slider MusicVolume = null;
        [SerializeField] private Slider SoundFXVolume = null;
        //[SerializeField] private Slider AmbientVolume = null;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        private void Start() => InitializeVariables();

        private void InitializeVariables()
        {
            Manager.AudioManager.Access.PlayMusic(eMusic.MainMenu, eMusicSource.Normal, true);
            MasterVolume.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustMasterVolume);
            MusicVolume.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustMusicVolume);
            SoundFXVolume.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustSoundFXVolume);
            //AmbientVolume.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustAmbientVolume);
        }

        #endregion
        #region Public Button Methods/Functions

        public void ExitGameButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIExitButton, eSoundFXSource.Normal);
            StartCoroutine(WaitForExitSoundOver());
        }

        private static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private IEnumerator WaitForExitSoundOver()
        {
            while (Manager.AudioManager.Access.GetSoundFXSource.isPlaying)
            {
                yield return null;
            }

            ExitGame();
        }

        private IEnumerator WaitForStartSoundOver()
        {
            while (Manager.AudioManager.Access.GetSoundFXSource.isPlaying)
            {
                yield return null;
            }

            StartGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene("LevelOne");
        }

        public void StartGameButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIStartGame, eSoundFXSource.Normal);
            StartCoroutine(WaitForStartSoundOver());
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

        public void BackToMainMenuButton()
        {
            Manager.AudioManager.Access.PlaySound(eSoundFX.UIButtonClick, eSoundFXSource.Normal);
            SettingsScreen.SetActive(false);
            HighScoreScreen.SetActive(false);
            MainMenuScreen.SetActive(true);
        }

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