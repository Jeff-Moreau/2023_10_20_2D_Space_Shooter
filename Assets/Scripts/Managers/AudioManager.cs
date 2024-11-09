/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: AudioManager.cs
 * Date Created: October 18, 2024
 * Created By: Jeff Moreau
 * Used On:
 * Description:
 ****************************************************************************************
 * Modified By: Jeff Moreau
 * Date Last Modified: November 7, 2024
 ****************************************************************************************
 * TODO:
 * Known Bugs:
 ****************************************************************************************/

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

//ENUMS
#region Public Enums: For Cross-Class References

public enum Music
{
    None,
    MainMenu,
    LevelOne,
    LevelTwo
}

public enum SoundFX
{
    None,
    UIButtonClick,
    UIHoverButton,
    UIExitButton,
    UIStartGame
}

public enum SoundFXSource
{
    Normal,
    EchoNormal,
    Ambient,
    EchoAmbient
}

public enum MusicSource
{
    Normal,
    EchoNormal
}

#endregion

namespace TrenchWars.Manager
{
	public class AudioManager : MonoBehaviour
	{
        //SINGLETON
        #region Singleton Handling: Instance Initialization and Access

        private static AudioManager _instance;

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

        public static AudioManager Access => _instance;

        #endregion

        //FIELDS
        #region Private Constants: For Class-Specific Fixed Values

        private const float VOLUME_MAX = 1.0f;
        private const float VOLUME_OFF = 0.0f;
        
        #endregion
        #region Private Serialized Fields: For Inspector Editable Values

        [Header("DATA >==============================================")]
        [SerializeField] private Data.AudioData _myData = null;
        [Space(10)]
        [Header("SOUND COMPONENTS >==================================")]
        [SerializeField] private AudioMixer _mainAudioMixer = null;
        [NonReorderable]
        [SerializeField] private AudioSource[] _musicSources = null;
        [NonReorderable]
        [SerializeField] private AudioSource[] _soundFXSources = null;

        #endregion
        #region Private Fields: For Internal Use

        private Coroutine _fadeMusic;

        #endregion

        //PROPERTIES
        #region Public Properties: For Accessing Class Fields

        public AudioSource GetSoundFXSource => _soundFXSources[(int)SoundFXSource.Normal];

        #endregion

        //METHODS
        #region Private Initialization Methods: For Class Setup

        private void Awake()
        {
            InitializeSingleton();
        }

        #endregion
        #region Private Implementation Methods: For Class Use

        private void PlayNewMusic(Music musicToPlay, MusicSource sourceToUse)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource musicSource = sourceToUse == MusicSource.Normal ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            musicSource.clip = _myData.GetMusicList[(int)musicToPlay];
            musicSource.loop = true;
            musicSource.Play();
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator FadeInMusic(float fadeDuration, float targetVolume, MusicSource sourceToUse)
        {
            // Happens when called
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource musicSource = sourceToUse == MusicSource.Normal ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            for (float time = 0 ; time < fadeDuration ; time += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(VOLUME_OFF, targetVolume, time / fadeDuration);

                // Wait for this to complete
                yield return null;
            }

            // Then do this after waiting
            musicSource.volume = targetVolume;
        }

        private IEnumerator FadeOutAndStopMusic(float fadeDuration)
        {
            // Happens when called
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource musicSource = _musicSources[(int)MusicSource.Normal].isPlaying ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            float currentVolume = musicSource.volume;

            for (float time = 0 ; time < fadeDuration ; time += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(currentVolume, VOLUME_OFF, time / fadeDuration);

                // Wait for this to complete
                yield return null;
            }

            // Then do this after waiting
            musicSource.volume = VOLUME_OFF;
            musicSource.Stop();
        }

        private IEnumerator FadeOutAndPlayNewMusic(float fadeDuration, Music musicToPlay, MusicSource sourceToUse, bool shouldFadeIn = false)
        {
            // Happens when called
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource musicSource = sourceToUse == MusicSource.Normal ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            float currentVolume = musicSource.volume;

            for (float time = 0 ; time < fadeDuration ; time += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(currentVolume, VOLUME_OFF, time / fadeDuration);

                // Wait for this to complete
                yield return null;
            }

            // Then do this after waiting
            musicSource.volume = VOLUME_OFF;
            musicSource.Stop();

            PlayMusic(musicToPlay, sourceToUse, shouldFadeIn);
        }

