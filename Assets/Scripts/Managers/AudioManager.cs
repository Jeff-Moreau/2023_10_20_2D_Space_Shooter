/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: AudioManager.cs
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

        private const float MAX_VOLUME = 1.0f;
        private const float MIN_VOLUME = 0.0f;
        
        #endregion
        #region Private Serialized Fields: For Inspector Editable Values

        [Space(10)]
        [Header("DATA REQUIRED >======================---")]
        [SerializeField] private Data.AudioData _myData = null;
        [SerializeField] private AudioMixer _mainAudioMixer = null;
        [Space(10)]
        [Header("MUSIC SOURCES >======================---")]
        [NonReorderable]
        [SerializeField] private AudioSource[] _musicSources = null;
        [Header("SOUNDFX SOURCES >======================---")]
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
            AudioSource newSource = sourceToUse == MusicSource.Normal ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            newSource.clip = _myData.GetMusicList[(int)musicToPlay];
            newSource.loop = true;
            newSource.Play();
        }

        #endregion
        #region Private Coroutine Methods: for Asynchronous Operations

        private IEnumerator FadeInMusic(float fadeDuration, float targetVolume, MusicSource sourceToUse)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = sourceToUse == MusicSource.Normal ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            for (float time = 0 ; time < fadeDuration ; time += Time.deltaTime)
            {
                newSource.volume = Mathf.Lerp(MIN_VOLUME, targetVolume, time / fadeDuration);

                yield return null;
            }

            newSource.volume = targetVolume;
        }

        private IEnumerator FadeOutAndStopMusic(float fadeDuration)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = _musicSources[(int)MusicSource.Normal].isPlaying ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            float currentVolume = newSource.volume;

            for (float time = 0 ; time < fadeDuration ; time += Time.deltaTime)
            {
                newSource.volume = Mathf.Lerp(currentVolume, MIN_VOLUME, time / fadeDuration);

                yield return null;
            }

            newSource.volume = MIN_VOLUME;
            newSource.Stop();
        }

        private IEnumerator FadeOutAndPlayNewMusic(float fadeDuration, Music musicToPlay, MusicSource sourceToUse, bool shouldFadeIn = false)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = sourceToUse == MusicSource.Normal ? _musicSources[(int)MusicSource.Normal] : _musicSources[(int)MusicSource.EchoNormal];

            float currentVolume = newSource.volume;

            for (float time = 0 ; time < fadeDuration ; time += Time.deltaTime)
            {
                newSource.volume = Mathf.Lerp(currentVolume, MIN_VOLUME, time / fadeDuration);

                yield return null;
            }

            newSource.volume = MIN_VOLUME;
            newSource.Stop();

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

                foreach (AudioSource aSource in _musicSources)
                {
                    aSource.Stop();
                }

                return;
            }

            foreach (AudioSource aSource in _musicSources)
            {
                if (aSource.isPlaying)
                {
                    if (_fadeMusic != null)
                    {
                        StopCoroutine(_fadeMusic);
                    }

                    _fadeMusic = StartCoroutine(FadeOutAndPlayNewMusic(_myData.GetMusicFadeDuration, musicToPlay, sourceToUse, shouldFadeIn));

                    return;
                }
            }

            if (shouldFadeIn)
            {
                foreach (AudioSource aSource in _musicSources)
                {
                    aSource.volume = MIN_VOLUME;
                }

                PlayNewMusic(musicToPlay, sourceToUse);

                if (_fadeMusic != null)
                {
                    StopCoroutine(_fadeMusic);
                }

                _fadeMusic = StartCoroutine(FadeInMusic(_myData.GetMusicFadeDuration, MAX_VOLUME, sourceToUse));

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

                _fadeMusic = StartCoroutine(FadeOutAndStopMusic(_myData.GetMusicFadeDuration));
            }
            else
            {
                foreach (AudioSource aSource in _musicSources)
                {
                    aSource.Stop();
                }
            }
        }

        public void AdjustMasterVolume(float amount)
        {
            _mainAudioMixer.SetFloat("MasterVolume", Mathf.Log10(amount) * 20);
        }

        public void AdjustMusicVolume(float amount)
        {
            _mainAudioMixer.SetFloat("MusicVolume", Mathf.Log10(amount) * 20);
        }

        public void AdjustSoundFXVolume(float amount)
        {
            _mainAudioMixer.SetFloat("SoundFXVolume", Mathf.Log10(amount) * 20);
        }

        public void AdjustAmbientVolume(float amount)
        {
            _mainAudioMixer.SetFloat("AmbientVolume", Mathf.Log10(amount) * 20);
        }

        #endregion
    }
}