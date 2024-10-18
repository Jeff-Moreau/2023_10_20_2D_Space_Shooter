/****************************************************************************************
 * Copyright: Jeff Moreau
 * Script: AudioManager.cs
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

using System.Collections;
using UnityEngine;

//ENUMERATORS
#region Public Enums

public enum eMusic
{
    None,
    MainMenu,
    LevelOne
}

public enum eSoundFX
{
    None,
    UIButtonClick,
    UIHoverButton
}

public enum eSoundFXSource
{
    Normal,
    EchoNormal,
    Ambient,
    EchoAmbient,
    UI
}

public enum eMusicSource
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
        #region Singleton

        private static AudioManager mInstance;

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

        public static AudioManager Access => mInstance;

        #endregion

        //VARIABLES
        #region Private Constant Variables/Fields used in this Class Only

        private const float MAX_VOLUME = 1.0f;
        private const float MIN_VOLUME = 0.0f;

        #endregion
        #region Inspector Variable Declarations and Initializations

        [Space(10)]
        [Header("DATA REQUIRED >======================---")]
        [SerializeField] private Data.AudioData TheAudioData = null;
        [Space(10)]
        [Header("MUSIC SOURCES >======================---")]
        [NonReorderable]
        [SerializeField] private AudioSource[] MusicSources = null;
        [Header("SOUNDFX SOURCES >======================---")]
        [NonReorderable]
        [SerializeField] private AudioSource[] SoundFXSources = null;

        #endregion
        #region Private Variables/Fields used in this Class Only

        private Coroutine mFadeMusic;

        private float mCurrentMusicVolume;
        private float mCurrentSoundFXVolume;
        private float mCurrentAmbientVolume;

        #endregion

        //FUNCTIONS
        #region Initialization Methods/Functions

        private void Awake() => InitializeSingleton();

        private void Start()
        {
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            mCurrentMusicVolume = MAX_VOLUME;
            mCurrentSoundFXVolume = MAX_VOLUME;
            mCurrentAmbientVolume = MAX_VOLUME;
        }

        #endregion
        #region Public Methods/Functions

        public void PlaySound(eSoundFX aSoundToPlay, eSoundFXSource aSourceToUse)
        {
            if ((int)aSoundToPlay < TheAudioData.GetSoudFXList.Length)
            {
                switch (aSourceToUse)
                {
                    case eSoundFXSource.Normal:
                        SoundFXSources[(int)eSoundFXSource.Normal].PlayOneShot(TheAudioData.GetSoudFXList[(int)aSoundToPlay]);
                        break;

                    case eSoundFXSource.EchoNormal:
                        SoundFXSources[(int)eSoundFXSource.EchoNormal].PlayOneShot(TheAudioData.GetSoudFXList[(int)aSoundToPlay]);
                        break;

                    case eSoundFXSource.Ambient:
                        SoundFXSources[(int)eSoundFXSource.Ambient].PlayOneShot(TheAudioData.GetSoudFXList[(int)aSoundToPlay]);
                        break;

                    case eSoundFXSource.EchoAmbient:
                        SoundFXSources[(int)eSoundFXSource.EchoAmbient].PlayOneShot(TheAudioData.GetSoudFXList[(int)aSoundToPlay]);
                        break;

                    case eSoundFXSource.UI:
                        SoundFXSources[(int)eSoundFXSource.UI].PlayOneShot(TheAudioData.GetSoudFXList[(int)aSoundToPlay]);
                        break;
                }
            }
        }

        public void PlayMusic(eMusic aMusicToPlay, eMusicSource aSourceToUse, bool aShouldFadeIn = false)
        {
            if ((int)aMusicToPlay >= TheAudioData.GetMusicList.Length)
            {
                Debug.LogWarning($"{TheAudioData.GetMusicList[(int)aMusicToPlay].name} <color=yellow>Was not found, Stopping Music!</color>");

                foreach (AudioSource aSource in MusicSources)
                {
                    aSource.Stop();
                }

                return;
            }

            foreach (AudioSource aSource in MusicSources)
            {
                if (aSource.isPlaying)
                {
                    if (mFadeMusic != null)
                    {
                        StopCoroutine(mFadeMusic);
                    }

                    mFadeMusic = StartCoroutine(FadeOutAndPlayNewMusic(TheAudioData.GetMusicFadeDuration, aMusicToPlay, aSourceToUse, aShouldFadeIn));

                    return;
                }
            }

            foreach (AudioSource aSource in MusicSources)
            {
                aSource.volume = mCurrentMusicVolume;
            }

            if (aShouldFadeIn)
            {
                foreach (AudioSource aSource in MusicSources)
                {
                    aSource.volume = MIN_VOLUME;
                }

                PlayNewMusic(aMusicToPlay, aSourceToUse);

                if (mFadeMusic != null)
                {
                    StopCoroutine(mFadeMusic);
                }

                mFadeMusic = StartCoroutine(FadeInMusic(TheAudioData.GetMusicFadeDuration, mCurrentMusicVolume, aSourceToUse));

                return;
            }

            foreach (AudioSource aSource in MusicSources)
            {
                aSource.volume = mCurrentMusicVolume;
            }

            PlayNewMusic(aMusicToPlay, aSourceToUse);
        }

        public void StopMusic(bool aShouldFadeOut = false)
        {
            if (!MusicSources[(int)eMusicSource.Normal].isPlaying && !MusicSources[(int)eMusicSource.EchoNormal].isPlaying)
            {
                Debug.LogWarning($"<color=yellow>There is no Music currently playing!</color>");

                return;
            }

            if (aShouldFadeOut)
            {
                if (mFadeMusic != null)
                {
                    StopCoroutine(mFadeMusic);
                }

                mFadeMusic = StartCoroutine(FadeOutAndStopMusic(TheAudioData.GetMusicFadeDuration));
            }
            else
            {
                foreach (AudioSource aSource in MusicSources)
                {
                    aSource.Stop();
                }
            }
        }

        public void AdjustMusicVolume(float aAmount)
        {
            mCurrentMusicVolume = aAmount;

            foreach (AudioSource aSource in MusicSources)
            {
                aSource.volume = mCurrentMusicVolume;
            }
        }

        public void AdjustSoudFXVolume(float aAmount)
        {
            mCurrentSoundFXVolume = aAmount;

            SoundFXSources[(int)eSoundFXSource.UI].volume = mCurrentSoundFXVolume;
            SoundFXSources[(int)eSoundFXSource.Normal].volume = mCurrentSoundFXVolume;
            SoundFXSources[(int)eSoundFXSource.EchoNormal].volume = mCurrentSoundFXVolume;
        }

        public void AdjustAmbientVolume(float aAmount)
        {
            mCurrentAmbientVolume = aAmount;

            SoundFXSources[(int)eSoundFXSource.Ambient].volume = mCurrentAmbientVolume;
            SoundFXSources[(int)eSoundFXSource.EchoAmbient].volume = mCurrentAmbientVolume;
        }

        #endregion
        #region Implementation Functions/Methods

        private void PlayNewMusic(eMusic aMusicToPlay, eMusicSource aSourceToUse)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = aSourceToUse == eMusicSource.Normal ? MusicSources[(int)eMusicSource.Normal] : MusicSources[(int)eMusicSource.EchoNormal];

            newSource.clip = TheAudioData.GetMusicList[(int)aMusicToPlay];
            newSource.loop = true;
            newSource.Play();
        }

        #endregion
        #region Coroutines

        private IEnumerator FadeInMusic(float aFadeDuration, float aTargetVolume, eMusicSource aSourceToUse)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = aSourceToUse == eMusicSource.Normal ? MusicSources[(int)eMusicSource.Normal] : MusicSources[(int)eMusicSource.EchoNormal];

            for (float time = 0 ; time < aFadeDuration ; time += Time.deltaTime)
            {
                newSource.volume = Mathf.Lerp(MIN_VOLUME, aTargetVolume, time / aFadeDuration);

                yield return null;
            }

            newSource.volume = aTargetVolume;
        }

        private IEnumerator FadeOutAndStopMusic(float aFadeDuration)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = MusicSources[(int)eMusicSource.Normal].isPlaying ? MusicSources[(int)eMusicSource.Normal] : MusicSources[(int)eMusicSource.EchoNormal];

            float currentVolume = newSource.volume;

            for (float time = 0 ; time < aFadeDuration ; time += Time.deltaTime)
            {
                newSource.volume = Mathf.Lerp(currentVolume, MIN_VOLUME, time / aFadeDuration);

                yield return null;
            }

            newSource.volume = MIN_VOLUME;
            newSource.Stop();
        }

        private IEnumerator FadeOutAndPlayNewMusic(float aFadeDuration, eMusic aMusicToPlay, eMusicSource aSourceToUse, bool aShouldFadeIn = false)
        {
            // Will need to change the way this works if more than 2 Music Sources
            AudioSource newSource = aSourceToUse == eMusicSource.Normal ? MusicSources[(int)eMusicSource.Normal] : MusicSources[(int)eMusicSource.EchoNormal];

            float currentVolume = newSource.volume;

            for (float time = 0 ; time < aFadeDuration ; time += Time.deltaTime)
            {
                newSource.volume = Mathf.Lerp(currentVolume, MIN_VOLUME, time / aFadeDuration);

                yield return null;
            }

            newSource.volume = MIN_VOLUME;
            newSource.Stop();

            PlayMusic(aMusicToPlay, aSourceToUse, aShouldFadeIn);
        }

        #endregion
    }
}