        #endregion
        #region Public Methods: For External Interactions

        public void PlaySound(SoundFX soundToPlay, SoundFXSource sourceToUse)
        {
            if ((int)soundToPlay < _myData.GetSoudFXList.Length)
            {
                switch (sourceToUse)
                {
                    case SoundFXSource.Normal:
                        _soundFXSources[(int)SoundFXSource.Normal].PlayOneShot(_myData.GetSoudFXList[(int)soundToPlay]);
                        break;

                    case SoundFXSource.EchoNormal:
                        _soundFXSources[(int)SoundFXSource.EchoNormal].PlayOneShot(_myData.GetSoudFXList[(int)soundToPlay]);
                        break;

                    case SoundFXSource.Ambient:
                        _soundFXSources[(int)SoundFXSource.Ambient].PlayOneShot(_myData.GetSoudFXList[(int)soundToPlay]);
                        break;

                    case SoundFXSource.EchoAmbient:
                        _soundFXSources[(int)SoundFXSource.EchoAmbient].PlayOneShot(_myData.GetSoudFXList[(int)soundToPlay]);
                        break;
                }
            }
        }

        public void PlayMusic(Music musicToPlay, MusicSource sourceToUse, bool shouldFadeIn = false)
        {
            if ((int)musicToPlay >= _myData.GetMusicList.Length)
            {
                Debug.LogWarning($"{_myData.GetMusicList[(int)musicToPlay].name} <color=yellow>Was not found, Stopping Music!</color>");

                foreach (AudioSource musicSource in _musicSources)
                {
                    musicSource.Stop();
                }

                return;
            }

            foreach (AudioSource musicSource in _musicSources)
            {
                if (musicSource.isPlaying)
                {
                    if (_fadeMusic != null)
                    {
                        StopCoroutine(_fadeMusic);
                    }

                    _fadeMusic = StartCoroutine(FadeOutAndPlayNewMusic(_myData.GetMusicInFadeDuration, musicToPlay, sourceToUse, shouldFadeIn));

                    return;
                }
            }

            if (shouldFadeIn)
            {
                foreach (AudioSource musicSource in _musicSources)
                {
                    musicSource.volume = VOLUME_OFF;
                }

                PlayNewMusic(musicToPlay, sourceToUse);

                if (_fadeMusic != null)
                {
                    StopCoroutine(_fadeMusic);
                }

                _fadeMusic = StartCoroutine(FadeInMusic(_myData.GetMusicInFadeDuration, VOLUME_MAX, sourceToUse));

                return;
            }

            PlayNewMusic(musicToPlay, sourceToUse);
        }

        public void StopMusic(bool shouldFadeOut = false)
        {
            if (!_musicSources[(int)MusicSource.Normal].isPlaying && !_musicSources[(int)MusicSource.EchoNormal].isPlaying)
            {
                Debug.LogWarning($"<color=yellow>There is no Music currently playing!</color>");

                return;
            }

            if (shouldFadeOut)
            {
                if (_fadeMusic != null)
                {
                    StopCoroutine(_fadeMusic);
                }

                _fadeMusic = StartCoroutine(FadeOutAndStopMusic(_myData.GetMusicInFadeDuration));
            }
            else
            {
                foreach (AudioSource musicSource in _musicSources)
                {
                    musicSource.Stop();
                }
            }
        }

        public void AdjustMasterVolume(float sliderValue)
        {
            _mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        }

        public void AdjustMusicVolume(float sliderValue)
        {
            _mainAudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        }

        public void AdjustSoundFXVolume(float sliderValue)
        {
            _mainAudioMixer.SetFloat("SoundFXVolume", Mathf.Log10(sliderValue) * 20);
        }

        public void AdjustAmbientVolume(float sliderValue)
        {
            _mainAudioMixer.SetFloat("AmbientVolume", Mathf.Log10(sliderValue) * 20);
        }

        #endregion
    }
}