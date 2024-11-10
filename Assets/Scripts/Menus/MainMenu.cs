/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: MainMenu.cs
 * Date Created: October 20, 2023
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
using UnityEngine.UI;
using System.Collections;

namespace TrenchWars
{
    public class MainMenu : MonoBehaviour
    {
        //FIELDS
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("OBJECT REFERENCES >=================================")]
        [SerializeField] private GameObject _theMainMenuScreen = null;
        [SerializeField] private GameObject _theSettingsScreen = null;
        [SerializeField] private GameObject _theHighScoreScreen = null;
        [SerializeField] private GameObject _theHUD = null;
        [Header("SLIDERS >===========================================")]
        [SerializeField] private Slider _masterVolumeSlider = null;
        [SerializeField] private Slider _musicVolumeSlider = null;
        [SerializeField] private Slider _soundFXVolumeSlider = null;
        [SerializeField] private Slider _ambientVolumeSlider = null;

        #endregion
        #region Private Fields: For Internal Use

        private bool _buttonIsPushed;

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Start()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            _buttonIsPushed = false;
            Manager.AudioManager.Access.PlayMusic(Music.MainMenu, MusicSource.Normal, true);
            _masterVolumeSlider.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustMasterVolume);
            _musicVolumeSlider.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustMusicVolume);
            _soundFXVolumeSlider.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustSoundFXVolume);
            _ambientVolumeSlider.onValueChanged.AddListener(Manager.AudioManager.Access.AdjustAmbientVolume);
        }

        #endregion
        #region Private Activation Methods: For Script Activation

        private void OnEnable()
        {
            _buttonIsPushed = false;
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void StartGame()
        {
            Manager.GameManager.Access.StartGame();
            _theHUD.SetActive(true);
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator WaitForExitSoundOver()
        {
            while (Manager.AudioManager.Access.GetSoundFXSource.isPlaying)
            {
                yield return null;
            }

            _buttonIsPushed = false;
            ExitGame();
        }

        private IEnumerator WaitForStartSoundOver()
        {
            while (Manager.AudioManager.Access.GetSoundFXSource.isPlaying)
            {
                yield return null;
            }

            _buttonIsPushed = false;
            StartGame();
        }

        #endregion
        #region Public Methods: For External Interactions

        public void ExitGameButton()
        {
            if (!_buttonIsPushed)
            {
                _buttonIsPushed = true;
                Manager.AudioManager.Access.PlaySound(SoundFX.UIExitButton, SoundFXSource.Normal);
                StartCoroutine(WaitForExitSoundOver());
            }
        }

        public void StartGameButton()
        {
            if (!_buttonIsPushed)
            {
                _buttonIsPushed = true;
                Manager.AudioManager.Access.PlaySound(SoundFX.UIStartGame, SoundFXSource.Normal);
                StartCoroutine(WaitForStartSoundOver());
            }
        }

        public void SettingsButton()
        {

                Manager.AudioManager.Access.PlaySound(SoundFX.UIButtonClick, SoundFXSource.Normal);
                _theMainMenuScreen.SetActive(false);
                _theSettingsScreen.SetActive(true);
        }

        public void HighScoreButton()
        {

                Manager.AudioManager.Access.PlaySound(SoundFX.UIButtonClick, SoundFXSource.Normal);
                _theMainMenuScreen.SetActive(false);
                _theHighScoreScreen.SetActive(true);
        }

        public void BackToMainMenuButton()
        {

                Manager.AudioManager.Access.PlaySound(SoundFX.UIButtonClick, SoundFXSource.Normal);
                _theSettingsScreen.SetActive(false);
                _theHighScoreScreen.SetActive(false);
                _theMainMenuScreen.SetActive(true);
        }

        #endregion
    }
}